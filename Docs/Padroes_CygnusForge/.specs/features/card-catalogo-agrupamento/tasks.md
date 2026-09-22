# Padrão de Card de Catálogo e Agrupamento por Categoria — Tasks

## Execution Protocol (MANDATORY -- do not skip)

Implement these tasks with the `tlc-spec-driven` skill: **activate it by name and follow its Execute flow and Critical Rules.** Do not search for skill files by filesystem path. The skill is the source of truth for the full flow (per-task cycle, sub-agent delegation, adequacy review, Verifier, discrimination sensor).

**If the skill cannot be activated, STOP and tell the user — do not proceed without it.**

---

**Design**: `.specs/features/card-catalogo-agrupamento/design.md`
**Status**: Approved

---

## Test Coverage Matrix

> Gerado a partir do código (site `cygnusforge/site` é HTML/CSS/JS estático, sem `package.json`,
> sem test runner, sem build step) e confirmado com o usuário — nenhuma guideline automatizada
> aplicável a este stack; verificação é end-to-end via navegador, com evidência capturada pela
> skill `e2e-verification` (Playwright), conforme decisão do usuário nesta fase.

| Code Layer | Required Test Type | Coverage Expectation | Location Pattern | Run Command |
| --- | --- | --- | --- | --- |
| Markup/CSS/JS do site (card + filtro integrados numa página) | e2e (Playwright, evidência capturada) | Cada chip de categoria filtra corretamente; chip "Todos" restaura tudo; categoria com 0 itens mostra estado vazio; chips quebram linha em <480px; todos os cards visíveis com JS desabilitado; zero erros no console | `site/css/catalog.css`, `site/js/catalog-filter.js`, `site/index.html`, `site/artigos/index.html` | Skill `e2e-verification` (Playwright), servindo `site/` via `python -m http.server` |
| CSS/JS isolado, ainda não linkado em nenhuma página | none (verificação adiada — ver "Resolving compilation dependencies") | — | `site/css/catalog.css`, `site/js/catalog-filter.js` (nas tasks que os criam, antes de serem referenciados por um `<link>`/`<script>`) | build gate only |
| Documentação do design system (novos `.md` e `Exemplos-HTML`) | none | Revisão de conteúdo/consistência com arquivos irmãos já existentes — Markdown estático, sem build/test automatizado | `02-Componentes/15-Cards-Catalogo.md`, `03-Layout/04-Agrupamento-Filtro.md`, `Exemplos-HTML/cards-catalogo.html`, `_index.md` | build gate only |

## Gate Check Commands

| Gate Level | When to Use | Command |
| --- | --- | --- |
| Quick | Tasks que criam/editam CSS ou JS ainda não linkado em nenhuma página (T1, T2, T3) e docs Markdown (T8, T9) | Revisão visual do arquivo (sintaxe válida, nomes de classe/atributo batem com o contrato do design.md) — nenhuma página para abrir ainda |
| Full | Tasks que alteram markup visível dos cards/chips ou comportamento de filtro (T4, T5, T6, T7) | Skill `e2e-verification`: servir `site/` via `python -m http.server`, abrir a página afetada, clicar em cada chip de categoria + "Todos" + a categoria com 0 itens, redimensionar para <480px, recarregar com JS desabilitado — capturar screenshot + `flow.md`; zero erros no console |
| Build | Fim de cada fase | Full gate (quando aplicável à fase) + revisão visual das seções não tocadas da página (ex.: Sobre, Serviços, Tecnologias, Contato em `index.html`) confirmando zero regressão |

---

## Execution Plan

Phases are ordered and run sequentially — each phase completes before the next begins, and tasks within a phase execute in order.

### Phase 1: Fundação CSS/JS compartilhada

```
T1 → T2 → T3
```

### Phase 2: Aplicação em Produtos (index.html)

```
T4 → T5
```

### Phase 3: Aplicação em Artigos (artigos/index.html)

```
T6 → T7
```

### Phase 4: Documentação no design system

```
T8, T9 → T10
```

