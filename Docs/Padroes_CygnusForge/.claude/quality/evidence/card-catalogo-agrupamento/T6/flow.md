# Evidência E2E — card-catalogo-agrupamento/T6

**Driver**: playwright-skill (Chromium headless)
**Data**: 2026-09-14

## Passos executados

1. Servir `site/` via `python -m http.server 8791` e navegar para `http://localhost:8791/artigos/index.html`.
2. Ler o texto de cada chip de categoria gerado pelo `catalog-filter.js`.
3. Clicar no chip "Uso de IA" e contar/ler os cards visíveis (sem `hidden`) dentro do grid.
4. Clicar no chip "Gestão de Projetos" (categoria com 0 itens) e verificar visibilidade + texto do elemento `[data-catalog-empty]`.
5. Clicar no chip "Todos" e contar os cards visíveis novamente.
6. Redimensionar o viewport para 375x800 e comparar `scrollWidth` vs `clientWidth` do documento (detecta scroll horizontal).
7. Capturar todas as mensagens de console (`console` + `pageerror`) durante toda a navegação.

## Resultado observado

- Chips: `["Todos (2)", "Uso de IA (1)", "Desenvolvimento (1)", "Gestão de Projetos (0)", "Processos (0)"]` — contadores corretos.
- Após clicar "Uso de IA": 1 card visível — título "IA nas empresas: por que tanta implementação vira frustração" (correto, o outro artigo ficou `hidden`).
- Após clicar "Gestão de Projetos": estado vazio visível, texto exato "Nenhum item nesta categoria ainda." (default do `catalog-filter.js`).
- Após clicar "Todos": 2 cards visíveis novamente (restaurado).
- Mobile 375px: `scrollWidth === clientWidth` (375 === 375) → sem overflow horizontal, chips quebraram linha corretamente.
- Console: nenhuma mensagem (array vazio) — zero erros/warnings.

**Screenshots**: `screenshot-filtro-uso-de-ia.png`, `screenshot-estado-vazio.png`, `screenshot-mobile-375.png`.

**Veredito**: ✅ Todos os critérios do "Done when" de T6 relacionados a comportamento de filtro confirmados.

---

## Correção pós-Verifier (2026-09-14)

O Verifier independente encontrou os screenshots originais malformados (não batiam com o nome do arquivo). Ao recapturá-los, uma checagem visual real (não só `el.hidden`) revelou que o card não filtrado **continuava renderizado** (`getComputedStyle(el).display === 'flex'`) mesmo com o atributo `hidden` corretamente setado — porque `.catalog-card--article { display: flex }` (CSS de autor) sempre vence o `[hidden] { display: none }` padrão do navegador, independente de especificidade. Este script original só verificava `el.hidden` (propriedade JS), nunca o estilo computado — por isso não pegou o bug.

**Fix real aplicado**: `site/css/catalog.css` — commit `2fd484b` (`.catalog-card[hidden] { display: none; }`). Screenshots recapturados após o fix; `getComputedStyle` confirmado `none` para os cards ocultos. Lição para futuras evidências e2e neste harness: sempre checar `getComputedStyle(el).display`/visibilidade real, nunca só a propriedade `hidden`/`aria-*` do DOM.
