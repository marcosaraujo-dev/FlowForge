# WPF — Ciclo de Vida de Views e ViewModels

Entender quando cada componente é criado, inicializado e destruído é fundamental para evitar bugs de inicialização, memory leaks e comportamentos inesperados.

---

## Índice

1. [Visão Geral do Ciclo](#1-visão-geral-do-ciclo)
2. [Fase 1 — Criação](#2-fase-1---criação)
3. [Fase 2 — Inicialização do ViewModel](#3-fase-2---inicialização-do-viewmodel)
4. [Fase 3 — View Ativa](#4-fase-3---view-ativa)
5. [Fase 4 — Cleanup e Dispose](#5-fase-4---cleanup-e-dispose)
6. [Problemas Comuns e Soluções](#6-problemas-comuns-e-soluções)

---

## 1. Visão Geral do Ciclo

```
[DI Container resolve View + ViewModel]
         |
         v
[Construtor da View]
  InitializeComponent()
  DataContext = viewModel
         |
         v
[Bindings inicializam]
  ViewModel.Properties lidas pela primeira vez
         |
         v
[View.Loaded event]
  ViewModel.OnViewLoaded() chamado
  Carregamento de dados iniciais
         |
         v
[View ativa — interação do usuário]
         |
         v
[View.Unloaded event]
  ViewModel.OnViewUnloaded() chamado
  Limpeza de recursos
```

**Regra crítica:** Nunca carregue dados no **construtor** do ViewModel. Use o evento `Loaded` da View ou um método `InitializeAsync()` chamado via Command.

---

## 2. Fase 1 — Criação

### Construtor da View

O construtor **só** chama `InitializeComponent()`. O DataContext é definido imediatamente depois pelo container DI.

```csharp
public partial class FuncionariosView : UserControl
{
    public FuncionariosView(FuncionariosViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
```

> O DataContext pode ser definido no construtor (como acima) ou via DI automático.

### O que NÃO fazer no construtor da View

```csharp
// ❌ ERRADO — nunca acesse o ViewModel antes do Loaded
public FuncionariosView(FuncionariosViewModel viewModel)
{
    InitializeComponent();
    DataContext = viewModel;
    viewModel.CarregarAsync(); // ❌ Muito cedo — bindings ainda não estão prontos
}
```

### Construtor do ViewModel

No construtor do ViewModel, apenas configure Commands e inicialize propriedades estáticas. **Não faça chamadas async aqui.**

```csharp
public FuncionariosViewModel(IFuncionarioService service)
{
    _service = service;

    // ✅ OK no construtor — apenas configuração
    CarregarCommand = new AsyncRelayCommand(CarregarAsync);
    Funcionarios = new ObservableCollection<FuncionarioDto>();
}
```

---

## 3. Fase 2 — Inicialização do ViewModel

### Quando carregar dados

O carregamento de dados deve acontecer **após** a View estar pronta. Duas abordagens válidas:

**Opção A: Evento Loaded da View (recomendado)**

```csharp
// Na View
public FuncionariosView(FuncionariosViewModel viewModel)
{
    InitializeComponent();
    DataContext = viewModel;
    Loaded += async (s, e) => await viewModel.InicializarAsync();
}

// No ViewModel
public async Task InicializarAsync()
{
    try
    {
        _loadingService.Show("Carregando...");
        Funcionarios = new ObservableCollection<FuncionarioDto>(
            await _service.ListarAsync());
    }
    finally
    {
        _loadingService.Hide();
    }
}
```

**Opção B: Command executado pelo XAML via Trigger**

```xml
<UserControl>
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Loaded">
            <i:InvokeCommandAction Command="{Binding CarregarCommand}"/>
        </i:EventTrigger>
    </i:Interaction.Triggers>
</UserControl>
```

---

## 4. Fase 3 — View Ativa

Durante a fase ativa, o fluxo é:

```
Usuário interage com a View
         |
         v
Binding envia valor para ViewModel (TwoWay)
         |
         v
ViewModel processa via Command ou Property setter
         |
         v
ViewModel notifica View via SetProperty() / OnPropertyChanged()
         |
         v
View atualiza o elemento visual
```

### Notificação de mudança

```csharp
// ✅ SetProperty notifica automaticamente
private string _nome;
public string Nome
{
    get => _nome;
    set => SetProperty(ref _nome, value); // notifica binding
}

// ✅ Para propriedades calculadas (readonly)
public string NomeCompleto => $"{Nome} {Sobrenome}";
// Deve notificar manualmente quando dependências mudam:
set
{
    SetProperty(ref _nome, value);
    OnPropertyChanged(nameof(NomeCompleto)); // ← notifica derivada
}
```

---

## 5. Fase 4 — Cleanup e Dispose

### Quando implementar IDisposable

Implemente `IDisposable` no ViewModel quando ele tiver:
- Subscriptions a eventos externos (`+=`)
- Timers
- Streams ou recursos não gerenciados

```csharp
public class MonitorViewModel : ViewModelBase, IDisposable
{
    private readonly Timer _timer;

    public MonitorViewModel()
    {
        _timer = new Timer(AtualizarDados, null, 0, 5000);
    }

    public void Dispose()
    {
        _timer?.Dispose(); // ← evita memory leak
    }
}
```

### Evento Unloaded na View

```csharp
public FuncionariosView(FuncionariosViewModel viewModel)
{
    InitializeComponent();
    DataContext = viewModel;
    Unloaded += (s, e) => (viewModel as IDisposable)?.Dispose();
}
```

---

## 6. Problemas Comuns e Soluções

### Problema: Binding não atualiza a View

**Causa:** Property não chama `SetProperty()` ou `OnPropertyChanged()`.

```csharp
// ❌ Não notifica
public string Nome { get; set; }

// ✅ Notifica corretamente
private string _nome;
public string Nome
{
    get => _nome;
    set => SetProperty(ref _nome, value);
}
```

---

### Problema: NullReferenceException ao abrir a View

**Causa:** ViewModel tenta acessar dados no construtor antes dos bindings estarem prontos.

**Solução:** Mova o carregamento de dados para `InicializarAsync()` chamado no `Loaded`.

---

### Problema: ComboBox "colapsa" horizontalmente

**Causa:** `ScrollViewer` pai sem `HorizontalScrollBarVisibility="Disabled"`.

```xml
<!-- ✅ Sempre adicionar em ScrollViewers que contêm formulários -->
<ScrollViewer HorizontalScrollBarVisibility="Disabled">
    <StackPanel>
        <ComboBox .../>
    </StackPanel>
</ScrollViewer>
```

---

### Problema: Memory leak — ViewModel não é coletado pelo GC

**Causa:** Evento estático ou serviço global mantém referência ao ViewModel.

**Solução:** Usar `WeakReference` para eventos ou implementar `IDisposable` com unsubscribe explícito.

---

### Problema: Loading não some após erro

**Causa:** `Hide()` não está no bloco `finally`.

```csharp
// ❌ Se der exceção, loading fica preso
try
{
    _loadingService.Show();
    await OperacaoAsync();
    _loadingService.Hide(); // ← não executa se exceção
}
catch { }

// ✅ Correto
try
{
    _loadingService.Show();
    await OperacaoAsync();
}
finally
{
    _loadingService.Hide(); // ← sempre executa
}
```
