# Evidência E2E — card-catalogo-agrupamento/T7 (checagem de regressão)

**Driver**: playwright-skill (Chromium headless)
**Data**: 2026-09-14

## Passos executados

Reexecução idêntica ao roteiro de T6 (ver `../T6/flow.md`), após remover de `site/artigos/style.css`
as regras `.theme-grid`, `.theme-card*`, `.theme-status*`, `@keyframes pulse-dot` e `.article-card*`
(confirmado via grep que nenhuma delas era referenciada em outro lugar do arquivo antes de remover).
`.chip`/`.hero-chips` foram preservados (ainda usados no hero da página, fora do escopo desta feature).

**Nota operacional**: antes de editar `style.css`, havia mudanças não commitadas e não relacionadas a
esta feature (estilos de tabela em `.prose` e ajuste de `.mermaid-block__visual`) — foram guardadas com
`git stash push -- site/artigos/style.css` para não misturar no commit desta task, e devolvidas ao
working tree com `git stash pop` logo depois do commit de T7 (ver resumo do lote).

## Resultado observado

Idêntico a T6: chips com contadores corretos, filtro "Uso de IA" isola o artigo certo, "Gestão de
Projetos" (0 itens) mostra o estado vazio, "Todos" restaura os 2 artigos, mobile 375px sem overflow
horizontal, zero mensagens de console.

**Veredito**: ✅ Remoção do CSS antigo não introduziu nenhuma regressão visual ou funcional.

---

## Correção pós-Verifier (2026-09-14)

Dois problemas encontrados pelo Verifier independente, ambos corrigidos:
1. `site/artigos/style.css:313` ainda tinha uma regra morta `.article-card { flex-direction: column; }` dentro do `@media (max-width: 768px)` — grep original não pegou por ser uma linha solta sem prefixo `__`. Removida isoladamente (commit `e346872`, sem misturar no trabalho não relacionado do usuário que estava pendente no mesmo arquivo).
2. Os screenshots originais estavam malformados (não batiam com o nome do arquivo) — ao recapturar, uma checagem de `getComputedStyle` (não só `el.hidden`) revelou o mesmo bug real de visibilidade descrito em `T6/flow.md`: `.catalog-card--article { display: flex }` vencia o `[hidden]{display:none}` do navegador. Fix real em `site/css/catalog.css` commit `2fd484b`. Screenshots recapturados após os dois fixes.
