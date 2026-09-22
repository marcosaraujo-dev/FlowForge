# Evidência E2E — card-catalogo-agrupamento/T4

**Driver**: playwright-skill (Playwright headless, Chromium)
**Data**: 2026-09-14

## Passos executados

1. Servido `C:\Users\Marcos\source\repos\cygnusforge\site` via `python -m http.server 8934`, aberto `http://localhost:8934/index.html`.
2. Screenshot da seção `#produtos` com os 4 cards e a barra de chips. → `produtos-01-todos.png`
3. Lido texto dos chips via `.category-filter__chip`.
4. Lido estado (`hidden`, título, badge de status) dos 4 `.catalog-card` na condição inicial ("Todos").
5. Clicado no chip `data-filter-key="produto"`, relido o estado dos 4 cards. → `produtos-02-filtro-produto.png`
6. Clicado no chip `data-filter-key="treinamento"`, relido o estado dos 4 cards.
7. Clicado no chip `data-filter-key="ferramenta-dev"`, relido o estado dos 4 cards.
8. Clicado no chip `data-filter-key="all"` ("Todos"), relido o estado dos 4 cards.
9. Redimensionado viewport para 375x800 (mobile), medido `document.documentElement.scrollWidth` vs `clientWidth`. → `produtos-03-mobile-chips.png`
10. Capturado `console.error`/`pageerror` durante toda a navegação.

## Resultado observado

- Chips: `["Todos (4)","Produto (2)","Treinamento (1)","Ferramenta Dev (1)"]` — contadores corretos (AC 2 de T4).
- Estado inicial ("Todos"): os 4 cards visíveis (`hidden:false`), badges de status corretos (OffWork/ForgeFinance = "Em desenvolvimento"; Treinamento IA/Code Guardian = "Disponível").
- Filtro "Produto": só OffWork e ForgeFinance visíveis; Treinamento IA e Code Guardian com `hidden:true`. ✅
- Filtro "Treinamento": só Treinamento IA visível, os outros 3 `hidden:true`. ✅
- Filtro "Ferramenta Dev": só Code Guardian visível, os outros 3 `hidden:true`. ✅
- "Todos": os 4 voltam a `hidden:false`. ✅
- Badge de status (`.catalog-card__status`) presente e correto em TODOS os estados acima, independente do filtro ativo. ✅
- Mobile (375px): `scrollWidth === clientWidth === 375` → **sem scroll horizontal**; chips renderizam em `flex-wrap`. ✅
- Console: `CONSOLE_ERRORS: []` — nenhum erro JS durante carregamento nem cliques. ✅

**Veredito**: Todos os critérios de "Done when" de T4 confirmados via evidência real (não autoavaliação). Gate Full ✅.

---

## Correção pós-Verifier (2026-09-14)

Este script só checava `el.hidden` (propriedade JS), nunca `getComputedStyle(el).display`. Uma checagem visual posterior (ver `T6/flow.md`) revelou que `.catalog-card--product { display: flex }` (CSS de autor) vencia o `[hidden] { display: none }` do navegador — os cards marcados `hidden:true` continuavam renderizando visualmente (`display: flex`), incluindo neste filtro "Produto" (Treinamento IA aparecia visível no `produtos-02-filtro-produto.png` original). **Fix real**: `site/css/catalog.css` commit `2fd484b`. Screenshots recapturados após o fix; `getComputedStyle` confirmado `none` para os cards ocultos.
