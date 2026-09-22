# WPF — Anti-Patterns

O que **nunca** fazer. Cada anti-pattern inclui o código problemático, por que é ruim e a solução correta.

---

## Índice por categoria

**Code-Behind:**
- [#1 — Lógica de negócio no Code-Behind](#1--lógica-de-negócio-no-code-behind)
- [#2 — Manipulação direta de controles](#2--manipulação-direta-de-controles)

**ViewModel:**
- [#3 — Instanciar Services no ViewModel](#3--instanciar-services-no-viewmodel)
- [#4 — Async sem try-finally](#4--async-sem-try-finally)
- [#5 — Property sem SetProperty](#5--property-sem-setproperty)

**XAML:**
- [#6 — Hardcode de cores e tamanhos](#6--hardcode-de-cores-e-tamanhos)
- [#7 — Binding sem Mode e UpdateSourceTrigger](#7--binding-sem-mode-e-updateSourcetrigger)
- [#8 — ScrollViewer sem HorizontalScrollBarVisibility](#8--scrollviewer-sem-horizontalscrollbarvisibility)

**Layout:**
- [#9 — ComboBox com MinWidth/MaxWidth](#9--combobox-com-minwidthmaxwidth)
- [#10 — Estilo de DataGrid inline](#10--estilo-de-datagrid-inline)

**UX:**
- [#11 — Botão de ícone sem ToolTip](#11--botão-de-ícone-sem-tooltip)
- [#12 — Loading sem finally](#12--loading-sem-finally)

**Acessibilidade:**
- [#13 — Height fixo em container com texto](#13--height-fixo-em-container-com-texto)

---

## #1 — Lógica de negócio no Code-Behind

```csharp
// ❌ NUNCA FAÇA ISSO
private void ProcessarButton_Click(object sender, RoutedEventArgs e)
{
    var competencia = CompetenciaTextBox.Text;
    var service = new RegistroService();           // ❌ new direto
    var resultado = service.Processar(competencia); // ❌ lógica no code-behind
    ResultadosGrid.ItemsSource = resultado;         // ❌ acesso direto ao controle
}
```

**Por que é ruim:** Impossível testar, acopla UI com negócio, viola DI, viola MVVM.

```csharp
// ✅ CORRETO — code-behind mínimo
public partial class MinhaView : UserControl
{
    public MinhaView(MinhaViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
```

```xml
<!-- ✅ Binding no XAML -->
<Button Command="{Binding ProcessarCommand}" Content="Processar"/>
<DataGrid ItemsSource="{Binding Resultados}"/>
```

---

## #2 — Manipulação direta de controles

```csharp
// ❌ NUNCA FAÇA ISSO
NomeTextBlock.Text = resultado.Nome;
ErroPanel.Visibility = resultado.TemErro ? Visibility.Visible : Visibility.Collapsed;
```

**Por que é ruim:** Código não testável, quebra ao renomear controles, viola MVVM.

```csharp
// ✅ CORRETO — propriedades no ViewModel
public string Nome => _resultado?.Nome;
public bool TemErro => _resultado?.TemErro ?? false;
```

```xml
<!-- ✅ Visibility via Binding + Converter -->
<StackPanel Visibility="{Binding TemErro, Converter={StaticResource BoolToVisibility}}">
    <TextBlock Text="{Binding Nome}"/>
</StackPanel>
```

---

## #3 — Instanciar Services no ViewModel

```csharp
// ❌ NUNCA FAÇA ISSO
public class MinhaViewModel : ViewModelBase
{
    private readonly FuncionarioService _service = new FuncionarioService(); // ❌
}
```

**Por que é ruim:** Derrota o DI, impossível mockar em testes, cria dependência direta de implementação.

```csharp
// ✅ CORRETO — injeção via construtor
public class MinhaViewModel : ViewModelBase
{
    private readonly IFuncionarioService _service;

    public MinhaViewModel(IFuncionarioService service) // ← injetado pelo DI
    {
        _service = service;
    }
}
```

---

## #4 — Async sem try-finally

```csharp
// ❌ ERRADO — loading pode ficar preso para sempre
private async Task CarregarAsync()
{
    _loadingService.Show();
    var dados = await _service.ListarAsync(); // ← se lançar exceção...
    _loadingService.Hide();                   // ← nunca executa
}
```

**Por que é ruim:** Um erro de rede deixa o loading travado e a UI inutilizável.

```csharp
// ✅ CORRETO
private async Task CarregarAsync()
{
    try
    {
        _loadingService.Show("Carregando...");
        Funcionarios = new ObservableCollection<FuncionarioDto>(
            await _service.ListarAsync());
    }
    catch (Exception ex)
    {
        _alertService.ShowError("Não foi possível carregar os dados.");
        _logger.Error(ex, "Erro ao carregar");
    }
    finally
    {
        _loadingService.Hide(); // ← SEMPRE executa, mesmo com exceção
    }
}
```

---

## #5 — Property sem SetProperty

```csharp
// ❌ ERRADO — a View nunca vai atualizar
public string Nome { get; set; }

// ❌ ERRADO — notificação manual esquecida
public string Nome
{
    get => _nome;
    set { _nome = value; } // ← não notifica o binding
}
```

```csharp
// ✅ CORRETO
private string _nome;
public string Nome
{
    get => _nome;
    set => SetProperty(ref _nome, value); // notifica automaticamente
}
```

---

## #6 — Hardcode de cores e tamanhos

```xml
<!-- ❌ NUNCA FAÇA ISSO -->
<Button Background="#184194"
        Height="34"
        Padding="16,8"
        FontSize="14"
        CornerRadius="6"/>

<TextBlock Foreground="#495057"
           FontSize="24"
           FontWeight="SemiBold"/>
```

**Por que é ruim:** Quando o Design System mudar (nova cor primary, novo tamanho), você terá que alterar cada XAML manualmente.

```xml
<!-- ✅ CORRETO — tokens -->
<Button Style="{StaticResource MediumPrimaryButton}"/>

<TextBlock Style="{StaticResource PageTitle}" Text="Título da Página"/>

<Border BorderBrush="{StaticResource BorderColor}"
        BorderThickness="{StaticResource BorderThickness.Thin}"
        CornerRadius="{StaticResource CornerRadius.Large}"
        Padding="{StaticResource Padding.Card}"/>
```

---

## #7 — Binding sem Mode e UpdateSourceTrigger

```xml
<!-- ❌ ERRADO — comportamento indefinido -->
<TextBox Text="{Binding Nome}"/>
<ComboBox SelectedItem="{Binding Empresa}"/>
```

**Por que é ruim:** O WPF pode usar `OneWay` ou `TwoWay` dependendo do controle, e o `UpdateSourceTrigger` padrão de `TextBox` é `LostFocus` — dados só salvam ao sair do campo.

```xml
<!-- ✅ CORRETO — explícito -->
<TextBox Text="{Binding Nome, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>

<ComboBox SelectedItem="{Binding EmpresaSelecionada, Mode=TwoWay}"
          TextSearch.TextPath="Nome"/>

<!-- Campos de busca/filtro com LostFocus é aceitável -->
<TextBox Text="{Binding Filtro, Mode=TwoWay, UpdateSourceTrigger=LostFocus}"/>
```

---

## #8 — ScrollViewer sem HorizontalScrollBarVisibility

```xml
<!-- ❌ ERRADO — ComboBox vai expandir indefinidamente -->
<ScrollViewer>
    <StackPanel>
        <ComboBox HorizontalAlignment="Stretch" .../>
    </StackPanel>
</ScrollViewer>
```

**Por que é ruim:** O ScrollViewer com scroll horizontal ativo calcula largura "infinita", fazendo o ComboBox expandir além do visível.

```xml
<!-- ✅ CORRETO -->
<ScrollViewer HorizontalScrollBarVisibility="Disabled">
    <StackPanel>
        <ComboBox HorizontalAlignment="Stretch" .../>
    </StackPanel>
</ScrollViewer>
```

---

## #9 — ComboBox com MinWidth/MaxWidth

```xml
<!-- ❌ ERRADO -->
<ComboBox MinWidth="200" MaxWidth="400" .../>
```

**Por que é ruim:** Interfere no layout responsivo — o ComboBox não se adapta ao espaço disponível.

```xml
<!-- ✅ CORRETO -->
<ComboBox HorizontalAlignment="Stretch"
          Style="{StaticResource RoundedComboBox}"
          SelectedItem="{Binding Item, Mode=TwoWay}"
          ItemsSource="{Binding Itens}"
          TextSearch.TextPath="Nome"/>
```

---

## #10 — Estilo de DataGrid inline

```xml
<!-- ❌ ERRADO — estilos de row definidos inline -->
<DataGrid>
    <DataGrid.RowStyle>
        <Style TargetType="DataGridRow">
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="Background" Value="#F5F8FF"/>
                </Trigger>
            </Style.Triggers>
        </Style>
    </DataGrid.RowStyle>
</DataGrid>
```

**Por que é ruim:** Duplicado em cada DataGrid, não usa os tokens e não atualiza se o Design System mudar.

```xml
<!-- ✅ CORRETO — usar o estilo global -->
<DataGrid RowStyle="{StaticResource DefaultDataGridRow}"
          Style="{StaticResource DefaultDataGrid}"/>
```

---

## #11 — Botão de ícone sem ToolTip

```xml
<!-- ❌ ERRADO — usuário não sabe o que o botão faz -->
<Button Content="&#xE70F;" Style="{StaticResource GridIconButton}"/>
```

```xml
<!-- ✅ CORRETO — sempre com ToolTip em botões de ícone -->
<Button Content="&#xE70F;"
        Style="{StaticResource GridIconButton}">
    <Button.ToolTip>
        <ToolTip>
            <StackPanel>
                <TextBlock FontWeight="SemiBold" Text="Editar"/>
                <TextBlock Text="Editar os dados deste registro"/>
            </StackPanel>
        </ToolTip>
    </Button.ToolTip>
</Button>
```

---

## #12 — Loading sem finally

Ver [#4](#4--async-sem-try-finally) — o mesmo problema se aplica a qualquer uso de `LoadingService`.

**Regra:** `Show()` sempre tem um `Hide()` correspondente no bloco `finally`. Sem exceção.

---

## #13 — Height fixo em container com texto

```xml
<!-- ❌ NUNCA FAÇA ISSO em containers que envolvem texto -->
<Button MinHeight="120" Height="120">
    <StackPanel>
        <TextBlock Text="{Binding Title}" FontSize="14"/>
        <TextBlock Text="{Binding Description}" FontSize="12"/>
    </StackPanel>
</Button>

<Border Height="80">
    <TextBlock Text="{Binding Label}" TextWrapping="Wrap"/>
</Border>
```

**Por que é ruim:** A aplicação usa `LayoutTransform` com `ScaleTransform` para escala de fonte (F2 — Normal / Grande / ExtraGrande). Uma `Height` fixa não cresce com a transformação — o texto fica cortado ou sobreposto e o usuário com baixa visão não consegue ler o conteúdo.

```xml
<!-- ✅ CORRETO — MinHeight permite crescimento -->
<Button MinHeight="120">
    <StackPanel>
        <TextBlock Text="{Binding Title}" FontSize="14" TextWrapping="Wrap"/>
        <TextBlock Text="{Binding Description}" FontSize="12" TextWrapping="Wrap"/>
    </StackPanel>
</Button>

<Border MinHeight="80">
    <TextBlock Text="{Binding Label}" TextWrapping="Wrap"/>
</Border>
```

**Regra:**
- `Height` → **proibido** em qualquer elemento que contenha texto direta ou indiretamente
- `MinHeight` → define o tamanho mínimo, mas o container cresce com o conteúdo
- `TextWrapping="Wrap"` → obrigatório nos `TextBlock` dentro de containers com largura limitada

**Exceções válidas:** Containers sem conteúdo textual (ícones fixos, separadores, imagens com dimensão fixa).

---

## #14 — await em I/O síncrono mascarado como Task

Bibliotecas legadas ou drivers nativos (Btrieve, COM, ODBC wrappers) frequentemente expõem
métodos `async Task` que internamente executam I/O síncrono via `Task.FromResult(...)`.

```csharp
// ❌ ERRADO — parece assíncrono, mas bloqueia a thread de UI
private async Task CarregarAsync()
{
    _loadingService.Mostrar("Carregando...");
    await Dispatcher.InvokeAsync(() => { }, DispatcherPriority.Background);

    var resultado = await _legadoRepository.ListarAsync(); // ← síncrono disfarçado!
    // UI travada aqui — overlay congelado, Windows exibe cursor "não responde"
}
```

**Por que é ruim:** O `await` de um `Task.FromResult(...)` não cede a thread de UI.
A chamada roda inteiramente na thread de UI, bloqueando-a. O overlay de loading fica
congelado ou nem aparece; o OS detecta o processo sem resposta e exibe o cursor de espera
do Windows (o "gif pensando") por cima da aplicação.

```csharp
// ✅ CORRETO — Task.Run empurra o I/O para o thread pool
private async Task CarregarAsync()
{
    _loadingService.Mostrar("Carregando...");
    await Dispatcher.InvokeAsync(() => { }, DispatcherPriority.Background); // ← overlay renderiza

    var param = MinhaPropriedade;                              // captura local antes do Task.Run
    var resultado = await Task.Run(() => _legadoRepository.ListarAsync());
    // continuação retorna automaticamente para a UI thread (SynchronizationContext WPF)

    Itens = new ObservableCollection<ItemDto>(resultado.Dados); // seguro — UI thread
    OnPropertyChanged(nameof(Itens));
}
```

**Regra de captura de parâmetros:** Propriedades do ViewModel acessadas dentro do
`Task.Run` devem ser capturadas como variáveis locais antes da chamada para evitar
acesso cross-thread:

```csharp
// ❌ ERRADO — acessa this.CodigoEmpresa de outra thread
await Task.Run(() => _repo.ListarPorEmpresaAsync(CodigoEmpresa));

// ✅ CORRETO — captura antes de mudar de thread
var codigoEmpresa = CodigoEmpresa;
await Task.Run(() => _repo.ListarPorEmpresaAsync(codigoEmpresa));
```

**Como identificar no code review:**
- Repositório ou service retorna `Task<T>` mas está em uma camada legada/nativa (Btrieve, COM, ODBC)
- `await _xxxRepository.XxxAsync()` direto no ViewModel sem `Task.Run`
- Usuário reporta "tela trava", "loading não aparece" ou "cursor gif pensando do Windows"

---

## Resumo rápido

| # | Anti-Pattern | Impacto |
|---|-------------|---------|
| 1 | Lógica no code-behind | Não testável, acopla UI |
| 2 | Acesso direto a controles | Não testável, quebra ao renomear |
| 3 | `new Service()` no ViewModel | Derrota DI, não mockável |
| 4 | Async sem try-finally | UI trava com loading preso |
| 5 | Property sem SetProperty | View nunca atualiza |
| 6 | Hardcode de cores/tamanhos | Inviabiliza mudanças no DS |
| 7 | Binding implícito | Comportamento indefinido |
| 8 | ScrollViewer horizontal ativo | ComboBox expande infinito |
| 9 | MinWidth/MaxWidth em ComboBox | Layout não responsivo |
| 10 | Estilo DataGrid inline | Duplicação, não usa tokens |
| 11 | Ícone sem ToolTip | UX ruim, usuário perdido |
| 12 | Loading sem finally | UI trava |
| 13 | `Height` fixo em container com texto | Texto cortado com zoom de acessibilidade |
| 14 | `await` em I/O síncrono mascarado como Task | UI congela, overlay não aparece |
