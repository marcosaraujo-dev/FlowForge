# Acessibilidade

Diretrizes para garantir que todas as interfaces Empresa sejam usaveis por todas as pessoas, incluindo usuarios com deficiencias visuais, motoras, cognitivas ou auditivas.

O nivel alvo e **WCAG 2.1 AA**.

---

## Indice

1. [Principios de acessibilidade](#1-principios-de-acessibilidade)
2. [Contraste de cores](#2-contraste-de-cores)
3. [Navegacao por teclado](#3-navegacao-por-teclado)
4. [Foco visivel](#4-foco-visivel)
5. [Textos e rotulos](#5-textos-e-rotulos)
6. [Imagens e icones](#6-imagens-e-icones)
7. [Formularios acessiveis](#7-formularios-acessiveis)
8. [Tabelas acessiveis](#8-tabelas-acessiveis)
9. [Feedback e notificacoes](#9-feedback-e-notificacoes)
10. [Escala de fonte](#10-escala-de-fonte)
11. [Checklist por componente](#11-checklist-por-componente)

---

## 1. Principios de acessibilidade

Baseados no WCAG (Web Content Accessibility Guidelines):

| Principio | Descricao | Exemplo |
|-----------|-----------|---------|
| **Perceptivel** | O conteudo pode ser percebido por todos os sentidos disponiveis | Texto alternativo em icones, contraste suficiente |
| **Operavel** | A interface pode ser operada por diferentes meios de entrada | Navegacao por teclado, sem dependencia de mouse |
| **Compreensivel** | O conteudo e a operacao sao compreensiveis | Mensagens claras, comportamento previsivel |
| **Robusto** | Funciona com diferentes tecnologias assistivas | Semantica correta, ARIA quando necessario |

---

## 2. Contraste de cores

### Razoes minimas de contraste (WCAG AA)

| Tipo de conteudo | Razao minima | Ferramenta para verificar |
|-----------------|-------------|--------------------------|
| Texto normal (< 18px) | 4.5:1 | WebAIM Contrast Checker |
| Texto grande (>= 18px ou >= 14px bold) | 3:1 | WebAIM Contrast Checker |
| Componentes de UI e graficos | 3:1 | WebAIM Contrast Checker |

### Cores do Design System e contraste

| Combinacao | Razao | Status |
|-----------|-------|--------|
| Texto Primary (#495057) em fundo branco (#FFFFFF) | 7.2:1 | Aprovado AA e AAA |
| Texto Secondary (#6C757D) em fundo branco | 4.7:1 | Aprovado AA |
| Texto Caption (#ADB5BD) em fundo branco | 2.7:1 | **Reprovado** — usar apenas para texto decorativo/complementar, nunca para informacao essencial |
| Texto branco em fundo Primary (#184194) | 8.1:1 | Aprovado AA e AAA |
| Texto branco em fundo Success (#28A745) | 3.9:1 | Aprovado AA (texto grande). Para texto normal, usar fundo mais escuro ou texto escuro |
| Texto branco em fundo Danger (#DC3545) | 4.0:1 | Aprovado AA (texto grande) |
| Texto escuro (#212529) em fundo Warning (#FFC107) | 14.8:1 | Aprovado AA e AAA |
| Texto branco em fundo Warning (#FFC107) | 1.4:1 | **Reprovado** — NUNCA usar texto branco em fundo Warning |

### Regras de contraste

- **Nunca** transmitir informacao apenas por cor. Sempre acompanhar com icone, texto ou padrao visual
- Badges de status tem **texto + cor de fundo** (ex: "Ativo" em verde, nao apenas um circulo verde)
- Linhas de tabela selecionadas usam fundo azulado **e** indicador visual (borda ou icone)
- Erros de formulario mostram borda vermelha **e** mensagem de texto

```
❌ ERRADO — so cor indica o estado
    ● (verde)     ● (vermelho)     ● (amarelo)

✅ CORRETO — cor + texto
    ● Ativo        ● Inativo        ● Pendente
```

---

## 3. Navegacao por teclado

Toda a aplicacao deve ser operavel apenas com teclado.

### Teclas padrao

| Tecla | Acao |
|-------|------|
| `Tab` | Avanca para o proximo elemento focavel |
| `Shift + Tab` | Volta para o elemento focavel anterior |
| `Enter` | Ativa botao, link ou item selecionado |
| `Space` | Ativa checkbox, toggle, botao |
| `Escape` | Fecha modal, dropdown, tooltip, cancela acao |
| `Setas ↑ ↓` | Navega entre itens de lista, menu, combobox |
| `Setas ← →` | Navega entre tabs, itens horizontais |
| `Home / End` | Vai para primeiro/ultimo item da lista |

### Ordem de foco (tab order)

A ordem de foco deve seguir o fluxo visual de leitura: **esquerda para direita, cima para baixo**.

```
❌ ERRADO — foco pula entre areas sem logica
    [Campo 3] → [Botao Salvar] → [Campo 1] → [Campo 2]

✅ CORRETO — foco segue o fluxo visual
    [Campo 1] → [Campo 2] → [Campo 3] → [Botao Cancelar] → [Botao Salvar]
```

### Armadilha de foco (focus trap)

Quando um Dialog/Modal esta aberto, o foco deve ficar **preso dentro do modal**. O usuario nao pode navegar por Tab para elementos atras do overlay.

```
Modal aberto:
    [X Fechar] → [Campo 1] → [Campo 2] → [Cancelar] → [Salvar] → (volta para [X Fechar])
                                                                     ↑ ciclo fechado
```

Ao fechar o modal, o foco retorna para o elemento que o abriu.

---

## 4. Foco visivel

Todo elemento focavel deve ter um indicador de foco **claramente visivel**. Nunca remover o outline de foco.

### Estilo de foco padrao

```
Borda: 2px solid #93C5FD (azul claro)
Offset: 2px (fora do elemento, nao sobrepoe conteudo)
Border Radius: mesmo do elemento + 2px
```

### Regras

| Regra | Descricao |
|-------|-----------|
| Nunca `outline: none` sem substituto | Se remover o outline nativo, **deve** adicionar estilo customizado equivalente |
| Foco visivel em TODOS os interativos | Botoes, inputs, links, checkboxes, tabs, itens de menu, linhas de tabela clicaveis |
| Foco contrastante | O indicador de foco deve ter contraste 3:1 contra o fundo adjacente |
| Foco nao muda layout | O indicador de foco nao deve mover ou redimensionar elementos vizinhos |

```css
/* ❌ ERRADO — remove foco sem substituto */
button:focus { outline: none; }

/* ✅ CORRETO — substitui por estilo visivel */
button:focus-visible {
  outline: 2px solid #93C5FD;
  outline-offset: 2px;
}
```

```xml
<!-- WPF — estilo de foco customizado -->
<Style.Triggers>
    <Trigger Property="IsKeyboardFocused" Value="True">
        <Setter Property="BorderBrush" Value="#93C5FD"/>
        <Setter Property="BorderThickness" Value="2"/>
    </Trigger>
</Style.Triggers>
```

---

## 5. Textos e rotulos

### Hierarquia de titulos

Usar titulos em ordem logica — nunca pular niveis.

```
❌ ERRADO
    <h1>Pagina</h1>
    <h4>Secao</h4>      ← pulou h2 e h3

✅ CORRETO
    <h1>Pagina</h1>
    <h2>Secao</h2>
    <h3>Subsecao</h3>
```

### Texto significativo em links e botoes

```
❌ ERRADO — texto generico
    Para ver o relatorio, <a>clique aqui</a>.

✅ CORRETO — texto descreve o destino
    <a>Ver relatorio de funcionarios</a>
```

### Abreviacoes e siglas

Na primeira ocorrencia em uma pagina, expandir a sigla:

```
CLT (Consolidacao das Leis do Trabalho)
eSocial (Sistema de Escrituracao Digital das Obrigacoes Fiscais, Previdenciarias e Trabalhistas)
```

---

## 6. Imagens e icones

### Icones decorativos vs informativos

| Tipo | Exemplo | Tratamento |
|------|---------|-----------|
| **Decorativo** | Icone ao lado de texto que ja descreve a acao | Ocultar de leitores de tela (`aria-hidden="true"` / `AutomationProperties.Name=""`) |
| **Informativo** | Icone sozinho em botao sem texto | Texto alternativo obrigatorio (`aria-label` / `AutomationProperties.Name` / `ToolTip`) |

```tsx
// ❌ ERRADO — icone sem texto alternativo
<button><Icon code="E74D" /></button>

// ✅ CORRETO — aria-label descreve a acao
<button aria-label="Excluir registro"><Icon code="E74D" /></button>

// ✅ CORRETO — icone decorativo (texto visivel ja descreve)
<button><Icon code="E74D" aria-hidden="true" /> Excluir</button>
```

```xml
<!-- WPF -->
<!-- ❌ ERRADO -->
<Button Content="&#xE74D;" Style="{StaticResource GridIconButton}"/>

<!-- ✅ CORRETO -->
<Button Content="&#xE74D;" Style="{StaticResource GridIconButton}"
        AutomationProperties.Name="Excluir registro">
    <Button.ToolTip>
        <ToolTip><TextBlock Text="Excluir registro"/></ToolTip>
    </Button.ToolTip>
</Button>
```

---

## 7. Formularios acessiveis

### Labels vinculados

Todo campo de formulario **deve** ter um label vinculado programaticamente.

```html
<!-- ❌ ERRADO — label solto sem vinculo -->
<span>Nome Completo</span>
<input type="text" />

<!-- ✅ CORRETO — htmlFor vincula label ao input -->
<label for="nome">Nome Completo</label>
<input id="nome" type="text" />
```

```xml
<!-- WPF — AutomationProperties vincula label -->
<TextBlock Text="Nome Completo" x:Name="LabelNome"/>
<TextBox AutomationProperties.LabeledBy="{Binding ElementName=LabelNome}"/>
```

### Campos obrigatorios

```
❌ ERRADO — apenas asterisco visual
    Nome *  [__________]

✅ CORRETO — asterisco + aria-required
    Nome * (obrigatorio)  [__________]
    <!-- aria-required="true" no input -->
```

### Mensagens de erro

Erros devem ser:
1. Vinculados ao campo (`aria-describedby` / `AutomationProperties.HelpText`)
2. Anunciados ao usuario (via `aria-live="polite"` ou equivalente)
3. Visiveis (texto + cor + icone)

```html
<label for="cpf">CPF *</label>
<input id="cpf" aria-describedby="cpf-erro" aria-invalid="true" />
<span id="cpf-erro" role="alert">CPF invalido. Use o formato 000.000.000-00</span>
```

### Agrupamento de campos

Campos relacionados devem ser agrupados semanticamente.

```html
<fieldset>
  <legend>Dados Pessoais</legend>
  <!-- campos -->
</fieldset>
```

```xml
<!-- WPF — GroupBox para agrupamento -->
<GroupBox Header="Dados Pessoais">
    <!-- campos -->
</GroupBox>
```

---

## 8. Tabelas acessiveis

### Cabecalhos de coluna

```html
<!-- ✅ Usar <th> com scope -->
<table>
  <thead>
    <tr>
      <th scope="col">Nome</th>
      <th scope="col">CPF</th>
      <th scope="col">Status</th>
    </tr>
  </thead>
</table>
```

### Acoes em linhas

Botoes de acao em cada linha devem ter label que identifica o registro:

```
❌ ERRADO — todos os botoes tem o mesmo label
    Editar | Editar | Editar

✅ CORRETO — label inclui contexto
    Editar Joao Silva | Editar Maria Santos | Editar Carlos Lima
```

```html
<button aria-label="Editar Joao Silva"><Icon code="E70F" /></button>
```

### Estado vazio

Quando a tabela nao tem dados, o estado vazio deve ser anunciado:

```html
<div role="status">Nenhum trabalhador encontrado.</div>
```

---

## 9. Feedback e notificacoes

### Alertas dinamicos

Alertas que aparecem em resposta a acoes devem ser anunciados por leitores de tela.

```html
<!-- Alerta de sucesso — anunciado automaticamente -->
<div role="alert">Dados salvos com sucesso.</div>

<!-- Regiao de status — anunciada na proxima pausa do leitor -->
<div role="status" aria-live="polite">3 resultados encontrados.</div>
```

### Loading

```html
<!-- Indicar que conteudo esta carregando -->
<div role="status" aria-live="polite">Carregando trabalhadores...</div>

<!-- Indicar que carregamento terminou -->
<div role="status" aria-live="polite">5 trabalhadores carregados.</div>
```

### Dialogs modais

```html
<div role="dialog" aria-modal="true" aria-labelledby="dialog-titulo">
  <h2 id="dialog-titulo">Confirmar Exclusao</h2>
  <p>Tem certeza que deseja excluir este registro?</p>
  <button>Cancelar</button>
  <button>Excluir</button>
</div>
```

---

## 10. Escala de fonte

A aplicacao deve suportar aumento de fonte sem quebrar o layout.

### Niveis de escala

| Nivel | Fator | Uso |
|-------|-------|-----|
| Normal | 1.0x | Padrao |
| Grande | 1.25x | Usuarios com baixa visao leve |
| Extra Grande | 1.5x | Usuarios com baixa visao moderada |

### Regras de escala

- Textos devem usar unidades relativas quando possivel (`em`, `rem` em web; tokens de fonte em WPF)
- O layout nao deve quebrar com escala 1.5x — conteudo pode redistribuir ou ativar scroll, mas nao sobrepor
- Botoes e inputs devem crescer proporcionalmente (altura e padding)
- Icones devem acompanhar a escala do texto

### Implementacao

A aplicacao Empresa disponibiliza um widget de acessibilidade (tecla F2) que cicla entre Normal → Grande → Extra Grande, aplicando `ScaleTransform` no conteudo.

### Regra critica — MinHeight em vez de Height

Quando `ScaleTransform` aumenta o fator de escala, elementos com `Height` fixo **nao crescem** — o texto fica cortado e o usuario com baixa visao nao consegue ler o conteudo.

**Regra obrigatoria:** todo container WPF que envolve texto deve usar `MinHeight` em vez de `Height`.

```xml
<!-- ❌ ERRADO — Height fixo corta o texto com zoom ativo -->
<Button Height="120">
    <StackPanel>
        <TextBlock Text="{Binding Titulo}"/>
        <TextBlock Text="{Binding Descricao}"/>
    </StackPanel>
</Button>

<Border Height="80">
    <TextBlock Text="{Binding Rotulo}" TextWrapping="Wrap"/>
</Border>
```

```xml
<!-- ✅ CORRETO — MinHeight define o minimo, container cresce com o conteudo -->
<Button MinHeight="120">
    <StackPanel>
        <TextBlock Text="{Binding Titulo}" TextWrapping="Wrap"/>
        <TextBlock Text="{Binding Descricao}" TextWrapping="Wrap"/>
    </StackPanel>
</Button>

<Border MinHeight="80">
    <TextBlock Text="{Binding Rotulo}" TextWrapping="Wrap"/>
</Border>
```

| Propriedade | Com ScaleTransform 1.5x | Resultado |
|-------------|------------------------|-----------|
| `Height="120"` | Container nao cresce | Texto cortado |
| `MinHeight="120"` | Container cresce com o conteudo | Texto legivel |

Aplica-se a: `Button`, `Border`, `Grid`, `StackPanel`, `ListBoxItem`, `DataGridRow` e qualquer outro elemento que defina dimensao vertical e contenha texto filho.

---

## 11. Checklist por componente

| Componente | Requisitos de acessibilidade |
|-----------|------------------------------|
| **Botao** | Label visivel ou `aria-label`, focavel via Tab, ativavel via Enter/Space |
| **Botao de icone** | `aria-label` obrigatorio + ToolTip visivel |
| **Input** | Label vinculado (`for`/`LabeledBy`), `aria-required` se obrigatorio, `aria-invalid` + mensagem se erro |
| **ComboBox/Select** | Label vinculado, navegacao por setas ↑↓, selecao por Enter |
| **Checkbox** | Label vinculado, toggle por Space |
| **DataGrid** | Cabecalhos `<th scope="col">`, navegacao por setas, estado vazio anunciado |
| **Tabs** | `role="tablist"` + `role="tab"`, setas ←→ para navegar, Tab para conteudo |
| **Accordion** | Botao com `aria-expanded`, Enter/Space para toggle |
| **Dialog** | `role="dialog"` + `aria-modal`, focus trap, ESC fecha, foco retorna ao elemento original |
| **Toast/Alert** | `role="alert"` (urgente) ou `role="status"` (informativo) |
| **Tooltip** | Aparece no foco (nao so hover), `role="tooltip"`, vinculado via `aria-describedby` |
| **Menu lateral** | `role="navigation"`, item ativo com `aria-current="page"`, submenu com `aria-expanded` |
| **Breadcrumb** | `role="navigation"` + `aria-label="Caminho"`, item atual com `aria-current="page"` |
| **Loading** | `role="status"` + `aria-live="polite"`, mensagem descritiva |
| **Badge** | Se informativo, acessivel via texto. Se decorativo, `aria-hidden="true"` |
