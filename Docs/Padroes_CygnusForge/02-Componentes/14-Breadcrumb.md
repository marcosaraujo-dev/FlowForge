# Breadcrumb

Indicador de caminho de navegacao que mostra ao usuario onde ele esta dentro da hierarquia do sistema e permite retornar a niveis anteriores com um clique.

> Exemplo visual interativo: [Exemplos-HTML/breadcrumb.html](../Exemplos-HTML/breadcrumb.html)
> Implementacao WPF: `Controls/BreadcrumbControl.xaml`

---

## Indice

1. [Quando usar](#1-quando-usar)
2. [Anatomia](#2-anatomia)
3. [Posicao na interface](#3-posicao-na-interface)
4. [Hierarquia e niveis](#4-hierarquia-e-niveis)
5. [Estados dos itens](#5-estados-dos-itens)
6. [Regras de uso](#6-regras-de-uso)
7. [Implementacao WPF](#7-implementacao-wpf)
8. [Implementacao HTML / React](#8-implementacao-html--react)

---

## 1. Quando usar

**Use breadcrumb quando o usuario pode estar em 2 ou mais niveis de profundidade** dentro do sistema.

| Situacao | Usar breadcrumb? |
|----------|-----------------|
| Tela inicial (nivel 1) | Opcional — apenas "Home" sem links |
| Categoria ou lista (nivel 2) | Sim — "Home > Categoria" |
| Detalhe ou editor (nivel 3) | Sim — "Home > Categoria > Item" |
| Sub-editor ou preview (nivel 4) | Sim — "Home > Categoria > Editor > Preview" |
| Dialog ou modal | Nao — dialogs nao tem breadcrumb |
| Wizard (passos) | Nao — o stepper cumpre essa funcao |

---

## 2. Anatomia

```
Home  >  Meus Templates  >  Editar: Acordo de Compensacao
[link]   [link]              [texto — nao clicavel]
 0.7op    0.7op               SemiBold, 1.0op
           8px  8px
```

**Componentes:**
- **Itens clicaveis:** texto com link, opacidade 0.7, sublinhado no hover
- **Separador:** chevron direito (`›`) do Segoe MDL2 Assets, margem 8px de cada lado, opacidade 0.5
- **Item atual (ultimo):** SemiBold, opacidade 1.0, sem link, sem cursor pointer

**Especificacoes visuais:**
- Fonte: 12px (`AppFontSmall`)
- Cor dos itens: `TextLightBrush` (herda do fundo — compativel com header escuro e fundo claro)
- Opacidade dos links: 0.7 (normal) → 1.0 (hover)
- Decoracao no hover: sublinhado
- Separador: `Icon.ChevronRight`, Margin="8,0", Opacity 0.5

---

## 3. Posicao na interface

Na POC, o breadcrumb fica no **header global da aplicacao** (MainWindow), centralizado horizontalmente entre o logo e os controles de acessibilidade:

```
+-- Header Global (escuro) ---------------------------------------+
| [Logo Empresa]    Home > Meus Templates > Editor    [F2] [A+]  |
+-----------------------------------------------------------------+
+-- Area de Conteudo (cinza claro) --------------------------------+
|  [conteudo da view ativa]                                       |
+-----------------------------------------------------------------+
```

**Por que no header global e nao dentro da view:**
- O breadcrumb reflete o estado de navegacao do aplicativo inteiro, nao de uma view especifica
- Fica sempre visivel independentemente do scroll do conteudo
- Gerenciado pelo `MainViewModel`, que controla a navegacao

---

## 4. Hierarquia e niveis

A hierarquia do sistema segue este padrao:

```
Home
├── Documentos Word                    (nivel 2)
├── Planilhas Excel                    (nivel 2)
├── Meus Templates                     (nivel 2)
│   └── Editar: [Nome do Template]     (nivel 3)
│       └── Preview                    (nivel 4)
├── Gerar Documentos                   (nivel 2)
│   ├── Preview                        (nivel 3)
│   └── Resultado                      (nivel 3)
└── Relatorios Integrados              (nivel 2)
```

**Exemplos reais da POC:**

| Tela | Breadcrumb |
|------|-----------|
| Home | `Home` |
| Documentos Word | `Home > Documentos Word` |
| Planilhas Excel | `Home > Planilhas Excel` |
| Meus Templates | `Home > Meus Templates` |
| Editor de Template | `Home > Meus Templates > Editar: Acordo de Compensacao` |
| Novo Template | `Home > Meus Templates > Novo Documento Word` |
| Preview (do editor) | `Home > Meus Templates > Editor > Preview` |
| Gerar Documentos | `Home > Gerar Documentos` |
| Preview (da geracao) | `Home > Gerar Documentos > Preview` |
| Resultado da Geracao | `Home > Gerar Documentos > Resultado` |
| Relatorios Integrados | `Home > Relatorios Integrados` |

**Regra de truncamento:** Se o caminho for muito longo para o espaco disponivel, truncar o(s) item(s) do meio com reticencias:
```
Home > ... > Editor > Preview
```

---

## 5. Estados dos itens

| Estado | Visual | Comportamento |
|--------|--------|---------------|
| **Link (clicavel)** | Texto claro, 0.7 opacidade | Clique navega para aquela tela |
| **Hover** | 1.0 opacidade + sublinhado | Cursor pointer |
| **Atual (ultimo)** | SemiBold, 1.0 opacidade | Nao clicavel, nao tem cursor |
| **Separador** | `›` em 0.5 opacidade | Apenas visual |

---

## 6. Regras de uso

### Obrigatorio

- O item **atual** (ultimo) nunca e clicavel — representa a tela onde o usuario esta
- O item **"Home"** e sempre o primeiro, clicavel (exceto quando e o unico)
- Separador sempre entre os itens — nunca no inicio ou no final
- Atualizar o breadcrumb ao navegar para qualquer tela

### Nomenclatura dos itens

- Usar os mesmos termos dos titulos de pagina correspondentes
- Itens de categoria: substantivo no plural ("Documentos Word", "Meus Templates")
- Item atual de detalhe/editor: pode incluir o nome do registro ("Editar: Acordo de Compensacao")
- Nao usar verbos como prefixo do item (exceto "Editar:" para distinguir de detalhe)

### O que evitar

- Breadcrumb com mais de 5 niveis — se isso acontece, revisar a arquitetura de navegacao
- Breadcrumb em dialogs ou modais
- Itens com nomes muito longos sem truncamento
- Repeticao do item atual no titulo da pagina de forma diferente (breadcrumb e titulo devem ser consistentes)

---

## 7. Implementacao WPF

### Componente existente

**Arquivo:** `Controls/BreadcrumbControl.xaml` e `BreadcrumbControl.xaml.cs`

O `BreadcrumbControl` e um `UserControl` ja implementado na POC. Suporta dois modos de uso.

### Modo 1 — Via string Path (mais simples)

Defina o breadcrumb como string no formato `"Item1 > Item2 > Item3"`.

```csharp
// No MainViewModel — ao navegar para uma tela
PageTitle = "Meus Templates";
Breadcrumb = "Home > Meus Templates";

// Para um editor com nome dinamico
PageTitle = $"Editar: {template.Nome}";
Breadcrumb = $"Home > Meus Templates > Editar: {template.Nome}";
```

```xml
<!-- No MainWindow.xaml -->
<controls:BreadcrumbControl
    Path="{Binding Breadcrumb}"
    NavigateCommand="{Binding NavigateToCommand}"
    HorizontalAlignment="Center"
    VerticalAlignment="Center"/>
```

O separador padrao e `" > "`. Para usar outro:
```xml
<controls:BreadcrumbControl
    Path="{Binding Breadcrumb}"
    Separator=" / "
    NavigateCommand="{Binding NavigateToCommand}"/>
```

### Modo 2 — Via lista de BreadcrumbItem (controle total)

```csharp
// No MainViewModel — para controle preciso sobre keys de navegacao
BreadcrumbItems = new ObservableCollection<BreadcrumbItem>
{
    new BreadcrumbItem { Text = "Home", Key = "home" },
    new BreadcrumbItem { Text = "Meus Templates", Key = "templates" },
    new BreadcrumbItem { Text = "Editar: Acordo", Key = "" } // key vazia = nao navega
};
```

```xml
<controls:BreadcrumbControl
    Items="{Binding BreadcrumbItems}"
    NavigateCommand="{Binding NavigateToCommand}"/>
```

### Propriedades de dependencia

| Propriedade | Tipo | Direcao | Descricao |
|-------------|------|---------|-----------|
| `Path` | `string` | OneWay | Caminho no formato `"A > B > C"` |
| `Separator` | `string` | OneWay | Separador entre itens (padrao: `" > "`) |
| `NavigateCommand` | `ICommand` | OneWay | Comando executado ao clicar num item |
| `Items` | `ObservableCollection<BreadcrumbItem>` | OneWay | Lista de itens (alternativa ao Path) |

### BreadcrumbItem

```csharp
public class BreadcrumbItem
{
    public string Text { get; set; }   // Texto exibido
    public string Key  { get; set; }   // Chave passada ao NavigateCommand
    public bool IsLast { get; set; }   // Definido automaticamente pelo controle
}
```

### NavigateToCommand no MainViewModel

O `NavigateToCommand` recebe a `Key` do item clicado e navega para a tela correspondente:

```csharp
public IRelayCommand<string> NavigateToCommand { get; }

public MainViewModel(...)
{
    NavigateToCommand = new RelayCommand<string>(NavigateTo);
}

private void NavigateTo(string? key)
{
    switch (key)
    {
        case "home":      NavigateToHomeCommand.Execute(null);      break;
        case "templates": NavigateToMeusTemplatesCommand.Execute(null); break;
        // ... demais cases
    }
}
```

### Padrao de atualizacao ao navegar

```csharp
// Padrao obrigatorio em todo metodo de navegacao
private async Task NavigarParaEditorAsync(Template template)
{
    await _loadingService.ExecuteAsync(async () =>
    {
        _editorViewModel = _factory.CreateEditorViewModel(this, template);
        CurrentView = _editorViewModel;
        PageTitle = $"Editar: {template.Nome}";       // ← titulo da pagina
        Breadcrumb = $"Home > Meus Templates > Editar: {template.Nome}";  // ← breadcrumb
        SelectedMenuItem = null;
    }, "Carregando Editor...");
}
```

---

## 8. Implementacao HTML / React

### HTML / CSS basico

```html
<nav class="breadcrumb" aria-label="Navegacao">
  <ol class="breadcrumb-list">
    <li class="breadcrumb-item">
      <a href="#" class="breadcrumb-link">Home</a>
    </li>
    <li class="breadcrumb-item">
      <a href="#" class="breadcrumb-link">Meus Templates</a>
    </li>
    <li class="breadcrumb-item breadcrumb-current" aria-current="page">
      Editar: Acordo de Compensacao
    </li>
  </ol>
</nav>
```

```css
.breadcrumb-list {
  display: flex;
  align-items: center;
  list-style: none;
  gap: 0;
  font-size: 12px; /* AppFontSmall */
}

.breadcrumb-item {
  display: flex;
  align-items: center;
}

.breadcrumb-item + .breadcrumb-item::before {
  content: '\E76C'; /* Icon.ChevronRight — Segoe MDL2 */
  font-family: 'Segoe MDL2 Assets';
  font-size: 11px;
  margin: 0 8px;
  opacity: 0.5;
  color: var(--text-light); /* #ADB5BD */
}

.breadcrumb-link {
  color: var(--text-light);  /* #ADB5BD */
  text-decoration: none;
  opacity: 0.7;
  transition: opacity 0.15s;
}

.breadcrumb-link:hover {
  opacity: 1;
  text-decoration: underline;
}

.breadcrumb-current {
  color: var(--text-light);
  font-weight: 600;
  opacity: 1;
}
```

### React

```tsx
interface BreadcrumbItem {
  label: string;
  href?: string;   // undefined = item atual (nao clicavel)
}

interface BreadcrumbProps {
  items: BreadcrumbItem[];
}

export function Breadcrumb({ items }: BreadcrumbProps) {
  return (
    <nav aria-label="Navegacao">
      <ol className="breadcrumb-list">
        {items.map((item, index) => {
          const isLast = index === items.length - 1;
          return (
            <li key={index} className="breadcrumb-item">
              {isLast || !item.href ? (
                <span className="breadcrumb-current" aria-current={isLast ? "page" : undefined}>
                  {item.label}
                </span>
              ) : (
                <a href={item.href} className="breadcrumb-link">
                  {item.label}
                </a>
              )}
            </li>
          );
        })}
      </ol>
    </nav>
  );
}

// Uso:
<Breadcrumb items={[
  { label: "Home", href: "/" },
  { label: "Meus Templates", href: "/templates" },
  { label: "Editar: Acordo de Compensacao" }
]} />
```

### Acessibilidade

- Usar `<nav aria-label="Navegacao">` como container
- Usar `<ol>` (lista ordenada) para os itens
- Marcar o item atual com `aria-current="page"`
- O separador deve ser visual apenas — nao incluir no texto lido pelo leitor de tela (usar `::before` CSS ou `aria-hidden`)

---

*Anterior: [13-Secoes-Formulario.md](13-Secoes-Formulario.md) | Proximo: [15-Cards-Catalogo.md](15-Cards-Catalogo.md)*