---

## Task Breakdown

### T1: Criar catalog.css — estrutura do Card de Catálogo

**What**: Criar `site/css/catalog.css` com tokens visuais locais (`--cc-*`, autocontidos — AD-001) e as classes `.catalog-grid`, `.catalog-card` + elementos (`__header`, `__icon`, `__title`, `__tagline`, `__desc`, `__tags`, `__status`, `__footer`, `__cta`) e modificadores `.catalog-card--product` / `.catalog-card--article`, reproduzindo a estrutura visual hoje espalhada em `.product-card`/`.article-card`.
**Where**: `site/css/catalog.css` (novo)
**Depends on**: None
**Reuses**: `site/css/style.css:488-655` (`.product-card`), `site/artigos/style.css:214-231` (`.article-card`)
**Requirement**: CAT-01, CAT-02, CAT-03, CAT-04

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] Arquivo criado com tokens `--cc-*` no topo (nenhuma leitura de `--primary`/`--blue`)
- [x] `.catalog-grid`, `.catalog-card` e todos os elementos/modificadores do design.md definidos
- [x] Nenhuma referência a `.product-card`/`.article-card` dentro do novo arquivo

**Tests**: none (arquivo ainda não linkado em nenhuma página — ver Test Coverage Matrix)
**Gate**: quick

**Commit**: `feat(site): criar catalog.css com estrutura do card de catálogo`

**Status**: ✅ Concluída — commit `ae0a8e7`. Nota: adicionados também `.catalog-card__features` (lista de bullets, distinto de `__tags`) e modificadores `--offwork/--forge/--training/--guardian` para o gradiente de header por produto — necessários para reproduzir fielmente o visual atual, não estavam nominalmente na lista de elementos do design.md.

---

### T2: Adicionar filtro e estado vazio a catalog.css

**What**: Adicionar a `site/css/catalog.css` as classes `.category-filter`, `.category-filter__chip` (+ estado `.is-active`), `.category-filter__count`, `.catalog-empty`, e a regra responsiva de `flex-wrap` para os chips em telas <480px (AD-003, GRP-06).
**Where**: `site/css/catalog.css` (modifica)
**Depends on**: T1
**Reuses**: Padrão de `.chip` existente em `site/artigos/style.css:163-169` como referência visual (não reaproveitado como classe — nome novo para evitar colisão, ver design.md)
**Requirement**: GRP-01, GRP-03, GRP-04, GRP-06

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] `.category-filter`, `.category-filter__chip`, `.category-filter__chip.is-active`, `.category-filter__count`, `.catalog-empty` definidos
- [x] Media query `max-width: 480px` aplica `flex-wrap: wrap` aos chips
- [x] `.catalog-empty` tem `display:none` por padrão (controlado via atributo `hidden`, não via classe)

**Tests**: none (ainda não linkado)
**Gate**: quick

**Commit**: `feat(site): adicionar filtro e estado vazio a catalog.css`

**Status**: ✅ Concluída — commit `fb41a66`.

---

### T3: Criar catalog-filter.js

**What**: Criar `site/js/catalog-filter.js` — no estilo IIFE + `'use strict'` de `js/main.js` — que em `DOMContentLoaded` varre todo `[data-catalog]`, lê `data-catalog-categories` (JSON), conta cards por `data-category` dentro de `[data-catalog-grid]`, gera os chips (incluindo "Todos") com contador, aplica `hidden` nos cards não correspondentes ao clique, marca `.is-active`/`aria-pressed` no chip ativo, e mostra/esconde `[data-catalog-empty]` conforme a contagem de cards visíveis. Inclui `try/catch` no parse do JSON (AD-002, AD-003).
**Where**: `site/js/catalog-filter.js` (novo)
**Depends on**: T2
**Reuses**: Estilo de código de `site/js/main.js` (IIFE, `'use strict'`, sem dependências externas)
**Requirement**: GRP-01, GRP-02, GRP-03, GRP-04, GRP-05

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] Script implementa exatamente o contrato de markup do design.md (`data-catalog`, `data-catalog-grid`, `data-catalog-categories`, `data-category`, `data-catalog-filter`, `data-catalog-empty`)
- [x] Múltiplos `[data-catalog]` na mesma página funcionam de forma independente (escopo local, sem vazamento entre catálogos)
- [x] JSON malformado em `data-catalog-categories` não quebra o restante da página (apenas não gera chips para aquele catálogo)

