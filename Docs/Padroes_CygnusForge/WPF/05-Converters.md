# WPF — Converters, Behaviors e Properties Calculadas

Guia de decisão: onde colocar lógica de transformação visual no WPF MVVM.

---

## Índice

1. [Quando usar cada abordagem](#1-quando-usar-cada-abordagem)
2. [Converters — catálogo completo](#2-converters--catálogo-completo)
3. [Como registrar converters no XAML](#3-como-registrar-converters-no-xaml)
4. [Como criar um converter novo](#4-como-criar-um-converter-novo)
5. [Properties calculadas no ViewModel](#5-properties-calculadas-no-viewmodel)
6. [Behaviors — quando e como usar](#6-behaviors--quando-e-como-usar)

---

## 1. Quando usar cada abordagem

### Regra de decisão

```
É lógica de NEGÓCIO que deriva de dados?
    → Property calculada no ViewModel

É transformação VISUAL (cor, visibilidade, texto formatado)?
    → Converter no XAML

É COMPORTAMENTO de UI sem lógica de negócio (foco, scroll, animação)?
    → Behavior no XAML
```

### Tabela comparativa

| Técnica | Onde vive | Quando usar | Exemplo |
|---------|-----------|-------------|---------|
| **Converter** | XAML — dentro do Binding | Transformação visual one-way | `bool` → `Visibility`, `enum` → cor |
| **Property calculada** | ViewModel | Derivar valor de outras properties | `IsFormValid`, `NomeCompleto` |
| **Behavior** | XAML — no elemento | Adicionar comportamento sem code-behind | Foco automático, drag-and-drop |

### Exemplos de decisão

```csharp
// ❌ ERRADO — lógica de apresentação no ViewModel
public string StatusColor => Status == "OK" ? "#28A745" : "#DC3545";

// ✅ CORRETO — ViewModel expõe dado bruto
public string Status { get; set; } = "OK";
// Converter transforma StatusDocumento → SolidColorBrush
```

```csharp
// ✅ CORRETO — property calculada para lógica de negócio
public bool PodeSalvar => !string.IsNullOrEmpty(Nome) && EmpresaSelecionada != null;
// O ViewModel sabe quando pode salvar — não é transformação visual
```

---

## 2. Converters — catálogo completo

### BoolToVisibilityConverter

Converte `bool` para `Visibility`. Suporta inversão via parâmetro.

```xml
<!-- Visível quando true -->
<StackPanel Visibility="{Binding TemErro, Converter={StaticResource BoolToVisibility}}"/>

<!-- Visível quando false (inverso) -->
<StackPanel Visibility="{Binding IsLoading, Converter={StaticResource BoolToVisibility}, ConverterParameter=Inverse}"/>
```

**Comportamento:**

| Entrada | Parâmetro | Saída |
|---------|-----------|-------|
| `true` | — | `Visible` |
| `false` | — | `Collapsed` |
| `true` | `"Inverse"` | `Collapsed` |
| `false` | `"Inverse"` | `Visible` |
| `null` | — | `Collapsed` |

---

### InverseBoolToVisibilityConverter

Atalho para `BoolToVisibility` com `ConverterParameter=Inverse`. Preferir `BoolToVisibility + Inverse` para consistência.

```xml
<Button Visibility="{Binding IsProcessing, Converter={StaticResource InverseBoolToVisibility}}"/>
```

---

### NullToVisibilityConverter

Colapsa o elemento quando o valor é `null` ou string vazia.

```xml
<!-- Mostra painel apenas quando há seleção -->
<StackPanel Visibility="{Binding ItemSelecionado, Converter={StaticResource NullToVisibility}}"/>

<!-- Mostra placeholder quando não há seleção -->
<TextBlock Text="Nenhum item selecionado"
           Visibility="{Binding ItemSelecionado, Converter={StaticResource NullToVisibility}, ConverterParameter=Inverse}"/>
```

---

### BoolToOpacityConverter

Converte `bool` para opacidade. Útil para desabilitar visualmente elementos sem usar `IsEnabled` (que muda o cursor).

```xml
<!-- Item desabilitado aparece com 40% de opacidade -->
<Border Opacity="{Binding IsHabilitado, Converter={StaticResource BoolToOpacity}}">
    <TextBlock Text="{Binding Nome}"/>
</Border>
```

**Valores:**

| Entrada | Saída |
|---------|-------|
| `true` | `1.0` (100% opaco) |
| `false` | `0.4` (40% opaco) |

---

### StringToBrushConverter

Converte string hexadecimal (`"#FF0000"`) para `SolidColorBrush`. Fallback: cinza (`TextSecondaryColor`).

```xml
<!-- Quando o dado já é uma cor hex (ex: vindo de JSON de configuração) -->
<Border Background="{Binding CorHex, Converter={StaticResource StringToBrush}}"/>
```

> **Quando NÃO usar:** Se a cor vem de um enum ou status do domínio, use um converter específico (como `StatusToColorConverter`) em vez de armazenar hex no ViewModel.

---

### StatusToColorConverter / StatusToTextConverter

Converte `StatusDocumento` para `SolidColorBrush` ou texto legível.

```xml
<Border Background="{Binding Status, Converter={StaticResource StatusToColor}}"
        CornerRadius="{StaticResource CornerRadius.Small}"
        Padding="6,2">
    <TextBlock Foreground="White"
               Text="{Binding Status, Converter={StaticResource StatusToText}}"/>
</Border>
```

**Mapeamento de cores:**

| Status | Cor | Token equivalente |
|--------|-----|-------------------|
| Pendente | Cinza | `BadgeSecondaryBackground` |
| Gerado | Verde | `BadgeSuccessBackground` |
| Enviado | Azul | `PrimaryColor` |
| Erro | Vermelho | `BadgeDangerBackground` |

---

### TipoTemplateToIconConverter / TipoTemplateToColorConverter

Converte `TipoTemplate` (Word/Excel) para ícone abreviado ou cor de fundo.

```xml
<!-- Badge colorido indicando tipo -->
<Border Background="{Binding Tipo, Converter={StaticResource TipoTemplateToColor}}"
        CornerRadius="{StaticResource CornerRadius.Small}"
        Padding="4,2">
    <TextBlock Foreground="White"
               FontWeight="Bold"
               Text="{Binding Tipo, Converter={StaticResource TipoTemplateToIcon}}"/>
</Border>
```

| Tipo | Ícone | Cor |
|------|-------|-----|
| Word | `"D"` | Azul Word |
| Excel | `"P"` | Verde Excel |

---

### StringSNToSimNaoConverter

Converte o padrão legado `"S"`/`"N"` (usado no Empresa) para `"Sim"`/`"Não"` exibível. É um converter **bidirecional** (suporta `ConvertBack`).

```xml
<TextBlock Text="{Binding Ativo, Converter={StaticResource StringSNToSimNao}}"/>

<!-- Em formulário editável -->
<CheckBox IsChecked="{Binding Ativo, Converter={StaticResource StringSNToSimNao}, Mode=TwoWay}"/>
```

---

### CountSelectedConverter

Converte contagem de itens selecionados para texto de resumo.

```xml
<TextBlock Text="{Binding ItensSelecionados.Count, Converter={StaticResource CountSelected}}"/>
<!-- Exibe: "3 selecionados" ou "Nenhum selecionado" -->
```

---

## 3. Como registrar converters no XAML

Os converters são registrados em `App.xaml` (ou `ResourceDictionary` global). Para usar em qualquer view, basta referenciar pelo `x:Key`.

```xml
<!-- Em App.xaml ou no ResourceDictionary importado -->
<ResourceDictionary>
    <converters:BoolToVisibilityConverter x:Key="BoolToVisibility"/>
    <converters:InverseBoolToVisibilityConverter x:Key="InverseBoolToVisibility"/>
    <converters:NullToVisibilityConverter x:Key="NullToVisibility"/>
    <converters:BoolToOpacityConverter x:Key="BoolToOpacity"/>
    <converters:StringToBrushConverter x:Key="StringToBrush"/>
    <converters:StatusToColorConverter x:Key="StatusToColor"/>
    <converters:StatusToTextConverter x:Key="StatusToText"/>
    <converters:TipoTemplateToIconConverter x:Key="TipoTemplateToIcon"/>
    <converters:TipoTemplateToColorConverter x:Key="TipoTemplateToColor"/>
    <converters:StringSNToSimNaoConverter x:Key="StringSNToSimNao"/>
    <converters:CountSelectedConverter x:Key="CountSelected"/>
</ResourceDictionary>
```

---

## 4. Como criar um converter novo

### Template base

```csharp
using System;
using System.Globalization;
using System.Windows.Data;

namespace MinhaApp.Converters
{
    /// <summary>
    /// [Descrição clara do que o converter faz]
    /// </summary>
    public class MeuConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Sempre fazer cast seguro — nunca assumir o tipo
            if (value is TipoEsperado dado)
            {
                // transformação → retornar valor visual
                return dado.Propriedade ? ValorA : ValorB;
            }

            // Retornar valor padrão seguro
            return ValorPadrao;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Se for one-way binding, não implementar:
            throw new NotImplementedException("MeuConverter é one-way apenas.");

            // Se for two-way (ex: StringSNToSimNao), implementar a conversão inversa.
        }
    }
}
```

### Regras para criar converters

1. **Um converter = uma transformação.** Não misture lógicas diferentes em um único converter.
2. **Nunca jogar exceção no `Convert()`** — retorne um valor padrão seguro.
3. **Cast sempre seguro** — use `value is TipoEsperado x` em vez de `(TipoEsperado)value`.
4. **`ConvertBack` desnecessário?** Jogue `NotImplementedException` com mensagem explicativa.
5. **Registrar em `App.xaml`** antes de usar em qualquer View.
6. **Nome descritivo** — sempre `EntradaParaSaídaConverter` (ex: `BoolToVisibilityConverter`).

---

## 5. Properties calculadas no ViewModel

Properties calculadas são propriedades `get`-only que derivam valor de outras properties. Não precisam de converter porque já retornam o valor final para o ViewModel.

```csharp
// ✅ CORRETO — property calculada para lógica de negócio
private string _nome;
public string Nome
{
    get => _nome;
    set
    {
        SetProperty(ref _nome, value);
        OnPropertyChanged(nameof(NomeCompleto)); // notifica derivada
        OnPropertyChanged(nameof(PodeSalvar));   // notifica derivada
    }
}

private string _sobrenome;
public string Sobrenome
{
    get => _sobrenome;
    set
    {
        SetProperty(ref _sobrenome, value);
        OnPropertyChanged(nameof(NomeCompleto));
    }
}

// Property calculada — não tem backing field
public string NomeCompleto => $"{Nome} {Sobrenome}".Trim();

// Property calculada de validação
public bool PodeSalvar =>
    !string.IsNullOrWhiteSpace(Nome) &&
    EmpresaSelecionada != null;
```

**Quando usar property calculada vs converter:**

| Situação | Use |
|----------|-----|
| `"João" + " " + "Silva"` → `"João Silva"` | Property calculada |
| `bool` → `Visibility.Visible` | Converter |
| `isValido && naoVazio` → `"Pode salvar"` | Property calculada |
| `StatusDocumento.Gerado` → cor verde | Converter |
| `Quantidade > 0` (regra de negócio) | Property calculada |

---

## 6. Behaviors — quando e como usar

Behaviors adicionam comportamento de UI sem código no code-behind e sem lógica no ViewModel. São raros — use somente quando não há outra solução.

### Casos de uso válidos

- **Foco automático** em um campo quando a view abre
- **Scroll para o topo** após carregar dados
- **Validação de máscara** em TextBox (ex: CPF, CNPJ)
- **Arrastar e soltar** (drag-and-drop)

### Como usar (Microsoft.Xaml.Behaviors.Wpf)

```xml
<UserControl xmlns:i="http://schemas.microsoft.com/xaml/behaviors">

    <!-- Executar command no evento Loaded da view -->
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Loaded">
            <i:InvokeCommandAction Command="{Binding CarregarCommand}"/>
        </i:EventTrigger>
    </i:Interaction.Triggers>

</UserControl>
```

### O que NÃO é um behavior válido

```csharp
// ❌ NUNCA — lógica de negócio em behavior
public class ValidarCnpjBehavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        AssociatedObject.LostFocus += (s, e) =>
        {
            var service = new CnpjService(); // ← viola DI
            if (!service.Validar(AssociatedObject.Text))  // ← lógica de negócio
                MessageBox.Show("CNPJ inválido");          // ← viola MVVM
        };
    }
}
```
