# Padrão de Card de Catálogo e Agrupamento por Categoria — Design

**Spec**: `.specs/features/card-catalogo-agrupamento/spec.md`
**Status**: Approved

---

## Architecture Overview

Abordagem **aditiva**: dois arquivos novos e autocontidos no site (`css/catalog.css`,
`js/catalog-filter.js`), sem tocar no sistema de tokens de `css/style.css` (index.html) nem
de `artigos/style.css` (artigos) — cada página segue com seus próprios tokens de cor/tipografia,
só as regras estruturais do card e do filtro passam a vir do partial compartilhado.

O filtro é **declarativo e orientado a dados**: o container de cada catálogo declara suas
categorias via atributo `data-catalog-categories` (JSON inline), cada card leva
`data-category="<key>"`, e a barra de chips é **inteira gerada por JS** em runtime — não existe
markup de chip no HTML estático. Isso resolve o requisito de progressive enhancement (GRP-05)
sem nenhuma lógica extra: sem JS, não há chips para clicar, e nenhum card é escondido por padrão
— tudo já está visível porque o HTML nunca aplica `hidden` sozinho.

```mermaid
graph TD
    A[DOMContentLoaded] --> B{Existe [data-catalog] na página?}
    B -- não --> Z[Nada acontece]
    B -- sim --> C[Lê data-catalog-categories JSON]
    C --> D[Conta cards por data-category dentro do grid]
    D --> E[Renderiza chips: Todos + uma por categoria, com contador]
    E --> F[Usuário clica num chip]
    F --> G[Aplica hidden nos cards que não batem a categoria]
    G --> H{Algum card visível?}
    H -- sim --> I[Esconde .catalog-empty]
    H -- não --> J[Mostra .catalog-empty]
```

---

## Code Reuse Analysis

### Existing Components to Leverage

| Component | Location | How to Use |
| --- | --- | --- |
| `.product-card` / `.product-card__*` (estrutura visual: header+ícone, título+tagline, corpo, footer) | `site/css/style.css:488-655` | Estrutura reaproveitada como base do `.catalog-card` — mesmo layout, classes renomeadas |
| `.article-card` / `.article-card__*` | `site/artigos/style.css:214-231` | Estrutura reaproveitada na variante textual do `.catalog-card` |
| `.chip` (já existe em `artigos/style.css:163-169`, decorativo, usado no hero) | `site/artigos/style.css` | **Não reaproveitado diretamente** — é estático/não clicável; o novo `.category-filter__chip` é um componente novo com estado (`aria-pressed`, contador). Evitar colisão de nome: usar `.category-filter__chip`, não `.chip`, para não confundir com o hero chip existente |
| Padrão de IIFE + `'use strict'` + `DOMContentLoaded` + `IntersectionObserver` já usado em `js/main.js` | `site/js/main.js` | Mesmo estilo de código no novo `js/catalog-filter.js` (consistência, zero dependências externas) |
| `initFadeInElements()` (lista de seletores que recebem fade-in ao rolar) | `site/js/main.js:79-81` | Atualizar a lista: trocar `.product-card` por `.catalog-card` (ver Tarefa de migração) |

### Integration Points

| System | Integration Method |
| --- | --- |
| `index.html` | Adiciona `<link rel="stylesheet" href="css/catalog.css">` e `<script src="js/catalog-filter.js" defer></script>`; seção `#produtos` migra markup de `.product-card` para `.catalog-card` |
| `artigos/index.html` | Adiciona `<link rel="stylesheet" href="../css/catalog.css">` e `<script src="../js/catalog-filter.js" defer></script>`; lista de artigos migra de `.article-card` avulso para `.catalog-card` dentro de um grid com filtro |

---

## Components

### `catalog.css` (Card de Catálogo + Grupo com Filtro)