**Tests**: none (ainda não linkado em nenhuma página)
**Gate**: quick

**Commit**: `feat(site): criar catalog-filter.js`

**Status**: ✅ Concluída — commit `0e565ad`. Nota: `data-catalog-filter`/`data-catalog-empty` são criados automaticamente pelo script quando ausentes do HTML (usado em T4/T6 — markup fica mais limpo, sem placeholders vazios manuais).

---

### T4: Migrar seção Produtos (index.html) para o Card de Catálogo

**What**: Em `site/index.html`, adicionar `<link rel="stylesheet" href="css/catalog.css">` e `<script src="js/catalog-filter.js" defer></script>`; envolver a grid de produtos com `[data-catalog]`/`[data-catalog-grid]` declarando `data-catalog-categories='[{"key":"produto","label":"Produto"},{"key":"treinamento","label":"Treinamento"},{"key":"ferramenta-dev","label":"Ferramenta Dev"}]'`; trocar a classe de cada card de `.product-card` para `.catalog-card .catalog-card--product` mantendo o conteúdo interno (ícone, tagline, features, tech tags, badge de status); marcar `data-category` em cada card conforme o mapeamento do design.md (OffWork=`produto`, ForgeFinance=`produto`, Treinamento IA=`treinamento`, Code Guardian=`ferramenta-dev`); adicionar placeholder `[data-catalog-empty]`.
**Where**: `site/index.html` (modifica, seção `#produtos`)
**Depends on**: T3
**Reuses**: `catalog.css`/`catalog-filter.js` (T1-T3); mantém o conteúdo textual (descrições, features, tech tags) de cada produto inalterado
**Requirement**: CAT-01, CAT-02, GRP-01, GRP-02, GRP-03, GRP-06, SITE-02, SITE-03

**Tools**:
- MCP: NONE
- Skill: `e2e-verification`

**Done when**:
- [x] Os 4 produtos renderizam com o novo card, badge de status (`Em desenvolvimento`/`Disponível`) continua visível independente do filtro ativo
- [x] Chips "Todos", "Produto", "Treinamento", "Ferramenta Dev" aparecem com contador correto (4/2/1/1)
- [x] Clicar em cada chip filtra os produtos corretamente; "Todos" restaura os 4
- [x] Em <480px, chips quebram linha sem scroll horizontal
- [x] Gate check passa: Full gate (skill `e2e-verification`) — screenshot + `flow.md` anexados

**Tests**: e2e (Playwright via `e2e-verification`)
**Gate**: full

**Commit**: `feat(site): migrar seção Produtos para o card de catálogo com filtro`

**Status**: ✅ Concluída — commit `a7acf35`. Evidência: `.claude/quality/evidence/card-catalogo-agrupamento/T4/` (3 screenshots + flow.md).

---

### T5: Remover CSS antigo de Produtos e atualizar fade-in

**What**: Remover de `site/css/style.css` as regras agora redundantes (`.products-grid`, `.product-card` e todos os `.product-card__*`, `.product-card--offwork/--forge/--training/--guardian`, `.product-status` e variantes); em `site/js/main.js`, atualizar `initFadeInElements()` trocando `.product-card` por `.catalog-card` na lista de seletores (design.md — Risks & Concerns).
**Where**: `site/css/style.css` (modifica), `site/js/main.js` (modifica)
**Depends on**: T4
**Reuses**: N/A (remoção)
**Requirement**: CAT-04

**Tools**:
- MCP: NONE
- Skill: `e2e-verification`

