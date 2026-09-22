# WPF — Padrão MVVM

O padrão MVVM (Model-View-ViewModel) separa a lógica de apresentação da interface visual. É o padrão arquitetural obrigatório em todas as telas WPF Empresa.

---

## Índice

1. [Os três camadas](#1-as-três-camadas)
2. [ViewModel](#2-viewmodel)
3. [View (XAML)](#3-view-xaml)
4. [Code-Behind](#4-code-behind)
5. [Data Binding](#5-data-binding)
6. [Commands](#6-commands)
7. [Dependency Injection](#7-dependency-injection)
8. [Serviços de UI](#8-serviços-de-ui)

---

## 1. As três camadas

```
+-- Model -----------+    +-- ViewModel --------+    +-- View (XAML) ---+
|                    |    |                     |    |                  |
| Entidades          |<---| Dados observáveis   |<-->| Binding          |
| DTOs               |    | Commands            |    | Triggers         |
| Repositórios       |    | Lógica de           |    | Animações        |
| Services           |    | apresentação        |    |                  |
+--------------------+    +---------------------+    +------------------+
```

**Regra fundamental:** A View nunca acessa o Model diretamente. Toda comunicação passa pelo ViewModel.

---

## 2. ViewModel

### Estrutura base

```csharp
public sealed class FuncionariosViewModel : ViewModelBase
{
    // Dependencies via DI
    private readonly IFuncionarioService _service;
    private readonly IAlertService _alertService;
    private readonly ILoadingService _loadingService;

    // Properties observáveis
    private ObservableCollection<FuncionarioDto> _funcionarios = new();
    public ObservableCollection<FuncionarioDto> Funcionarios
    {
        get => _funcionarios;
        set => SetProperty(ref _funcionarios, value);
    }

    private FuncionarioDto? _selecionado;
    public FuncionarioDto? Selecionado
    {
        get => _selecionado;
        set => SetProperty(ref _selecionado, value);
    }

    // Commands
    public IAsyncRelayCommand CarregarCommand { get; }
    public IRelayCommand<FuncionarioDto> EditarCommand { get; }

    public FuncionariosViewModel(
        IFuncionarioService service,
        IAlertService alertService,
        ILoadingService loadingService)
    {
        _service = service;
        _alertService = alertService;
        _loadingService = loadingService;

        CarregarCommand = new AsyncRelayCommand(CarregarAsync);
        EditarCommand = new RelayCommand<FuncionarioDto>(Editar);
    }

    private async Task CarregarAsync()
    {
        try
        {
            _loadingService.Show("Carregando funcionários...");
            var lista = await _service.ListarAsync();
            Funcionarios = new ObservableCollection<FuncionarioDto>(lista);
        }
        catch (Exception ex)
        {
            _alertService.ShowError("Não foi possível carregar os dados.");
            _logger.Error(ex, "Erro ao carregar funcionários");
        }
        finally
        {
            _loadingService.Hide();   // SEMPRE no finally
        }
    }

    private void Editar(FuncionarioDto dto)
    {
        // Navegar para tela de edição
    }
}
```

### Regras do ViewModel

| Obrigatório | Proibido |
|------------|---------|
| Herdar de `ViewModelBase` | Criar instâncias de Services (`new`) |
| Usar `SetProperty()` para notificação | Referenciar controles WPF (TextBox, DataGrid) |
| Injetar dependências via construtor | Código de negócio em properties |
| `try-finally` garantindo `HideLoading` | Acessar `Application.Current.Dispatcher` diretamente |
| `ObservableCollection` para listas | Campos públicos (usar sempre Properties) |

---

## 3. View (XAML)

### Estrutura padrão

```xml
<UserControl x:Class="MeuProjeto.Views.FuncionariosView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Loading overlay — sempre o primeiro filho -->
    <Grid>
        <ScrollViewer HorizontalScrollBarVisibility="Disabled">
            <StackPanel Margin="{StaticResource Margin.Container}">

                <!-- Page Header -->
                <Border Style="{StaticResource PageTitleContainer}">
                    <TextBlock Style="{StaticResource PageTitle}"
                               Text="Funcionários"/>
                </Border>

                <!-- Conteúdo -->
                <DataGrid ItemsSource="{Binding Funcionarios}"
                          SelectedItem="{Binding Selecionado, Mode=TwoWay}"/>

            </StackPanel>
        </ScrollViewer>

        <!-- Loading overlay sobre tudo -->
        <local:LoadingOverlay IsVisible="{Binding IsLoading}"/>
    </Grid>
</UserControl>
```

### Regras da View

- Sempre usar `{StaticResource}` para cores e estilos — nunca hardcode
- `Binding Mode` explícito: `TwoWay` para inputs, `OneWay` para displays
- `UpdateSourceTrigger="PropertyChanged"` em inputs de formulário
- `HorizontalScrollBarVisibility="Disabled"` no `ScrollViewer` para evitar expansão horizontal

---

## 4. Code-Behind

O code-behind deve ser **quase vazio**. Apenas `InitializeComponent()` é obrigatório.

```csharp
// ✅ CORRETO — Code-Behind mínimo
public partial class FuncionariosView : UserControl
{
    public FuncionariosView()
    {
        InitializeComponent();
    }
}
```

**Exceções permitidas no code-behind:**
- Foco inicial em um campo após carregamento
- Eventos de UI puros sem lógica de negócio (ex: scroll para topo)
- Interações que a API de Binding não suporta nativamente

---

## 5. Data Binding

### Modos de binding

| Mode | Quando usar |
|------|------------|
| `TwoWay` | Inputs editáveis (TextBox, ComboBox, CheckBox) |
| `OneWay` | Displays read-only (TextBlock, Labels) |
| `OneTime` | Dados que não mudam (enum lists, config) |

### UpdateSourceTrigger

```xml
<!-- ✅ Atualiza a cada tecla digitada -->
<TextBox Text="{Binding Nome, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>

<!-- ✅ Atualiza ao sair do campo -->
<TextBox Text="{Binding Cpf, Mode=TwoWay, UpdateSourceTrigger=LostFocus}"/>
```

---

## 6. Commands

### AsyncRelayCommand — para operações assíncronas

```csharp
// Declaração
public IAsyncRelayCommand SalvarCommand { get; }

// Inicialização
SalvarCommand = new AsyncRelayCommand(SalvarAsync, () => PodeExecutar);

// Implementação
private async Task SalvarAsync()
{
    try
    {
        _loadingService.Show("Salvando...");
        await _service.SalvarAsync(Dto);
        _alertService.ShowSuccess("Salvo com sucesso!");
    }
    catch (Exception ex)
    {
        _alertService.ShowError("Erro ao salvar.");
    }
    finally
    {
        _loadingService.Hide();
    }
}
```

### RelayCommand — para operações síncronas

```csharp
public IRelayCommand CancelarCommand { get; }
CancelarCommand = new RelayCommand(Cancelar);

private void Cancelar() => _navigator.Voltar();
```

---

## 7. Dependency Injection

Todas as dependências são registradas em `App.xaml.cs` e injetadas via construtor.

```csharp
// Registro em App.xaml.cs
services.AddScoped<IFuncionarioService, FuncionarioService>();
services.AddScoped<FuncionariosViewModel>();
services.AddTransient<FuncionariosView>();
```

**Nunca** instancie um service diretamente no ViewModel:
```csharp
// ❌ ERRADO
var service = new FuncionarioService();

// ✅ CORRETO
public MinhaViewModel(IFuncionarioService service) { _service = service; }
```

---

## 8. Serviços de UI

### LoadingService — Loading Overlay

```csharp
_loadingService.Show("Carregando...");   // ativar
_loadingService.Hide();                  // sempre no finally
```

### AlertService — Feedback ao usuário

```csharp
_alertService.ShowSuccess("Cadastro realizado com sucesso.");
_alertService.ShowError("Não foi possível carregar os dados.");
_alertService.ShowWarning("Existem itens com divergência.");
_alertService.ShowInfo("Os dados foram atualizados.");
```
