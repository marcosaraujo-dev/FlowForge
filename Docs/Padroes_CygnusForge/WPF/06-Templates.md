# WPF — Templates de Código

Templates prontos para criar novos arquivos. Copie, substitua os nomes e comece a implementar.

---

## Índice

1. [Template de View](#1-template-de-view)
2. [Template de ViewModel](#2-template-de-viewmodel)
3. [Template de ViewModel com lista e seleção](#3-template-de-viewmodel-com-lista-e-seleção)
4. [Template de Converter](#4-template-de-converter)
5. [Template de Dialog (modal)](#5-template-de-dialog-modal)
6. [Checklist ao criar arquivo novo](#6-checklist-ao-criar-arquivo-novo)

---

## 1. Template de View

### MinhaView.xaml

```xml
<UserControl x:Class="MinhaApp.Views.MinhaView"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:i="http://schemas.microsoft.com/xaml/behaviors">

    <Grid>
        <!-- Conteúdo principal -->
        <ScrollViewer HorizontalScrollBarVisibility="Disabled">
            <StackPanel Margin="{StaticResource Margin.Container}">

                <!-- Cabeçalho da página -->
                <Border Style="{StaticResource PageTitleContainer}">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>

                        <TextBlock Grid.Column="0"
                                   Style="{StaticResource PageTitle}"
                                   Text="Título da Página"/>

                        <!-- Ação principal do cabeçalho (opcional) -->
                        <Button Grid.Column="1"
                                Command="{Binding AcaoPrincipalCommand}"
                                Content="Nova Ação"
                                Style="{StaticResource MediumPrimaryButton}"/>
                    </Grid>
                </Border>

                <!-- Área de conteúdo -->
                <Border Style="{StaticResource CardContainer}">
                    <!-- Conteúdo do card aqui -->
                </Border>

            </StackPanel>
        </ScrollViewer>

        <!-- Loading overlay — sempre o último filho do Grid raiz -->
        <!-- Gerenciado pelo LoadingService — não precisa de binding manual -->
    </Grid>

    <!-- Carregar dados ao abrir (opção A — via code-behind) -->
    <!-- Veja MinhaView.xaml.cs -->

    <!-- Carregar dados ao abrir (opção B — via Interaction.Triggers) -->
    <!--
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Loaded">
            <i:InvokeCommandAction Command="{Binding CarregarCommand}"/>
        </i:EventTrigger>
    </i:Interaction.Triggers>
    -->
</UserControl>
```

### MinhaView.xaml.cs

```csharp
namespace MinhaApp.Views
{
    public partial class MinhaView : UserControl
    {
        public MinhaView(MinhaViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            // Opção A: carregar dados via evento Loaded
            Loaded += async (s, e) => await viewModel.InicializarAsync();
        }
    }
}
```

---

## 2. Template de ViewModel

Template mínimo para uma tela simples (sem lista/DataGrid).

```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinhaApp.Services;
using MinhaApp.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace MinhaApp.ViewModels
{
    public sealed class MinhaViewModel : ViewModelBase
    {
        private readonly IMeuService _service;
        private readonly IAlertService _alertService;
        private readonly ILoadingService _loadingService;

        // ── Properties observáveis ─────────────────────────────────────────

        private string _titulo = string.Empty;
        public string Titulo
        {
            get => _titulo;
            set => SetProperty(ref _titulo, value);
        }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        // ── Commands ──────────────────────────────────────────────────────

        public IAsyncRelayCommand InicializarCommand { get; }
        public IAsyncRelayCommand SalvarCommand { get; }
        public IRelayCommand CancelarCommand { get; }

        // ── Construtor ────────────────────────────────────────────────────

        public MinhaViewModel(
            IMeuService service,
            IAlertService alertService,
            ILoadingService loadingService)
        {
            _service = service;
            _alertService = alertService;
            _loadingService = loadingService;

            InicializarCommand = new AsyncRelayCommand(InicializarAsync);
            SalvarCommand = new AsyncRelayCommand(SalvarAsync, () => PodeSalvar);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        // ── Properties calculadas ─────────────────────────────────────────

        public bool PodeSalvar => !string.IsNullOrWhiteSpace(Titulo);

        // ── Implementação dos Commands ─────────────────────────────────────

        public async Task InicializarAsync()
        {
            try
            {
                _loadingService.Show("Carregando...");
                var dados = await _service.ObterAsync();
                Titulo = dados.Titulo;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Erro ao inicializar MinhaViewModel");
                _alertService.ShowError("Não foi possível carregar os dados.");
            }
            finally
            {
                _loadingService.Hide();
            }
        }

        private async Task SalvarAsync()
        {
            try
            {
                _loadingService.Show("Salvando...");
                await _service.SalvarAsync(Titulo);
                _alertService.ShowSuccess("Salvo com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Erro ao salvar");
                _alertService.ShowError("Não foi possível salvar.");
            }
            finally
            {
                _loadingService.Hide();
            }
        }

        private void Cancelar()
        {
            // Navegar de volta ou fechar dialog
        }
    }
}
```

---

## 3. Template de ViewModel com lista e seleção

Para telas com DataGrid, filtro e ações por linha.

```csharp
using System.Collections.ObjectModel;

public sealed class MinhaListaViewModel : ViewModelBase
{
    private readonly IMeuService _service;
    private readonly IAlertService _alertService;
    private readonly ILoadingService _loadingService;

    // ── Lista e seleção ───────────────────────────────────────────────────

    private ObservableCollection<MeuDto> _itens = new();
    public ObservableCollection<MeuDto> Itens
    {
        get => _itens;
        set => SetProperty(ref _itens, value);
    }

    private MeuDto? _itemSelecionado;
    public MeuDto? ItemSelecionado
    {
        get => _itemSelecionado;
        set
        {
            SetProperty(ref _itemSelecionado, value);
            OnPropertyChanged(nameof(TemSelecionado));
            EditarCommand.NotifyCanExecuteChanged();
            ExcluirCommand.NotifyCanExecuteChanged();
        }
    }

    public bool TemSelecionado => ItemSelecionado != null;

    // ── Filtro ────────────────────────────────────────────────────────────

    private string _filtro = string.Empty;
    public string Filtro
    {
        get => _filtro;
        set
        {
            SetProperty(ref _filtro, value);
            _ = CarregarAsync(); // recarregar ao filtrar
        }
    }

    // ── Commands ──────────────────────────────────────────────────────────

    public IAsyncRelayCommand CarregarCommand { get; }
    public IRelayCommand<MeuDto> EditarCommand { get; }
    public IAsyncRelayCommand<MeuDto> ExcluirCommand { get; }
    public IRelayCommand NovoCommand { get; }

    public MinhaListaViewModel(
        IMeuService service,
        IAlertService alertService,
        ILoadingService loadingService)
    {
        _service = service;
        _alertService = alertService;
        _loadingService = loadingService;

        CarregarCommand = new AsyncRelayCommand(CarregarAsync);
        EditarCommand = new RelayCommand<MeuDto>(Editar, dto => dto != null);
        ExcluirCommand = new AsyncRelayCommand<MeuDto>(ExcluirAsync, dto => dto != null);
        NovoCommand = new RelayCommand(Novo);
    }

    public async Task InicializarAsync() => await CarregarAsync();

    private async Task CarregarAsync()
    {
        try
        {
            _loadingService.Show("Carregando...");
            var lista = await _service.ListarAsync(Filtro);
            Itens = new ObservableCollection<MeuDto>(lista);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao carregar lista");
            _alertService.ShowError("Não foi possível carregar os dados.");
        }
        finally
        {
            _loadingService.Hide();
        }
    }

    private void Editar(MeuDto? dto)
    {
        if (dto == null) return;
        // _navigator.NavigateTo<EditarViewModel>(dto.Id);
    }

    private async Task ExcluirAsync(MeuDto? dto)
    {
        if (dto == null) return;

        // TODO: confirmar com dialog antes de excluir
        try
        {
            _loadingService.Show("Excluindo...");
            await _service.ExcluirAsync(dto.Id);
            Itens.Remove(dto);
            _alertService.ShowSuccess("Excluído com sucesso!");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Erro ao excluir item {Id}", dto.Id);
            _alertService.ShowError("Não foi possível excluir.");
        }
        finally
        {
            _loadingService.Hide();
        }
    }

    private void Novo()
    {
        // _navigator.NavigateTo<NovoViewModel>();
    }
}
```

### View correspondente (DataGrid)

```xml
<!-- Filtro + ação -->
<Grid Margin="{StaticResource Margin.BetweenCards}">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="Auto"/>
    </Grid.ColumnDefinitions>

    <TextBox Grid.Column="0"
             Text="{Binding Filtro, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
             Style="{StaticResource RoundedTextBox}"
             Tag="Buscar..."/>

    <Button Grid.Column="1"
            Command="{Binding NovoCommand}"
            Content="Novo"
            Style="{StaticResource MediumPrimaryButton}"
            Margin="{StaticResource Margin.BetweenCards}"/>
</Grid>

<!-- DataGrid -->
<DataGrid ItemsSource="{Binding Itens}"
          SelectedItem="{Binding ItemSelecionado, Mode=TwoWay}"
          Style="{StaticResource DefaultDataGrid}"
          RowStyle="{StaticResource DefaultDataGridRow}"
          AutoGenerateColumns="False">
    <DataGrid.Columns>
        <DataGridTextColumn Header="Nome"
                            Binding="{Binding Nome}"
                            Width="*"/>
        <DataGridTextColumn Header="Status"
                            Binding="{Binding Status}"
                            Width="Auto"/>
        <!-- Coluna de ações -->
        <DataGridTemplateColumn Header="" Width="80" CanUserSort="False">
            <DataGridTemplateColumn.CellTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
                        <Button Command="{Binding DataContext.EditarCommand,
                                         RelativeSource={RelativeSource AncestorType=DataGrid}}"
                                CommandParameter="{Binding}"
                                Content="&#xE70F;"
                                Style="{StaticResource GridIconButton}">
                            <Button.ToolTip>
                                <ToolTip>
                                    <StackPanel>
                                        <TextBlock FontWeight="SemiBold" Text="Editar"/>
                                        <TextBlock Text="Editar este registro"/>
                                    </StackPanel>
                                </ToolTip>
                            </Button.ToolTip>
                        </Button>
                        <Button Command="{Binding DataContext.ExcluirCommand,
                                         RelativeSource={RelativeSource AncestorType=DataGrid}}"
                                CommandParameter="{Binding}"
                                Content="&#xE74D;"
                                Style="{StaticResource GridIconButtonDanger}">
                            <Button.ToolTip>
                                <ToolTip>
                                    <StackPanel>
                                        <TextBlock FontWeight="SemiBold" Text="Excluir"/>
                                        <TextBlock Text="Excluir este registro permanentemente"/>
                                    </StackPanel>
                                </ToolTip>
                            </Button.ToolTip>
                        </Button>
                    </StackPanel>
                </DataTemplate>
            </DataGridTemplateColumn.CellTemplate>
        </DataGridTemplateColumn>
    </DataGrid.Columns>
</DataGrid>
```

---

## 4. Template de Converter

```csharp
using System;
using System.Globalization;
using System.Windows.Data;

namespace MinhaApp.Converters
{
    /// <summary>
    /// Converte [TipoEntrada] para [TipoSaida].
    /// Exemplo: StatusPedido → SolidColorBrush (cor do badge)
    /// </summary>
    public class NomeDoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TipoEsperado dado)
            {
                // lógica de transformação
                return dado switch
                {
                    TipoEsperado.OpcaoA => ValorVisualA,
                    TipoEsperado.OpcaoB => ValorVisualB,
                    _ => ValorPadrao
                };
            }

            return ValorPadrao; // nunca retornar null se possível
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // One-way: remover esta linha e a exceção se for two-way
            throw new NotImplementedException("NomeDoConverter é one-way apenas.");
        }
    }
}
```

**Após criar, registrar em `App.xaml`:**

```xml
<converters:NomeDoConverter x:Key="NomeDoConverter"/>
```

---

## 5. Template de Dialog (modal)

### DialogWindow.xaml.cs (já existente no projeto)

```csharp
// O projeto já possui um DialogWindow genérico em Components/
// Para usar:
var dialog = new DialogWindow();
dialog.Title = "Confirmar exclusão";
dialog.SetContent(new MinhaConfirmacaoView());
dialog.ShowDialog();
```

### ViewModel de confirmação simples

```csharp
public sealed class ConfirmacaoViewModel : ViewModelBase
{
    public string Mensagem { get; }
    public bool Confirmado { get; private set; }

    public IRelayCommand ConfirmarCommand { get; }
    public IRelayCommand CancelarCommand { get; }

    public ConfirmacaoViewModel(string mensagem)
    {
        Mensagem = mensagem;
        ConfirmarCommand = new RelayCommand(() => { Confirmado = true; /* fechar */ });
        CancelarCommand = new RelayCommand(() => { Confirmado = false; /* fechar */ });
    }
}
```

---

## 6. Checklist ao criar arquivo novo

### Nova View + ViewModel

- [ ] Arquivo nomeado `[Nome]View.xaml` e `[Nome]ViewModel.cs`
- [ ] View herda de `UserControl` (não `Window`)
- [ ] Code-behind: apenas `InitializeComponent()` e DataContext
- [ ] ViewModel herda de `ViewModelBase`
- [ ] Registrar ambos em `App.xaml.cs` (DI)
- [ ] Todas as properties usam `SetProperty()`
- [ ] Todos os commands são `IAsyncRelayCommand` ou `IRelayCommand`
- [ ] Operações async têm `try-catch-finally` com `LoadingService`
- [ ] `ScrollViewer` tem `HorizontalScrollBarVisibility="Disabled"`
- [ ] Nenhum valor hardcoded de cor, fonte ou tamanho

### Novo Converter

- [ ] Nome no formato `[Entrada]Para[Saida]Converter` (ex: `BoolToVisibilityConverter`)
- [ ] Cast seguro com `value is Tipo x`
- [ ] Valor de retorno padrão em todos os caminhos
- [ ] Registrado em `App.xaml` com `x:Key`
- [ ] Documentado com `/// <summary>`