**Done when**:
- [x] Nenhuma regra `.product-card*`/`.products-grid`/`.product-status*` restante em `style.css`
- [x] `initFadeInElements()` referencia `.catalog-card` (não mais `.product-card`)
- [x] Seção Produtos continua idêntica visualmente após a remoção (nenhuma regra em uso foi apagada por engano)
- [x] Fade-in ao rolar até a seção Produtos continua funcionando
- [x] Gate check passa: Full gate (re-executar o mesmo fluxo de T4) — screenshot + `flow.md` anexados

**Tests**: e2e (Playwright via `e2e-verification` — regressão)
**Gate**: full

**Commit**: `chore(site): remover CSS antigo de product-card e atualizar fade-in`

**Status**: ✅ Concluída — commit `20920d1`. Evidência: `.claude/quality/evidence/card-catalogo-agrupamento/T5/` (screenshot + flow.md). Também removida a regra responsiva órfã `.products-grid { grid-template-columns: 1fr; }` do media query (não listada explicitamente na task, mas coberta pelo "Done when" — `.products-grid` não pode restar em nenhum lugar do arquivo).

---

### T6: Migrar Artigos (artigos/index.html) para o Card de Catálogo

**What**: Em `site/artigos/index.html`, remover a seção "Temas" decorativa (`.theme-grid`/`.theme-card`, incluindo os 3 cards e seus badges `theme-status`); adicionar `<link rel="stylesheet" href="../css/catalog.css">` e `<script src="../js/catalog-filter.js" defer></script>`; envolver a listagem de artigos com `[data-catalog]`/`[data-catalog-grid]` declarando `data-catalog-categories` com os 4 temas (`uso-de-ia`, `desenvolvimento`, `gestao-de-projetos`, `processos`); marcar o artigo existente ("IA nas empresas...") com `data-category="uso-de-ia"`; trocar a classe do card de `.article-card` para `.catalog-card .catalog-card--article`, preservando ícone/título/descrição/meta; adicionar placeholder `[data-catalog-empty]`.
**Where**: `site/artigos/index.html` (modifica)
**Depends on**: T3
**Reuses**: `catalog.css`/`catalog-filter.js` (T1-T3)
**Requirement**: CAT-01, CAT-03, GRP-01, GRP-02, GRP-03, GRP-04, GRP-06, SITE-01

**Tools**:
- MCP: NONE
- Skill: `e2e-verification`

**Done when**:
- [x] Seção "Temas" antiga removida da página
- [x] Chips com contador correto — na prática 2 artigos existiam no momento da execução (não 1, como no Design): "Todos" (2), "Uso de IA" (1), "Desenvolvimento" (1), "Gestão de Projetos" (0), "Processos" (0)
- [x] Clicar em "Uso de IA" mostra só o artigo de IA; clicar em "Gestão de Projetos" (0 itens) mostra o estado vazio (`.catalog-empty`); "Todos" restaura os 2
- [x] Em <480px, chips quebram linha sem scroll horizontal
- [x] Gate check passa: Full gate (skill `e2e-verification`→`playwright-skill`) — 3 screenshots + `flow.md` anexados

**Tests**: e2e (Playwright via `e2e-verification`)
**Gate**: full

**Commit**: `feat(site): migrar Artigos para o card de catálogo com filtro, remover seção Temas`

**Status**: ✅ Concluída — commit `40ffe77`. Desvio anotado: entre o Design e o Execute desta feature, um segundo artigo ("Harness multiagente...") foi publicado no site (fora desta feature, commits anteriores do repositório) e já vinha com sua categoria explícita no `section-label` original ("Desenvolvimento") — categorizado como `desenvolvimento` sem ambiguidade. Evidência: `.claude/quality/evidence/card-catalogo-agrupamento/T6/` (3 screenshots + flow.md).

---

### T7: Remover CSS antigo de Artigos