- **Purpose**: Estrutura visual compartilhada do card de catálogo e da barra de chips de categoria — único arquivo CSS novo para as duas páginas.
- **Location**: `site/css/catalog.css`
- **Classes principais**:
  - `.catalog-grid` — grid responsivo (`repeat(auto-fit, minmax(...))`, mesmo princípio do `.products-grid`/`.theme-grid` já existentes)
  - `.catalog-card`, `.catalog-card__header`, `.catalog-card__icon`, `.catalog-card__title`, `.catalog-card__tagline`, `.catalog-card__desc`, `.catalog-card__tags`, `.catalog-card__status`, `.catalog-card__footer`, `.catalog-card__cta`, `.catalog-card__meta`
  - Modificadores: `.catalog-card--product` (variante com features/tech tags/status, como o `product-card` atual), `.catalog-card--article` (variante compacta: título+descrição+meta+ícone, como o `article-card` atual)
  - `.category-filter`, `.category-filter__chip`, `.category-filter__chip.is-active`, `.category-filter__count`
  - `.catalog-empty` (estado vazio, `hidden` por padrão, alternado via JS)
- **Dependencies**: Nenhuma — tokens próprios definidos no topo do arquivo (ver Tech Decisions), não lê `--primary` nem `--blue` das outras stylesheets.
- **Reuses**: Layout estrutural de `.product-card` e `.article-card` (ver Code Reuse Analysis).

### `catalog-filter.js`

- **Purpose**: Gera a barra de chips a partir de dados declarados no HTML e aplica o filtro por categoria, sem dependências externas.
- **Location**: `site/js/catalog-filter.js`
- **Interfaces (contrato de markup, não API JS)**:
  - Container: `<div data-catalog data-catalog-categories='[{"key":"produto","label":"Produto"},...]'>`
  - Grid: `<div data-catalog-grid>` dentro do container
  - Card: qualquer filho direto do grid com `data-category="<key>"`
  - Placeholder do filtro (opcional): `<div data-catalog-filter></div>` — se ausente, o script cria a barra antes do grid automaticamente
  - Estado vazio (opcional): `<p data-catalog-empty hidden>texto</p>` — se ausente, o script cria um `<p>` padrão
- **Comportamento**:
  - `init()` roda em `DOMContentLoaded`, varre todo `[data-catalog]` da página (idempotente — cada catálogo é independente, Produtos e Artigos não interferem entre si)
  - Conta cards por `key` declarado + chip "Todos" (`key: "all"`, contador = total)
  - Clique num chip: toggla `hidden` nos cards do grid correspondente, marca `.is-active` + `aria-pressed="true"` no chip clicado, remove dos demais, mostra/esconde `.catalog-empty` conforme contagem de cards visíveis
  - Categoria declarada com 0 cards: chip ainda é renderizado (`(0)`), clicável, resulta em estado vazio
- **Dependencies**: Nenhuma (vanilla JS, `querySelectorAll`, `addEventListener`).
- **Reuses**: Estilo de código de `js/main.js` (IIFE, `'use strict'`).

---

## Data Models

### Declaração de categorias (atributo `data-catalog-categories`)

```typescript
interface CatalogCategory {
  key: string;    // chave técnica, casa com data-category dos cards — ex: "produto"
  label: string;  // texto exibido no chip — ex: "Produto"
}
// Exemplo real — seção Produtos (index.html):
// data-catalog-categories='[
//   {"key":"produto","label":"Produto"},
//   {"key":"treinamento","label":"Treinamento"},
//   {"key":"ferramenta-dev","label":"Ferramenta Dev"}
// ]'

// Exemplo real — Artigos (artigos/index.html):
// data-catalog-categories='[
//   {"key":"uso-de-ia","label":"Uso de IA"},
//   {"key":"desenvolvimento","label":"Desenvolvimento"},
//   {"key":"gestao-de-projetos","label":"Gestão de Projetos"},
//   {"key":"processos","label":"Processos"}
// ]'
```

**Mapeamento de conteúdo atual → categoria:**

| Página | Item | `data-category` |
| --- | --- | --- |
| Produtos | OffWork | `produto` |
| Produtos | ForgeFinance | `produto` |
| Produtos | Treinamento IA | `treinamento` |
| Produtos | Code Guardian | `ferramenta-dev` |
| Artigos | "IA nas empresas: por que tanta implementação vira frustração" | `uso-de-ia` |

**Relationships**: Puramente declarativo em HTML — não há modelo de dado persistido, backend ou build step. `key` é único dentro de um mesmo `[data-catalog]`; dois catálogos na mesma página (não é o caso hoje) podem reusar a mesma `key` sem conflito, pois a contagem/filtro é sempre escopado ao `[data-catalog]` mais próximo.

