# WPF — Padrões Obrigatórios

Regras que todo código WPF submetido para revisão deve seguir. Itens marcados com ⚠️ são bloqueadores de PR.

---

## Checklist de PR

### ⚠️ Code-Behind

- [ ] Nenhuma lógica de negócio no Code-Behind
- [ ] Nenhuma chamada a Services no Code-Behind
- [ ] Code-Behind tem no máximo 10–15 linhas
- [ ] Event handlers apenas delegam para Commands

### ⚠️ ViewModel

- [ ] Herda de `ViewModelBase`
- [ ] Propriedades usam `SetProperty()` para notificação
- [ ] Commands são `IAsyncRelayCommand` ou `IRelayCommand` do CommunityToolkit.Mvvm
- [ ] `ObservableCollection<T>` para todas as listas dinâmicas
- [ ] Nenhum campo público (tudo via Properties)
- [ ] Backing fields com prefixo `_` e camelCase
- [ ] Dependências injetadas via construtor — nenhum `new Service()`

### ⚠️ XAML e Binding

- [ ] Cores usam `{StaticResource NomeDoToken}` — nunca valor hex direto
- [ ] Estilos de texto usam `Style="{StaticResource PageTitle}"` etc.
- [ ] Espaçamentos usam `{StaticResource Padding.Card}` etc.
- [ ] `Binding Mode` explícito em todos os bindings
- [ ] `UpdateSourceTrigger` explícito em inputs de formulário
- [ ] Nenhum valor de cor, fonte ou tamanho hardcoded no XAML

### ⚠️ Layout e ScrollViewer

- [ ] `HorizontalScrollBarVisibility="Disabled"` em **todos** os ScrollViewers que contêm formulários
- [ ] ScrollViewers aninhados (pai + filho) todos com `Disabled`
- [ ] Sem `MinWidth`/`MaxWidth` fixo em ComboBoxes — usar `HorizontalAlignment="Stretch"`

### ⚠️ Loading e Async

- [ ] `LoadingService.Show()` antes de qualquer operação assíncrona > 300ms
- [ ] `LoadingService.Hide()` sempre no bloco `finally`
- [ ] `AsyncRelayCommand` para operações assíncronas
- [ ] Nenhuma chamada async sem tratamento de exceção
- [ ] Repositórios/services legados com I/O síncrono mascarado como `Task` → **obrigatório `Task.Run()`** (ver Anti-Pattern #14)

### UI/UX — Botões e Tooltips

- [ ] Todo botão de icone sem texto tem `ToolTip`
- [ ] ToolTip de botão importante tem título e descrição
- [ ] Botões Danger têm confirmação antes de executar

### UI/UX — DataGrid

- [ ] Estilo de hover usa `DataGridRowHoverBrush` (`#F5F8FF`)
- [ ] Estilo de seleção usa `DataGridRowSelectedBrush` (`#E3EFFF`)
- [ ] Estilo hover+seleção usa `DataGridRowHoverSelectedBrush` (`#D6E8FF`)
- [ ] Triggers definidos no `RowStyle`, não inline

### UI/UX — ComboBox

- [ ] Usa `Style="{StaticResource RoundedComboBox}"`
- [ ] `SelectedItem` binding tem `Mode=TwoWay`
- [ ] `TextSearch.TextPath` definido para busca por teclado

---

## Regras de Nomenclatura

| Item | Padrão | Exemplo |
|------|--------|---------|
| Classes | PascalCase | `FuncionariosViewModel` |
| Properties | PascalCase | `IsProcessing`, `Funcionarios` |
| Backing fields | `_camelCase` | `_isProcessing` |
| Commands | PascalCase + "Command" | `CarregarCommand`, `SalvarCommand` |
| Métodos privados async | PascalCase + "Async" | `CarregarAsync()` |
| Arquivos XAML | PascalCase + "View" | `FuncionariosView.xaml` |
| Arquivos ViewModel | PascalCase + "ViewModel" | `FuncionariosViewModel.cs` |
| Interfaces | `I` + PascalCase | `IFuncionarioService` |

---

## Alocação de Código — Onde colocar o quê?

```
É visual (cores, layout, animação)?
    → XAML / View

É lógica de apresentação (estado, validação de UI, commands)?
    → ViewModel

É regra de negócio (cálculo, validação de domínio)?
    → Service

É acesso a dados (banco, arquivo, API)?
    → Repository / Infrastructure Service

É apenas estrutura de dados?
    → DTO / Model
```

### Tabela de responsabilidades

| Pasta | Contém | Não contém |
|-------|--------|------------|
| `Views/` | XAML, InitializeComponent, handlers de evento UI | Lógica de negócio, chamadas a Services |
| `ViewModels/` | Properties, Commands, orquestração de UI | Acesso direto a banco, regras de domínio |
| `Services/` | Lógica de negócio, regras de validação de domínio | UI, Binding, XAML |
| `Dtos/` | Estruturas de dados simples | Lógica de negócio |
| `Converters/` | Transformação de valor para binding | Lógica de negócio |
| `Theme/` | Cores, fontes, estilos | Dados, lógica |

---

## Convenções de Tratamento de Erros

```csharp
// ✅ Padrão obrigatório para operações assíncronas
private async Task MinhaOperacaoAsync()
{
    try
    {
        _loadingService.Show("Processando...");

        var resultado = await _service.ExecutarAsync();
        // usar resultado

        _alertService.ShowSuccess("Operação concluída.");
    }
    catch (Exception ex)
    {
        _logger.Error(ex, "Falha em MinhaOperacaoAsync");
        _alertService.ShowError("Não foi possível concluir a operação.");
    }
    finally
    {
        _loadingService.Hide(); // ← SEMPRE no finally
    }
}
```

---

## Tokens obrigatórios no XAML

Sempre use os tokens definidos em [00-Tokens.md](../01-Fundamentos/00-Tokens.md). Nunca escreva valores literais.

```xml
<!-- ❌ ERRADO — valores literais -->
<Button Background="#184194" Height="34" Padding="16,8"/>
<TextBlock FontSize="24" FontWeight="SemiBold" Foreground="#495057"/>

<!-- ✅ CORRETO — tokens -->
<Button Style="{StaticResource MediumPrimaryButton}"/>
<TextBlock Style="{StaticResource PageTitle}" Text="Título"/>
<Border Padding="{StaticResource Padding.Card}"
        CornerRadius="{StaticResource CornerRadius.Large}"
        BorderThickness="{StaticResource BorderThickness.Thin}"
        BorderBrush="{StaticResource BorderColor}"/>
```