**What**: Remover de `site/artigos/style.css` as regras agora redundantes (`.theme-grid`, `.theme-card` e `.theme-card__*`, `.theme-status` e variantes, `@keyframes pulse-dot` se não usado em mais nada, `.article-card` e `.article-card__*`, `.chip` **apenas se** não for mais usado em `.hero-chips` — conferir antes de remover).
**Where**: `site/artigos/style.css` (modifica)
**Depends on**: T6
**Reuses**: N/A (remoção)
**Requirement**: CAT-04

**Tools**:
- MCP: NONE
- Skill: `e2e-verification`

**Done when**:
- [x] Nenhuma regra `.theme-grid`/`.theme-card*`/`.theme-status*`/`.article-card*` restante que não seja mais referenciada no HTML (confirmado via grep antes de remover — nenhuma outra referência no arquivo)
- [x] `.chip`/`.hero-chips` preservados (ainda usados no hero da página, fora do escopo desta migração)
- [x] Página continua idêntica visualmente após a remoção (nenhuma regra em uso foi apagada por engano)
- [x] Gate check passa: Full gate (reexecução idêntica do fluxo de T6) — screenshot + `flow.md` anexados, resultado idêntico

**Tests**: e2e (Playwright via `e2e-verification` — regressão)
**Gate**: full

**Commit**: `chore(site): remover CSS antigo de theme-card/article-card`

**Status**: ✅ Concluída — commit `fad14be`. Nota operacional: `site/artigos/style.css` já tinha mudanças não commitadas e não relacionadas a esta feature (estilos de tabela em `.prose` e ajuste de `.mermaid-block__visual`) antes de eu começar a editar — guardadas com `git stash push -- site/artigos/style.css` para não misturar no commit desta task, e devolvidas ao working tree com `git stash pop` (auto-merge limpo) logo após o commit, preservando esse trabalho em progresso do usuário exatamente como estava. Evidência: `.claude/quality/evidence/card-catalogo-agrupamento/T7/` (3 screenshots + flow.md).

---

### T8: Documentar Card de Catálogo no design system

**What**: Criar `02-Componentes/15-Cards-Catalogo.md` seguindo o formato de `03-Cards.md` (índice, anatomia com diagrama ASCII, variantes Produto/Artigo, tokens, regras de uso), deixando explícito que é distinto do card de aplicação (`03-Cards.md`) — para catálogo/conteúdo filtrável.
**Where**: `02-Componentes/15-Cards-Catalogo.md` (novo)
**Depends on**: T5, T7
**Reuses**: Estrutura/formato de `02-Componentes/03-Cards.md`; classes reais definidas em `site/css/catalog.css` (já validadas em produção pelas tasks anteriores)
**Requirement**: DOC-01

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] Anatomia do `.catalog-card` documentada com as duas variantes (`--product`, `--article`)
- [x] Diferença explícita frente ao card de app (`03-Cards.md`) registrada
- [x] Tabela de tokens (`--cc-*`) documentada

**Tests**: none
**Gate**: quick (revisão de conteúdo)

**Commit**: `docs: adicionar padrão de Card de Catálogo (02-Componentes/15)`

**Status**: ✅ Concluída — arquivo criado em `02-Componentes/15-Cards-Catalogo.md` (sem commit, repositório sem git). Também atualizado o link "Próximo" de `14-Breadcrumb.md` para fechar a cadeia de navegação.

---

### T9: Documentar Agrupamento com Filtro por Categoria

**What**: Criar `03-Layout/04-Agrupamento-Filtro.md` documentando o padrão de chips + filtro: contrato de markup (`data-catalog`, `data-catalog-categories`, `data-category`, etc.), comportamento (contagem em runtime, progressive enhancement sem JS, responsivo mobile, estado vazio), e um passo a passo de "como aplicar num catálogo novo" (AD-002, AD-003).
**Where**: `03-Layout/04-Agrupamento-Filtro.md` (novo)
**Depends on**: T5, T7
**Reuses**: `site/js/catalog-filter.js` (contrato real já validado em produção)
**Requirement**: DOC-01

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] Contrato de markup documentado com exemplo de `data-catalog-categories` JSON
- [x] AD-002 e AD-003 referenciados como as regras que este padrão segue
- [x] Passo a passo de aplicação em catálogo futuro (ex.: página de skills)