---

## Error Handling Strategy

| Error Scenario | Handling | User Impact |
| --- | --- | --- |
| JS falha ao carregar / desabilitado | Nenhum — HTML estático já mostra todos os cards por padrão, chips simplesmente não aparecem | Site funciona normalmente, só sem o filtro (GRP-05) |
| `data-catalog-categories` com JSON malformado | `try/catch` no parse; se falhar, loga aviso no console e o script não gera chips para aquele catálogo (mesmo resultado do "sem JS") | Cards continuam visíveis, sem filtro nessa seção específica |
| Categoria filtrada sem nenhum card correspondente | Contador do chip mostra `(0)`; ao clicar, `.catalog-empty` é exibido | Usuário vê mensagem clara em vez de área em branco |
| Card sem `data-category` dentro de um grid filtrado (erro de markup futuro) | Tratado como não pertencente a nenhuma categoria — fica oculto em qualquer filtro exceto "Todos" | Força quem for adicionar conteúdo novo a lembrar do atributo (comportamento intencional, documentado) |

---

## Risks & Concerns

| Concern | Location (file:line) | Impact | Mitigation |
| --- | --- | --- | --- |
| `js/main.js` mantém `.product-card` na lista de seletores do fade-in (`initFadeInElements`) | `site/js/main.js:79-81` | Se o markup migrar para `.catalog-card` e a lista não for atualizada, os cards de Produtos perdem a animação de entrada | Task de implementação inclui atualizar essa lista para `.catalog-card` |
| `artigos/style.css` já define uma classe `.chip` (decorativa, estática) | `site/artigos/style.css:163-169` | Risco de colisão de nome/confusão visual com o novo componente de filtro | Nome do novo componente é `.category-filter__chip`, deliberadamente distinto — sem colisão de seletor |
| Nenhum teste automatizado no site (é HTML/CSS/JS estático sem test runner) | projeto todo | "Build verde" do harness não se aplica (guardrail 4 já prevê isso: "Genérico / sem stack detectada → hook não bloqueia") | Verificação será manual/visual (abrir no navegador) — ver Tasks/Execute |

> Nenhum risco de segurança, performance ou dívida técnica adicional identificado além dos listados acima.

---

## Tech Decisions

| Decision | Choice | Rationale |
| --- | --- | --- |
| Tokens visuais do novo componente | Valores próprios hardcoded no topo de `catalog.css` (ex.: `--cc-accent:#184194`), não lidos de `--primary` (style.css) nem `--blue` (artigos/style.css) | As duas stylesheets já usam nomes de variável diferentes para o mesmo azul da marca; acoplar a um dos dois nomes quebraria silenciosamente se ele mudar. Um terceiro conjunto pequeno e autocontido é mais seguro e mantém o Out of Scope (não unificar tokens) |
| Onde vive a contagem por categoria | Calculada em runtime pelo JS (conta `data-category` no DOM), nunca hardcoded no HTML/JSON | Elimina risco de contador dessincronizado do conteúdo real (ex.: adicionar um artigo novo e esquecer de atualizar "(1)" para "(2)") |
| Chips existem no HTML estático ou só via JS | Só via JS — HTML nunca contém markup de chip | É o mecanismo mais simples para garantir GRP-05 (sem JS = tudo visível) sem precisar de classe `no-js`/`js` ou lógica condicional extra |
| Nome da classe base do card | `.catalog-card` (não `.card` genérico) | Evita colisão com qualquer classe `.card` que já exista ou venha a existir no site; deixa explícito que é o card de catálogo/conteúdo, distinto do card de app do design system (`02-Componentes/03-Cards.md`) |
| Onde documentar o padrão neste repo | Novo arquivo `02-Componentes/15-Cards-Catalogo.md` (card) + novo arquivo `03-Layout/04-Agrupamento-Filtro.md` (chips/filtro), ambos linkados em `_index.md` e em `Exemplos-HTML/` | Segue a convenção já estabelecida neste design system (um arquivo por componente, exemplo HTML separado) |
