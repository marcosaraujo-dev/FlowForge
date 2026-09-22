# WPF — Componentes Específicos

Componentes reutilizáveis exclusivos do projeto. Cada um é um `UserControl` em `src/MinhaApp/Components/`.

---

## Índice

1. [LoadingOverlay](#1-loadingoverlay)
2. [LookupComboBox](#2-lookupcombobox)
3. [DataNotFound](#3-datanotfound)
4. [DialogWindow](#4-dialogwindow)
5. [GruposTrabalhadorPopup](#5-grupostrabalhadorpopup)
6. [BreadcrumbControl](#6-breadcrumbcontrol)

---

## 1. LoadingOverlay

**Arquivo:** `Components/LoadingOverlay.xaml`

Exibe um overlay escuro com spinner centralizado e mensagem de progresso. Cobre toda a área da tela enquanto uma operação assíncrona está em andamento.

### Como usar

O `LoadingOverlay` é controlado pelo `ILoadingService` — **nunca acione o componente diretamente**. O serviço cuida de exibir e ocultar o overlay automaticamente.

```csharp
// No ViewModel — uso correto via serviço
private async Task CarregarAsync()
{
    try
    {
        _loadingService.Show("Carregando dados...");
        var dados = await _service.ListarAsync();
        Itens = new ObservableCollection<MeuDto>(dados);
    }
    catch (Exception ex)
    {
        _alertService.ShowError("Não foi possível carregar.");
    }
    finally
    {
        _loadingService.Hide(); // ← SEMPRE no finally
    }
}
```

### Propriedades de dependência

| Propriedade | Tipo | Padrão | Descrição |
|-------------|------|--------|-----------|
| `Message` | `string` | `"Carregando..."` | Texto exibido abaixo do spinner |

### Regras de uso

- `Show()` sempre tem um `Hide()` no bloco `finally` — sem exceção.
- Não instanciar ou manipular o `LoadingOverlay` diretamente do code-behind ou ViewModel.
- Para atualizar a mensagem durante uma operação: `_loadingService.Show("Nova mensagem")`.
- O overlay fica posicionado como **último filho** do `Grid` raiz da View, garantindo que sobreponha todo o conteúdo.

```xml
<!-- Estrutura correta da View -->
<Grid>
    <ScrollViewer HorizontalScrollBarVisibility="Disabled">
        <!-- conteúdo da tela -->
    </ScrollViewer>

    <!-- LoadingOverlay registrado globalmente pelo serviço — não adicionar aqui manualmente -->
</Grid>
```

---

## 2. LookupComboBox

**Arquivo:** `Components/LookupComboBox.xaml`

ComboBox avançado com busca integrada. Exibe os itens em um popup com DataGrid filtrável por texto, permitindo localizar registros em listas longas (ex: 15+ empresas, 60+ trabalhadores).

### Quando usar

- Listas com mais de 10 itens onde busca por texto é útil
- Seleção de empresa, trabalhador ou qualquer entidade com código + nome

### Propriedades de dependência

| Propriedade | Tipo | Direção | Descrição |
|-------------|------|---------|-----------|
| `Header` | `string` | OneWay | Rótulo exibido acima do campo |
| `ItemsSource` | `IEnumerable` | OneWay | Coleção de itens |
| `SelectedItem` | `object` | TwoWay | Item atualmente selecionado |
| `SelectedValue` | `object` | TwoWay | Valor da propriedade `SelectedValuePath` do item selecionado |
| `SelectedValuePath` | `string` | OneWay | Nome da propriedade usada como valor (ex: `"Id"`, `"Codigo"`) |
| `IsDropDownOpen` | `bool` | TwoWay | Controla abertura do popup |

### Como usar

```xml
<components:LookupComboBox
    Header="Empresa"
    ItemsSource="{Binding Empresas}"
    SelectedItem="{Binding EmpresaSelecionada, Mode=TwoWay}"
    SelectedValuePath="CodigoEmpresa"
    SelectedValue="{Binding CodigoEmpresaSelecionada, Mode=TwoWay}"/>
```

```csharp
// No ViewModel
private EmpresaDto? _empresaSelecionada;
public EmpresaDto? EmpresaSelecionada
{
    get => _empresaSelecionada;
    set => SetProperty(ref _empresaSelecionada, value);
}

private string? _codigoEmpresaSelecionada;
public string? CodigoEmpresaSelecionada
{
    get => _codigoEmpresaSelecionada;
    set => SetProperty(ref _codigoEmpresaSelecionada, value);
}
```

### Comportamento do popup

- O popup abre ao clicar no campo de exibição
- Digitar no campo de busca filtra os itens em tempo real
- Clicar em uma linha ou pressionar `Enter` seleciona o item e fecha o popup
- Clicar fora do popup ou pressionar `Esc` cancela a seleção
- O botão "Limpar" limpa o filtro de busca (não a seleção)

### Colunas exibidas

O `LookupComboBox` atual exibe por padrão colunas específicas para `EmpresaDto` (`CodigoEmpresa`, `RazaoSocial`, `CnpjCpf`). Para adaptar a outros DTOs, consulte a equipe para criar uma variante configurável.

---

## 3. DataNotFound

**Arquivo:** `Components/DataNotFound.xaml`

Estado vazio padronizado para uso quando uma lista não tem resultados — após busca sem retorno, filtro restritivo, ou dados ainda não cadastrados.

### Quando usar

- DataGrid ou lista vazia após carregar
- Resultado de filtro/busca sem itens
- Substituir o espaço em branco por feedback visual ao usuário

### Propriedades de dependência

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| `Title` | `string` | Título em destaque (ex: `"Nenhum registro encontrado"`) |
| `Message` | `string` | Mensagem explicativa principal |
| `SubMessage` | `string` | Segunda linha opcional (ex: `"Tente ajustar os filtros"`) |

### Como usar

```xml
<!-- Em um DataGrid com estado vazio -->
<DataGrid ItemsSource="{Binding Itens}" ...>
    <!-- ... colunas ... -->
</DataGrid>

<!-- Visível apenas quando lista está vazia -->
<components:DataNotFound
    Visibility="{Binding Itens.Count, Converter={StaticResource CountToVisibility}}"
    Title="Nenhum trabalhador encontrado"
    Message="Não há trabalhadores para os filtros selecionados."
    SubMessage="Tente ajustar a empresa ou o período."/>
```

```csharp
// No ViewModel — alternativa via property calculada
public bool ListaVazia => Itens == null || Itens.Count == 0;
```

```xml
<!-- Usando property calculada -->
<components:DataNotFound
    Visibility="{Binding ListaVazia, Converter={StaticResource BoolToVisibility}}"
    Title="Nenhum resultado"
    Message="Nenhum item foi encontrado."/>
```

### Visual

O componente exibe:
1. Ícone de lupa (Segoe MDL2 `Icon.Search`) em cinza
2. `Title` em negrito, centralizado
3. `Message` em texto secundário
4. `SubMessage` (opcional) em texto secundário, abaixo

---

## 4. DialogWindow

**Arquivo:** `Components/DialogWindow.xaml`

Janela modal padronizada para confirmações, formulários e informações. Usa o estilo visual do Design System com título, área de conteúdo e rodapé de ações.

### Quando usar

- Confirmação antes de ação destrutiva (excluir, cancelar)
- Formulário secundário sem criar uma View completa
- Exibir informações detalhadas de um registro

### Como usar

```csharp
// Via DialogService — uso recomendado no ViewModel
var resultado = await _dialogService.ShowConfirmationAsync(
    titulo: "Excluir registro",
    mensagem: "Tem certeza que deseja excluir este item? Esta ação não pode ser desfeita.",
    confirmLabel: "Excluir",
    cancelLabel: "Cancelar");

if (resultado == true)
{
    await ExcluirAsync();
}
```

```csharp
// Uso direto com conteúdo customizado (para formulários)
var dialog = new DialogWindow();
dialog.Title = "Editar configuração";

var viewModel = _serviceProvider.GetService<EditarConfiguracaoViewModel>();
var view = new EditarConfiguracaoView(viewModel);
dialog.SetContent(view);

dialog.ShowDialog();
```

### Tamanhos disponíveis

| Uso | Largura recomendada |
|-----|---------------------|
| Confirmação simples | 400px |
| Formulário pequeno | 560px |
| Formulário complexo | 720px |

### Regras de uso

- Sempre usar `ShowDialog()` (modal bloqueante) — nunca `Show()` em dialogs de confirmação.
- Botões de ação ficam no rodapé: confirmar à direita, cancelar à esquerda.
- Botão de ação destrutiva (excluir) usa `Style="{StaticResource MediumDangerButton}"`.
- Nunca colocar dois dialogs abertos simultaneamente.

---

## 5. GruposTrabalhadorPopup

**Arquivo:** `Components/GruposTrabalhadorPopup.xaml`

Popup que exibe os grupos/vínculos de um trabalhador. Aparece ao clicar em um indicador de grupos na lista de trabalhadores.

### Como usar

```csharp
// No code-behind ou via evento de clique
private void BtnGrupos_Click(object sender, RoutedEventArgs e)
{
    var popup = new Popup { AllowsTransparency = true, StaysOpen = false };
    var content = new GruposTrabalhadorPopup();
    content.SetParentPopup(popup);
    content.CarregarDados(
        nomeTrabalhador: trabalhador.Nome,
        cpf: trabalhador.Cpf,
        grupos: trabalhador.Grupos.Cast<object>().ToList());

    popup.Child = content;
    popup.PlacementTarget = (UIElement)sender;
    popup.Placement = PlacementMode.Bottom;
    popup.IsOpen = true;
}
```

### Métodos públicos

| Método | Descrição |
|--------|-----------|
| `SetParentPopup(Popup)` | Registra o popup pai para que o botão "Fechar" interno possa fechá-lo |
| `CarregarDados(nome, cpf, grupos)` | Preenche os dados exibidos no popup |

### Notas de implementação

- O componente exibe nome, CPF e uma tabela de grupos do trabalhador.
- O `GrupoTrabalhadorDto` está pendente de criação — atualmente recebe `List<object>`.
- O popup fecha ao clicar no botão "Fechar" ou ao clicar fora (`StaysOpen=False`).

---

## 6. BreadcrumbControl

**Arquivo:** `Controls/BreadcrumbControl.xaml`

Exibe o caminho de navegação no header global da aplicação (MainWindow). Permite ao usuário entender onde está e retornar a níveis anteriores com um clique.

> Documentação completa: [02-Componentes/14-Breadcrumb.md](../02-Componentes/14-Breadcrumb.md)

### Quando usar

- Definir no `MainViewModel` ao navegar para qualquer tela
- Sempre atualizar em conjunto com `PageTitle`
- Nunca usar dentro de dialogs ou wizards

### Propriedades de dependência

| Propriedade | Tipo | Descrição |
|-------------|------|-----------|
| `Path` | `string` | Caminho no formato `"Home > Categoria > Item"` |
| `Separator` | `string` | Separador entre itens (padrão: `" > "`) |
| `NavigateCommand` | `ICommand` | Executado ao clicar em item — recebe a `Key` do item |
| `Items` | `ObservableCollection<BreadcrumbItem>` | Alternativa ao Path para controle total de keys |

### Como usar — Modo Path (mais comum)

```xml
<!-- MainWindow.xaml -->
<controls:BreadcrumbControl
    Path="{Binding Breadcrumb}"
    NavigateCommand="{Binding NavigateToCommand}"
    HorizontalAlignment="Center"
    VerticalAlignment="Center"/>
```

```csharp
// MainViewModel — definir ao navegar
PageTitle = $"Editar: {template.Nome}";
Breadcrumb = $"Home > Meus Templates > Editar: {template.Nome}";
```

### Como usar — Modo Items (controle total)

```csharp
BreadcrumbItems = new ObservableCollection<BreadcrumbItem>
{
    new BreadcrumbItem { Text = "Home",           Key = "home" },
    new BreadcrumbItem { Text = "Meus Templates", Key = "templates" },
    new BreadcrumbItem { Text = "Editar: Acordo", Key = "" } // Key vazia = não navega
};
```

### Padrão de atualização obrigatório

Todo método de navegação no `MainViewModel` deve definir **ambos** `PageTitle` e `Breadcrumb`:

```csharp
private async Task NavigarParaEditorAsync(Template template)
{
    await _loadingService.ExecuteAsync(async () =>
    {
        CurrentView = _factory.CreateEditorViewModel(this, template);
        PageTitle = $"Editar: {template.Nome}";
        Breadcrumb = $"Home > Meus Templates > Editar: {template.Nome}";
        SelectedMenuItem = null;
    }, "Carregando Editor...");
}
```

### Hierarquia de caminhos do sistema

| Tela | Breadcrumb |
|------|-----------|
| Home | `"Home"` |
| Documentos Word | `"Home > Documentos Word"` |
| Meus Templates | `"Home > Meus Templates"` |
| Editor de Template | `"Home > Meus Templates > Editar: {Nome}"` |
| Preview (do editor) | `"Home > Meus Templates > Editor > Preview"` |
| Gerar Documentos | `"Home > Gerar Documentos"` |
| Resultado da Geração | `"Home > Gerar Documentos > Resultado"` |
