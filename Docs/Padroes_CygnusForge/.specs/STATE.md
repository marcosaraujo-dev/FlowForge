# STATE

## Decisions

### AD-001
- **Decision**: Componentes de catálogo/conteúdo (cards, filtros) usam tokens visuais próprios e autocontidos, nunca acoplados aos nomes de variável de cor de uma página específica do site (ex.: `--primary` em `css/style.css`, `--blue` em `artigos/style.css`).
- **Reason**: O site `cygnusforge/site` já tem múltiplos sistemas de tokens independentes por página/seção (home, artigos, treinamento-ia-analistas); acoplar um componente compartilhado ao nome de variável de uma página específica quebra silenciosamente se essa página renomear/remover a variável.
- **Trade-off**: Um pequeno conjunto de tokens próprios (ex. `--cc-accent`) fica duplicado em valor (mesmo hex) em vez de referenciar uma fonte única — aceitável porque o valor de marca (#184194) é estável e raramente muda.
- **Scope**: Qualquer componente de catálogo/conteúdo futuro no site `cygnusforge` (produtos, artigos, skills, treinamentos) e no design system deste repositório (`02-Componentes/`).
- **Date**: 2026-09-14
- **Status**: active

### AD-002
- **Decision**: Categorias e contadores de qualquer catálogo/grid filtrável são declarados via atributo `data-*` no HTML (nunca hardcoded no texto do chip) e a contagem por categoria é sempre calculada em runtime por JS.
- **Reason**: Evita contadores dessincronizados do conteúdo real quando um item é adicionado/removido — a fonte da verdade é o DOM, não um número escrito à mão.
- **Trade-off**: Nenhum contador aparece sem JS habilitado (aceito — ver AD-003 sobre progressive enhancement).
- **Scope**: Qualquer grid filtrável futuro no site `cygnusforge`.
- **Date**: 2026-09-14
- **Status**: active

### AD-003
- **Decision**: Em grids filtráveis do site, a barra de chips de filtro é inteiramente gerada via JS (não existe markup de chip no HTML estático) e nenhum card é escondido por padrão no HTML.
- **Reason**: É o mecanismo mais simples para garantir que o site funcione sem JavaScript (progressive enhancement) sem precisar de classes `no-js`/`js` ou lógica condicional extra — sem JS, não há chips para clicar, e todo conteúdo já está visível.
- **Trade-off**: A barra de filtro "pisca" para existir alguns milissegundos após o carregamento da página, em vez de já vir renderizada no HTML (custo aceitável para um site estático de baixo tráfego de interação).
- **Scope**: Qualquer grid filtrável futuro no site `cygnusforge`.
- **Date**: 2026-09-14
- **Status**: active

### AD-004
- **Decision**: O fundo de leitura oficial de qualquer página de conteúdo do site `cygnusforge` é `#F8F9FA` (frio) — nunca um tom quente/creme como o antigo `--sand #f8f6f1`.
- **Reason**: Tom quente destoava do resto do site (inteiramente construído sobre azul frio — header navy, links azuis); `#F8F9FA` já existe como "Sidebar Background" no Design System oficial (`01-Fundamentos/02-Cores.md`) e como `--bg-alt` em `css/style.css` — reaproveitado, não inventado. Nota: a razão de contraste WCAG do tom antigo já era 16.53:1 (bem acima de AAA) — a melhora é de harmonia de paleta, não de acessibilidade que estivesse quebrada.
- **Trade-off**: Nenhum identificado.
- **Scope**: Qualquer página de conteúdo (artigo, treinamento, doc) futura no site `cygnusforge`.
- **Date**: 2026-09-14
- **Status**: active

### AD-005
- **Decision**: A fonte de qualquer página do site `cygnusforge` é Segoe UI/system-ui — nunca uma fonte via Google Fonts (Syne/Inter foram removidas de `treinamento-ia-analistas`).
- **Reason**: Consistência com o Design System oficial e o resto do ecossistema CygnusForge (WPF já usa Segoe UI); elimina a dependência de rede externa.
- **Trade-off**: Nenhum identificado — Segoe UI/system-ui é zero-custo e já era usada em 2 das 3 stylesheets do site.
- **Scope**: Qualquer página futura no site `cygnusforge`.
- **Date**: 2026-09-14
- **Status**: active

## Handoff

- **Feature**: unificacao-tokens-visuais-site (`.specs/features/unificacao-tokens-visuais-site/`)
- **Phase / Task**: Concluída — Specify → Design → Execute inline (Tasks formal pulado, escopo caiu para pequeno após investigação) → Validate (PASS ✅)
- **Completed**: card-catalogo-agrupamento (feature anterior, completa) + unificacao-tokens-visuais-site (completa)
- **In-progress** (file:line): nenhum — ambas as features concluídas
- **Next step**: nenhum pendente; fora de escopo registrado para o futuro (se o usuário quiser depois): redesenho de layout estilo techleads.club (hero com badge/CTA de código, cards com ícone em chip) — ver spec.md da feature de tokens, tabela Out of Scope
- **Blockers**: nenhum
- **Uncommitted files**: nenhum pendente de push. `Padroes_CygnusForge`: sincronizado até `217638d`. `cygnusforge` (site): sincronizado até `88f50f7` (push feito a pedido do usuário — "Suba tudo"). Working tree do `cygnusforge` ainda tem mudanças não relacionadas e não commitadas do próprio usuário (`site/artigos/harness/harness-multiagente.html`, trechos de `site/artigos/style.css` sobre tabelas/mermaid) — não tocar, são trabalho em andamento dele.
- **Branch**: `Padroes_CygnusForge`: `master`, sincronizado com `origin/master` até `217638d`. `cygnusforge`: `master`, sincronizado com `origin/master` até `88f50f7`.
