# Padrão de Card de Catálogo e Agrupamento por Categoria — Specification

## Problem Statement

O Design System atual (`02-Componentes/03-Cards.md`) cobre apenas cards de aplicação interna
(dashboard/formulário, estilo WPF/app de linha de negócio) e não define um padrão para
**cards de catálogo/conteúdo** (produtos, artigos). O site institucional (`cygnusforge/site`)
reflete essa lacuna: `product-card`, `service-card`, `theme-card` e `article-card` são quatro
implementações CSS distintas e não compartilhadas para o mesmo conceito, e não existe nenhum
mecanismo de agrupamento/filtro por categoria — o `theme-card` de Artigos hoje é puramente
decorativo. Referência de inspiração: https://agent-skills.techleads.club/ (card padronizado
reutilizável + chips de categoria com contador + filtro funcional).

## Goals

- [ ] Documentar um componente **Card de Catálogo** reutilizável (anatomia, variantes, tokens) em `02-Componentes/`, distinto do card de app já existente
- [ ] Documentar um padrão **Grupo com Filtro por Categoria** (chips + contador + filtro) em `03-Layout/`, inexistente hoje no design system
- [ ] Aplicar os dois padrões em `artigos/index.html` e `index.html` (seção `#produtos`) do site `cygnusforge`, com filtro funcional em JS puro (sem framework, sem build step)
- [ ] Eliminar duplicação de CSS entre `product-card` e `article-card`, substituindo-os por uma base compartilhada

## Out of Scope

Explicitly excluded. Documented to prevent scope creep.

| Feature | Reason |
| --- | --- |
| `service-card` (seção Serviços da home) | Conteúdo institucional estático (4 ofertas fixas), não é catálogo filtrável — decisão do usuário |
| Unificação de tokens de cor/tipografia entre este design system (WPF/app interno) e o CSS do site (`--primary #184194` etc.) | Sistemas de token permanecem independentes; só o padrão de card/agrupamento é compartilhado |
| CMS/gerenciamento dinâmico de categorias | Categorias são estáticas no HTML — site não tem backend |
| Paginação | Volume atual de itens (4 produtos, 1 artigo) não justifica |
| Busca textual | Só filtro por categoria (chips) nesta feature |
| Ordenação (Featured/Name/Recent, como no techleads.club) | Fora de escopo agora — pode virar feature futura |
| Multi-categoria por item (um produto/artigo em 2+ categorias) | Cada item pertence a exatamente 1 categoria nesta versão |

---

## Assumptions & Open Questions

Every ambiguity is resolved or recorded here — nothing is left silently unclear.

| Assumption / decision | Chosen default | Rationale | Confirmed? |
| --- | --- | --- | --- |
| Eixo de filtro em Produtos | **Tipo de produto** (Produto / Treinamento / Ferramenta Dev), não status | Usuário: status converge para "Disponível" conforme produtos amadurecem (+ 2 produtos futuros ainda não publicados) — status sozinho vira filtro fraco a médio prazo; badge de status (`product-status`) é mantido como está, só deixa de ser o eixo de filtro | y |
| Categorias de Produtos | `Produto` (OffWork, ForgeFinance), `Treinamento` (Treinamento IA), `Ferramenta Dev` (Code Guardian) | Opção recomendada aceita pelo usuário; escala bem para os 2 produtos futuros | y |
| Categorias de Artigos | Reaproveitar os 4 temas já usados visualmente no `theme-card` hoje: Uso de IA, Desenvolvimento, Gestão de Projetos, Processos | Evita nova taxonomia; usuário já validou esses nomes ao criar o `theme-card` original | y |
| `service-card` fora de escopo | Não entra no novo padrão | Decisão explícita do usuário | y |
| Contador por chip (ex. "Produto (2)") | Incluído no MVP (P1), não como nice-to-have | Elemento central da inspiração (techleads.club) e trivial de calcular (contagem de itens por categoria) | y |
| Comportamento dos chips em mobile (<480px) | `wrap` (quebra de linha), não scroll horizontal | Poucas categorias por seção (3–4), todas cabem visíveis sem interação extra | y |
| Metadado "última atualização" (presente no techleads.club) | Não adotado para Produtos (sem data natural de "atualização"); Artigos mantêm o metadado de data que `article-card__meta` já exibe hoje (não é campo novo) | Não existe fonte de dado de "atualizado em" para produtos; artigos já têm data de publicação | y |
| Categoria sem nenhum item (ex. "Desenvolvimento" e "Gestão de Projetos" em Artigos, hoje com 0 artigos) | Chip aparece mesmo assim, mostrando `(0)`, clicável, leva a estado vazio | Comunica ao visitante que a categoria existe e virá conteúdo, em vez de escondê-la | y |
| Progressive enhancement sem JS | Todos os cards ficam visíveis (sem filtro aplicado) se JS falhar/estiver desabilitado | Site é estático hoje — filtro é um enhancement, não pode ser o único caminho de acesso ao conteúdo | y |