**Tests**: none
**Gate**: quick (revisão de conteúdo)

**Commit**: `docs: adicionar padrão de Agrupamento com Filtro por Categoria (03-Layout/04)`

**Status**: ✅ Concluída — arquivo criado em `03-Layout/04-Agrupamento-Filtro.md` (sem commit, repositório sem git). Também atualizado o link "Próximo" de `03-Receitas-Tela.md` para fechar a cadeia de navegação.

---

### T10: Exemplo HTML e atualização do índice

**What**: Criar `Exemplos-HTML/cards-catalogo.html` com um exemplo standalone funcional (2-3 categorias fictícias, cards de exemplo, filtro funcionando) linkando `catalog.css`/reproduzindo `catalog-filter.js` (ou copiando-os localmente, conforme o padrão já usado pelos outros arquivos em `Exemplos-HTML/`); adicionar as duas entradas novas (`02-Componentes/15-Cards-Catalogo.md`, `03-Layout/04-Agrupamento-Filtro.md`) em `_index.md`.
**Where**: `Exemplos-HTML/cards-catalogo.html` (novo), `_index.md` (modifica)
**Depends on**: T8, T9
**Reuses**: Padrão de estrutura dos demais arquivos em `Exemplos-HTML/` (ex.: `cards.html`)
**Requirement**: DOC-01

**Tools**:
- MCP: NONE
- Skill: NONE

**Done when**:
- [x] Exemplo abre no navegador e o filtro funciona de fato (não é só markup estático ilustrativo) — verificado via `playwright-skill`: 2 catálogos independentes na mesma página, filtro e estado vazio confirmados por automação, zero erros de console
- [x] `_index.md` referencia os dois novos arquivos na mesma convenção `@caminho/arquivo.md` já usada
- [x] Gate check passa: aberto via `python -m http.server` e verificado com Playwright (chips corretos, filtro funcional, estado vazio)

**Tests**: none (exemplo isolado, verificação visual direta — não faz parte do site em produção)
**Gate**: quick

**Commit**: `docs: adicionar exemplo interativo de card de catálogo e indexar no design system`

**Status**: ✅ Concluída — arquivos criados em `Exemplos-HTML/cards-catalogo.html` e `_index.md` atualizado (sem commit, repositório sem git). Esta é a última task da feature.

---

## Phase Execution Map

```
Phase 1 → Phase 2 → Phase 3 → Phase 4

Phase 1:  T1 ──→ T2 ──→ T3
Phase 2:               T3 ──→ T4 ──→ T5
Phase 3:               T3 ──→ T6 ──→ T7
Phase 4:     T5, T7 ──→ T8
             T5, T7 ──→ T9
                  T8, T9 ──→ T10
```

Execution is strictly sequential — there is no intra-phase parallelism. A single agent (or batch worker) works one task at a time, in order.

**Total: 10 tasks.** Acima do limite inline (~8) — ao iniciar Execute, será feita a oferta de dividir em sub-agentes por lote de fases (ver Sub-Agent Delegation na SKILL.md). Sugestão de corte: Lote 1 = Fase 1+2 (5 tasks), Lote 2 = Fase 3+4 (5 tasks) — nunca corta uma fase ao meio.

---

## Task Granularity Check

