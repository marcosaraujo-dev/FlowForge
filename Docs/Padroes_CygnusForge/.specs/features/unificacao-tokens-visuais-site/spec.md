# Unificação de Tokens Visuais do Site — Specification

## Problem Statement

O site `cygnusforge/site` tem **3 sistemas de token de cor/fonte independentes e divergentes**:
`css/style.css` (home, Produtos, Serviços — já conforme o Design System oficial), `artigos/style.css`
(paleta parecida mas nomes/valores próprios) e `treinamento-ia-analistas/style.css` (paleta e fonte
completamente diferentes — azul errado, fontes Google em vez de Segoe UI). O usuário notou
concretamente que o fundo de leitura `--sand: #f8f6f1` (usado em Artigos e Treinamento IA) deixa o
texto "meio apagado" — baixo contraste, tom quente — mas a causa raiz é mais ampla: o site não tem
uma identidade visual única, apesar de existir um Design System oficial (`Padroes_CygnusForge`) que
já resolve a maioria dessas decisões e que a home já segue.

## Goals

- [ ] Um único sistema de tokens de cor (Primary, Success, Danger, Warning, Info, Backgrounds, Text, Borders) idêntico ao Design System oficial, usado nas 3 stylesheets do site
- [ ] Fonte única (Segoe UI/system) em todas as páginas, removendo a dependência de Google Fonts (Syne/Inter) em Treinamento IA
- [ ] Fundo de leitura de conteúdo textual (Artigos, Treinamento IA) trocado de `--sand` (#f8f6f1, quente) para `#F8F9FA` (frio, "Sidebar Background" do Design System) — melhora de contraste mensurável
- [ ] Zero regressão visual nas páginas que já seguem o Design System (home, Produtos, Serviços, Code Guardian, ForgeFinance, OffWork)

## Out of Scope

| Feature | Reason |
| --- | --- |
| Recolorir os acentos decorativos por módulo em `treinamento-ia-analistas/index.html` (10 cores diferentes nos cards de módulo, hoje hex inline) | Variedade visual intencional entre módulos de curso, não um token de marca — trocar descaracterizaria a distinção visual sem ganho de legibilidade |
| Recolorir elementos de diagrama/legenda em `04-fluxos.html` | Cores específicas de diagrama técnico, não tokens de leitura/marca |
| Redesenho completo de hero/CTA no estilo techleads.club (badge pill, bloco de código com botão "Copy", títulos peso 900, grid de cards com ícone em chip) | Mudança de **layout/composição**, não de token — fica para uma feature de layout futura; esta feature é só cor/fonte |
| Paleta de callout boxes de artigo (info/warn/good/danger/tip — teal/amber/rose/green/purple) | Não tem equivalente 1:1 no Design System de app (que não define "tip"/"good" como estados distintos); mantida como está — só garantir que os tons derivados do azul (`--blue-light`/`--blue-pale`) continuem harmônicos após a troca do azul base |
| `site/css/catalog.css` (card de catálogo/filtro da feature anterior) | Já usa tokens `--cc-*` autocontidos por decisão de projeto registrada (AD-001) — o valor do acento (`#184194`) já bate com a marca; fora do escopo de re-referenciar |

---

## Assumptions & Open Questions

Every ambiguity is resolved or recorded here — nothing is left silently unclear.

| Assumption / decision | Chosen default | Rationale | Confirmed? |
| --- | --- | --- | --- |
| Fundo de leitura (substitui `--sand`) | `#F8F9FA` | Decisão explícita do usuário — já existe como "Sidebar Background" no Design System oficial e como `--bg-alt` em `css/style.css` | y |
| Escopo | Unificar os 3 sistemas de token do site inteiro (causa raiz), não só o `--sand` pontual | Decisão explícita do usuário | y |
| Fonte padrão do site | Segoe UI / system-ui (não Inter/Syne) | Decisão explícita do usuário — alinha ao Design System oficial e ao resto do ecossistema CygnusForge (WPF) | y |
| Fonte da verdade dos valores de cor/tipografia | `Padroes_CygnusForge/01-Fundamentos/02-Cores.md` e `03-Tipografia.md` — não inventar novos valores | `css/style.css` (home) já segue esses valores byte-a-byte (conferido nesta sessão); artigos/treinamento divergiram deles | y |
| Cores de apoio de callout (teal/amber/rose/green/purple) | Mantidas como estão | Não têm equivalente no Design System de app; são semântica editorial, não de marca | y |
| Acentos decorativos por módulo e cores de diagrama (hex inline) | Fora de escopo, não tocados | Ver tabela Out of Scope | y |
| Risco: títulos de `treinamento-ia-analistas` dimensionados para Syne (fonte display) podem ficar desproporcionais em Segoe UI | Verificar visualmente (screenshot antes/depois) durante Execute e ajustar tamanho/peso pontualmente se necessário — não é um valor que dá pra prever sem ver o resultado renderizado | Syne e Segoe UI têm métricas de fonte muito diferentes; decidir o ajuste exato antes de ver o resultado seria adivinhação | y |
| Separação de seção estilo techleads.club (blocos de cor alternados em vez de bordas) | Parcialmente alcançada como efeito colateral (trocar `--sand` por `#F8F9FA` já cria alternância branco/cinza-frio entre cards e fundo de página); redesenho completo de hero/CTA fica fora de escopo | Ver tabela Out of Scope — separar mudança de token (esta feature) de mudança de layout (futura) | y |

**Open questions:** none — todas resolvidas ou registradas acima.

---

## User Stories

### P1: Fundo de leitura de alto contraste ⭐ MVP

**User Story**: Como leitor de Artigos ou Treinamento IA, quero que o texto tenha contraste forte contra o fundo, para ler sem esforço.

**Why P1**: É a reclamação concreta que motivou a feature — impacto direto na experiência de leitura, hoje.

**Acceptance Criteria**:

1. WHEN `artigos/index.html` (e páginas de artigo) carrega THEN o fundo da página SHALL ser `#F8F9FA`, não mais `--sand #f8f6f1`.
2. WHEN qualquer página de `treinamento-ia-analistas/` carrega THEN o fundo SHALL ser `#F8F9FA`, não mais `--sand #f8f6f1`.
3. WHEN a cor de texto principal (`--ink`) é medida contra o novo fundo `#F8F9FA` THEN a razão de contraste SHALL ser ≥ 7:1 (WCAG AAA para texto normal).

**Independent Test**: Abrir `artigos/index.html` e uma página de `treinamento-ia-analistas/`, inspecionar `background-color` computado do body/page-wrap — deve ser `#F8F9FA`.

---

### P1: Paleta de marca unificada ⭐ MVP

**User Story**: Como visitante navegando entre a home, Artigos e Treinamento IA, quero ver a mesma identidade de cor (azul da marca, cores semânticas), para o site parecer um produto único, não três.

**Why P1**: É a causa raiz do problema — sem isso, qualquer ajuste fica pontual e a divergência volta a aparecer.

**Acceptance Criteria**:

1. WHEN os tokens de cor primária são comparados entre `css/style.css`, `artigos/style.css` e `treinamento-ia-analistas/style.css` THEN os valores SHALL ser idênticos: Primary `#184194`, Primary Hover `#0F2D6B`.
2. WHEN os tokens de cor semântica (Success/Danger/Warning/Info) são comparados entre as 3 stylesheets THEN os valores SHALL ser idênticos aos do Design System oficial (`#28A745`/`#DC3545`/`#FFC107`/`#17A2B8`).
3. WHEN qualquer link, botão primário ou destaque de navegação é exibido em qualquer página do site THEN a cor SHALL ser `#184194` — nenhuma página SHALL usar `#1d6fcd` ou qualquer outro azul não-oficial.
4. WHEN os tons derivados do azul (`--blue-light`/`--blue-pale`, usados em callouts e fundos suaves) são recalculados a partir do novo `#184194` THEN eles SHALL permanecer visualmente harmônicos (mesma matiz, tint mais claro) — não uma cor de matiz diferente "sobrando" do azul antigo.

**Independent Test**: Abrir as 3 stylesheets lado a lado (ou via `getComputedStyle` no navegador) e confirmar que os valores hex dos tokens de marca batem exatamente.

---

### P1: Fonte única Segoe UI ⭐ MVP

**User Story**: Como visitante, quero que a tipografia seja consistente entre páginas, sem uma seção "parecer de outro site".

**Why P1**: Fonte é o elemento mais perceptível de identidade visual depois da cor — Syne/Inter em uma única seção quebra a percepção de site único.

**Acceptance Criteria**:

1. WHEN qualquer página do site carrega THEN a fonte computada do `body` SHALL ser a pilha `'Segoe UI', system-ui, -apple-system, sans-serif` (ou equivalente already usado em `css/style.css`).
2. WHEN `treinamento-ia-analistas/*.html` é inspecionado THEN nenhuma tag `<link>`/`@import` para `fonts.googleapis.com` (Syne, Inter) SHALL existir.
3. WHEN um título (`h1`/`h2`/`h3`) de `treinamento-ia-analistas` é comparado visualmente antes/depois da troca de fonte THEN nenhum título SHALL parecer desproporcionalmente grande, pequeno ou "quebrado" em relação ao conteúdo ao redor (verificação visual via screenshot, não é um valor numérico pré-definido — ver Assumption sobre risco de Syne→Segoe UI).

**Independent Test**: `getComputedStyle(document.body).fontFamily` em qualquer página deve retornar a pilha Segoe UI; nenhuma requisição de rede para `fonts.googleapis.com` deve ocorrer.

---

### P2: Consistência de hierarquia tipográfica

**User Story**: Como visitante, quero que títulos de mesmo nível pareçam ter a mesma importância visual em qualquer página do site.

**Why P2**: Refinamento sobre a P1 (fonte) — a troca de família de fonte é o requisito duro; a calibração fina de tamanho é ajuste incremental, não bloqueante.

**Acceptance Criteria**:

1. WHEN um título de nível equivalente (ex.: título de seção) é comparado entre páginas do site THEN a relação de tamanho/peso SHALL seguir a mesma lógica de hierarquia do Design System (títulos maiores = mais peso/tamanho, nunca o inverso).

---

## Edge Cases

- WHEN uma página do site (dentre as ~15 páginas HTML existentes) não for tocada por engano THEN o Verifier SHALL sinalizar como gap (grep por `--sand`, `Syne`, `Inter`, `fonts.googleapis` em todo `site/`, não só nas 3 stylesheets).
- WHEN o texto dentro de um callout box (`.callout.info`, `.callout.warn`, etc.) é revisado após a troca do azul base THEN o contraste texto/fundo dentro do callout SHALL continuar adequado — a troca de `--blue`/`--blue-pale` não pode quebrar contraste ali.
- WHEN uma página usa cor hardcoded (hex direto, não token) para algo que deveria ser o azul de marca (ex.: `#1d6fcd` em `treinamento-ia-analistas/index.html` fora dos acentos de módulo) THEN essa ocorrência específica SHALL ser corrigida para o token oficial.

---

## Requirement Traceability

| Requirement ID | Story | Phase | Status |
| --- | --- | --- | --- |
| TOK-01 | P1: Fundo de leitura de alto contraste | Verified | ✅ Verified |
| TOK-02 | P1: Fundo de leitura de alto contraste | Verified | ✅ Verified |
| TOK-03 | P1: Fundo de leitura de alto contraste (contraste WCAG) | Verified | ✅ Verified |
| TOK-04 | P1: Paleta de marca unificada | Verified | ✅ Verified |
| TOK-05 | P1: Paleta de marca unificada (cores semânticas) | Verified | ✅ Verified |
| TOK-06 | P1: Paleta de marca unificada (nenhum azul não-oficial) | Verified | ✅ Verified |
| TOK-07 | P1: Paleta de marca unificada (tons derivados harmônicos) | Verified | ✅ Verified |
| TOK-08 | P1: Fonte única Segoe UI | Verified | ✅ Verified |
| TOK-09 | P1: Fonte única Segoe UI (sem Google Fonts) | Verified | ✅ Verified |
| TOK-10 | P1: Fonte única Segoe UI (sem quebra visual de título) | Verified | ✅ Verified |
| TOK-11 | P2: Consistência de hierarquia tipográfica | Verified | ✅ Verified |

**ID format:** `TOK-NNN`.

**Coverage:** 11 total, 11 mapeados para Design, 0 sem mapeamento.

---

## Success Criteria

- [ ] Os 3 stylesheets do site usam os mesmos valores de token de cor de marca/semântica, idênticos ao Design System oficial
- [ ] Nenhuma página carrega fonte externa (Google Fonts) — só Segoe UI/system
- [ ] Fundo de leitura de Artigos e Treinamento IA passa de `--sand` para `#F8F9FA`, com contraste ≥ 7:1 medido
- [ ] Home, Produtos, Serviços e demais páginas que já seguiam o padrão continuam visualmente idênticas (zero regressão)
