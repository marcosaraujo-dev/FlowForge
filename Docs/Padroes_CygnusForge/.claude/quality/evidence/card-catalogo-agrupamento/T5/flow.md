# Evidência E2E — card-catalogo-agrupamento/T5 (regressão)

**Driver**: playwright-skill (Playwright headless, Chromium)
**Data**: 2026-09-14

## Passos executados

1. Removidas de `site/css/style.css` as regras `.products-grid`, `.product-card` + `__*`, `.product-card--offwork/--forge/--training/--guardian`, `.product-status` + variantes, `@keyframes pulse-dot`, e a regra responsiva `.products-grid { grid-template-columns: 1fr; }` dentro do media query.
2. Atualizado `site/js/main.js` (`initFadeInElements`): `.product-card` → `.catalog-card`.
3. Reexecutado o mesmo script de T4 (`produtos-01-todos.png` até `MOBILE_SCROLLWIDTH`) como checagem de regressão.
4. Verificação adicional: contagem de `.catalog-card.fade-in` após `DOMContentLoaded`.

## Resultado observado

- Resultado idêntico ao de T4 em todos os passos (chips, contadores, filtro por categoria, badge de status, mobile sem scroll horizontal, zero erros de console) — nenhuma regressão visual ou funcional após a remoção do CSS antigo.
- `CATALOG_CARDS_WITH_FADEIN_CLASS: 4` — confirma que `initFadeInElements()` agora aplica a classe `fade-in` aos 4 `.catalog-card` (antes aplicava a `.product-card`, que não existe mais no DOM).
- Nenhuma regra `.product-card*`/`.products-grid`/`.product-status*` restante em `style.css` (grep confirmado antes do commit).

**Veredito**: Limpeza não introduziu regressão. Gate Full ✅.

---

## Correção pós-Verifier (2026-09-14)

Mesmo blind spot de T4 (checava só `el.hidden`, não `getComputedStyle`) — ver `T4/flow.md` e `T6/flow.md` para a causa raiz e o fix real (`site/css/catalog.css` commit `2fd484b`). Screenshot recapturado; comportamento confirmado correto após o fix.