| Task | Scope | Status |
| --- | --- | --- |
| T1: Criar catalog.css — card | 1 arquivo, 1 conceito (estrutura do card) | ✅ Granular |
| T2: Adicionar filtro/vazio a catalog.css | 1 arquivo, 1 conceito (filtro + estado vazio) | ✅ Granular |
| T3: Criar catalog-filter.js | 1 arquivo, 1 componente | ✅ Granular |
| T4: Migrar Produtos | 1 arquivo (`index.html`), 1 seção coesa | ✅ Granular |
| T5: Remover CSS antigo de Produtos + fade-in | 2 arquivos, mas uma única ação coesa (limpeza pós-migração) | ⚠️ OK — coeso |
| T6: Migrar Artigos | 1 arquivo (`artigos/index.html`), 1 seção coesa | ✅ Granular |
| T7: Remover CSS antigo de Artigos | 1 arquivo, 1 ação coesa (limpeza pós-migração) | ✅ Granular |
| T8: Doc Card de Catálogo | 1 arquivo | ✅ Granular |
| T9: Doc Agrupamento/Filtro | 1 arquivo | ✅ Granular |
| T10: Exemplo HTML + índice | 2 arquivos, mas uma única ação coesa (publicar o exemplo e indexá-lo) | ⚠️ OK — coeso |

**Granularity check**: Nenhuma task excede "2-3 coisas relacionadas no mesmo objetivo" — sem violações.

---

## Diagram-Definition Cross-Check

| Task | Depends On (task body) | Diagram Shows | Status |
| --- | --- | --- | --- |
| T1 | None | (nenhuma seta de entrada) | ✅ Match |
| T2 | T1 | T1 → T2 | ✅ Match |
| T3 | T2 | T2 → T3 | ✅ Match |
| T4 | T3 | T3 → T4 | ✅ Match |
| T5 | T4 | T4 → T5 | ✅ Match |
| T6 | T3 | T3 → T6 | ✅ Match |
| T7 | T6 | T6 → T7 | ✅ Match |
| T8 | T5, T7 | T5, T7 → T8 | ✅ Match |
| T9 | T5, T7 | T5, T7 → T9 | ✅ Match |
| T10 | T8, T9 | T8, T9 → T10 | ✅ Match |

**Rules check**: nenhuma task depende de uma task de fase posterior — todas as setas apontam para trás ou dentro da mesma fase. ✅

---

## Test Co-location Validation

| Task | Code Layer Created/Modified | Matrix Requires | Task Says | Status |
| --- | --- | --- | --- | --- |
| T1: catalog.css (card) | CSS isolado, não linkado | none | none | ✅ OK |
| T2: catalog.css (filtro) | CSS isolado, não linkado | none | none | ✅ OK |
| T3: catalog-filter.js | JS isolado, não linkado | none | none | ✅ OK |
| T4: Migrar Produtos | Markup/CSS/JS integrados (primeira vez visível) | e2e | e2e | ✅ OK |
| T5: Remover CSS antigo Produtos | Markup/CSS/JS integrados (regressão) | e2e | e2e | ✅ OK |
| T6: Migrar Artigos | Markup/CSS/JS integrados (primeira vez visível) | e2e | e2e | ✅ OK |
| T7: Remover CSS antigo Artigos | Markup/CSS/JS integrados (regressão) | e2e | e2e | ✅ OK |
| T8: Doc Card de Catálogo | Documentação Markdown | none | none | ✅ OK |
| T9: Doc Agrupamento/Filtro | Documentação Markdown | none | none | ✅ OK |
| T10: Exemplo HTML + índice | Documentação (exemplo isolado) | none | none | ✅ OK |

**Resolving compilation dependencies aplicado**: T1-T3 criam CSS/JS que não são testáveis até serem linkados numa página real — os testes e2e foram movidos para frente (merge forward) para T4 e T6, que são as tasks que tornam o código visível/interativo pela primeira vez. Nenhuma task fica com código não verificado permanentemente — T5 e T7 (limpeza) re-executam o mesmo gate e2e como checagem de regressão.

**Nenhuma violação.** ✅

---

## Tools Summary (pergunta obrigatória antes do Execute)

- **MCP**: nenhum MCP aplicável a este stack (site estático sem backend/API) — todas as tasks usam `NONE`.
- **Skills**: `e2e-verification` (Playwright) nas tasks T4-T7 (as que tornam comportamento visível/interativo), conforme decisão do usuário nesta fase. Nenhuma outra skill do harness se aplica (não é C#/.NET, não é React — `design-system-cygnusforge` não cobre HTML/CSS/JS estático).