**Open questions:** none — todas resolvidas ou registradas acima.

---

## User Stories

### P1: Filtro por categoria em Artigos ⭐ MVP

**User Story**: Como visitante do site, quero filtrar artigos por tema/categoria para achar rapidamente o conteúdo que me interessa.

**Why P1**: É o caso de uso mais direto da inspiração (techleads.club) e a página com estrutura mais simples para validar o padrão antes de aplicar em Produtos.

**Acceptance Criteria**:

1. WHEN a página de Artigos carrega THEN o sistema SHALL exibir chips de categoria (Todos + Uso de IA + Desenvolvimento + Gestão de Projetos + Processos), cada um com contador de itens.
2. WHEN o usuário clica em um chip de categoria THEN o sistema SHALL exibir somente os `article-card` daquela categoria e ocultar os demais, sem recarregar a página.
3. WHEN o usuário clica no chip "Todos" THEN o sistema SHALL exibir todos os artigos novamente.
4. WHEN o chip de uma categoria está selecionado THEN o sistema SHALL indicar visualmente o estado ativo (diferente dos demais chips).
5. WHEN uma categoria não possui nenhum artigo (ex.: Desenvolvimento, Gestão de Projetos hoje) THEN o sistema SHALL exibir o chip com contador "(0)" e, se clicado, um estado vazio (ver P3).

**Independent Test**: Abrir `artigos/index.html`, clicar em "Uso de IA" → só o artigo de IA aparece; clicar em "Todos" → todos os artigos voltam a aparecer.

---

### P1: Card de Catálogo padronizado (base compartilhada) ⭐ MVP

**User Story**: Como visitante, quero reconhecer visualmente o mesmo padrão de card entre Produtos e Artigos, para navegar o site de forma consistente.

**Why P1**: É a fundação estrutural — sem uma base de card compartilhada, o filtro por categoria (outras stories) não tem em que operar de forma consistente.

**Acceptance Criteria**:

1. WHEN a seção Produtos é renderizada THEN cada produto SHALL usar a estrutura do Card de Catálogo (ícone, título, tagline, descrição, badges de tecnologia, badge de status, CTA).
2. WHEN a listagem de Artigos é renderizada THEN cada artigo SHALL usar a mesma base do Card de Catálogo (título, descrição curta, metadado categoria+data, CTA) na variante de conteúdo textual.
3. WHEN os CSS de Produtos e Artigos são comparados THEN ambos SHALL compartilhar a mesma classe base (ex.: `.catalog-card`) e modificadores — sem regras de card duplicadas entre as duas seções.

**Independent Test**: Inspecionar o CSS aplicado a um `product-card` e a um `article-card` — a classe base e as propriedades estruturais (padding, radius, sombra, layout do header) vêm da mesma origem.

---

### P1: Filtro por tipo de produto ⭐ MVP

**User Story**: Como visitante, quero filtrar produtos por tipo (Produto / Treinamento / Ferramenta Dev) para focar no que me interessa.

**Why P1**: Replica em Produtos o mesmo mecanismo validado em Artigos — sem isso a home não se beneficia do novo padrão de agrupamento.

**Acceptance Criteria**:

1. WHEN a seção Produtos carrega THEN o sistema SHALL exibir chips de categoria (Todos + Produto + Treinamento + Ferramenta Dev), cada um com contador.
2. WHEN o usuário clica em um chip de tipo THEN o sistema SHALL exibir somente os `product-card` daquele tipo.
3. WHEN um card de produto é exibido, independente do filtro ativo THEN o badge de status (`Em desenvolvimento`/`Disponível`) SHALL permanecer visível no card.

**Independent Test**: Abrir `index.html#produtos`, clicar em "Ferramenta Dev" → só Code Guardian aparece, com seu badge de status visível; clicar em "Todos" → os 4 produtos voltam.

---

### P2: Padrão documentado e reutilizável

**User Story**: Como mantenedor do design system, quero um padrão de "Grupo com Filtro por Chips" documentado com HTML/CSS/JS de referência, para reaproveitar em catálogos futuros (ex.: página de skills/treinamentos) sem redesenhar do zero.

**Why P2**: Não bloqueia o lançamento em Artigos/Produtos, mas é o motivo de este padrão viver em `Padroes_CygnusForge` em vez de só no CSS do site.

**Acceptance Criteria**:

1. WHEN um novo catálogo for criado no futuro THEN a documentação SHALL fornecer markup HTML, classes CSS e o snippet JS de filtro prontos para copiar/adaptar.
2. WHEN o componente é consultado no índice do design system THEN ele SHALL estar listado em `_index.md`.

---

### P3: Estado vazio elegante

**User Story**: Como visitante, quando uma categoria filtrada não tem itens, quero um retorno visual claro em vez de uma área em branco.

**Why P3**: Melhora de polish — o filtro funciona sem isso, mas evita a impressão de página quebrada.

**Acceptance Criteria**:

1. WHEN uma categoria filtrada resulta em zero cards THEN o sistema SHALL exibir uma mensagem de estado vazio (ex.: "Nenhum item nesta categoria ainda").

---

## Edge Cases

- WHEN JavaScript está desabilitado/falha ao carregar THEN o sistema SHALL ainda exibir todos os cards sem filtro aplicado (progressive enhancement — nenhum conteúdo fica inacessível).
- WHEN a tela é mobile (<480px) THEN os chips de categoria SHALL quebrar linha (`flex-wrap`), permanecendo todos visíveis sem exigir scroll horizontal.
- WHEN uma categoria tem 0 itens hoje THEN o chip ainda SHALL aparecer (não é ocultado), mostrando `(0)` e levando a um estado vazio se clicado.
- WHEN o usuário recarrega a página após aplicar um filtro THEN o sistema SHALL resetar para "Todos" (sem persistência de estado entre navegações — não especificado como requisito, comportamento padrão de página estática).

---

## Requirement Traceability

| Requirement ID | Story | Phase | Status |
| --- | --- | --- | --- |
| CAT-01 | P1: Card de Catálogo padronizado | T1, T4, T6 | Implementing |
| CAT-02 | P1: Card de Catálogo padronizado (variante Produto) | T1, T4 | Implementing |
| CAT-03 | P1: Card de Catálogo padronizado (variante Artigo) | T1, T6 | Implementing |
| CAT-04 | P1: Card de Catálogo padronizado (base CSS compartilhada) | T1, T5, T7 | Implementing |
| GRP-01 | P1: Filtro por categoria em Artigos / P1: Filtro por tipo de produto | T2, T3, T4, T6 | Implementing |
| GRP-02 | P1: Filtro por categoria em Artigos / P1: Filtro por tipo de produto | T3, T4, T6 | Implementing |
| GRP-03 | P1: Filtro por categoria em Artigos | T2, T3, T4 | Implementing |
| GRP-04 | P3: Estado vazio elegante | T2, T3, T6 | Implementing |
| GRP-05 | Edge Cases (progressive enhancement) | T3 | Implementing |
| GRP-06 | Edge Cases (mobile wrap) | T2, T4, T6 | Implementing |
| SITE-01 | P1: Filtro por categoria em Artigos | T6, T7 | Implementing |
| SITE-02 | P1: Filtro por tipo de produto | T4, T5 | Implementing |
| SITE-03 | P1: Filtro por tipo de produto (badge de status mantido) | T4 | Implementing |
| DOC-01 | P2: Padrão documentado e reutilizável | T8, T9, T10 | Implementing |

**ID format:** `[CATEGORY]-[NUMBER]` — `CAT` = Card de Catálogo, `GRP` = Agrupamento/Filtro, `SITE` = aplicação no site, `DOC` = documentação reutilizável.

**Coverage:** 14 total, 14 implementados (T1-T10), 0 sem mapeamento. Status `Implementing` até o Verifier confirmar (`Verified`).

---

## Success Criteria

- [ ] Visitante consegue filtrar Artigos por categoria e Produtos por tipo, sem recarregar a página
- [ ] `product-card` e `article-card` passam a compartilhar a mesma base CSS (`.catalog-card`), sem regras estruturais duplicadas
- [ ] Padrão documentado em `Padroes_CygnusForge` (`02-Componentes/` + `03-Layout/`) pronto para reuso em catálogo futuro
- [ ] Site funciona corretamente (todos os cards visíveis) mesmo com JS desabilitado
