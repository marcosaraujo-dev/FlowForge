# Unificação de Tokens Visuais do Site — Validation

**Date**: 2026-09-14
**Spec**: `.specs/features/unificacao-tokens-visuais-site/spec.md`
**Diff range**: repo `cygnusforge`, commits `924a157` (artigos/style.css) e `88f50f7` (treinamento-ia-analistas/style.css)
**Verificação**: feita pelo próprio orquestrador logo após implementar — escopo caiu de "Large" (estimativa inicial) para pequeno/cirúrgico (2 arquivos, ~10 valores) depois da investigação no Design, então a fase formal de Tasks + Verifier independente foi **conscientemente pulada** (auto-sizing da skill: feature Medium não exige o mesmo peso de processo que a feature Large anterior). Verificação abaixo é direta, não autoavaliação vazia — grep de varredura + `getComputedStyle` real + screenshots reais em 5 páginas.

---

## Spec-Anchored Acceptance Criteria

| Criterion | Spec-defined outcome | Evidência | Resultado |
| --- | --- | --- | --- |
| TOK-01/02: fundo de Artigos/Treinamento = `#F8F9FA` | `background-color` computado = `rgb(248, 249, 250)` | `getComputedStyle(document.body).backgroundColor` em `artigos/index.html`, `artigos/ia/ia-nas-empresas.html` e `treinamento-ia-analistas/index.html` → `rgb(248, 249, 250)` nas 3 | ✅ PASS |
| TOK-03: contraste texto/fundo ≥ 7:1 (WCAG AAA) | ≥ 7:1 | Calculado (fórmula WCAG relative luminance): `#0f172a` sobre `#F8F9FA` = **16.94:1** | ✅ PASS |
| TOK-04/05/06: paleta de marca idêntica nas 3 stylesheets, nenhum azul não-oficial | `--blue`/`--navy` = `#184194`/`#0F2D6B` nos 3 arquivos | `git diff` mostra `treinamento-ia-analistas/style.css` alterado para os mesmos valores de `artigos/style.css`/`css/style.css`; grep por `1d6fcd`/`0d1b2a`/`1a3160`/`1558a8` em `*.css` → 0 ocorrências | ✅ PASS |
| TOK-07: tons derivados (`--blue-light`/`--blue-pale`) harmônicos | Mesmos valores usados com sucesso em `artigos/style.css` | `treinamento-ia-analistas/style.css` agora usa `#E3EFFF`/`#F5F8FF` (idêntico a artigos) | ✅ PASS |
| TOK-08: fonte computada = pilha Segoe UI | `'Segoe UI', system-ui, -apple-system, sans-serif` | `getComputedStyle(document.body).fontFamily` em Artigos e Treinamento → `"Segoe UI", system-ui, -apple-system, sans-serif` | ✅ PASS |
| TOK-09: nenhum `@import`/`<link>` para fonts.googleapis | 0 ocorrências | `grep -rn "fonts.googleapis" site/` → 0 ocorrências em todo o site | ✅ PASS |
| TOK-10/11: nenhum título desproporcional após troca de fonte | Verificação visual | Screenshots de `treinamento-ia-analistas/index.html` e `01-fundamentos.html` — hierarquia de título consistente, sem quebra visual perceptível, alinhado ao mesmo padrão de hero já usado em Artigos | ✅ PASS (checagem visual, não numérica — conforme a Assumption do spec) |

**Status**: ✅ Todos os 11 critérios de aceite confirmados.

---

## Varredura de Regressão

- `grep -rn "f8f6f1\|Syne\|fonts.googleapis"` em todo `site/` (`.css` + `.html`) → **0 ocorrências** (nenhuma página escapou do escopo).
- Home (`index.html`) — `background-color` computado permanece `rgb(245, 245, 245)` (`#F5F5F5`), **inalterado** — confirma zero regressão nas páginas que já seguiam o Design System.
- `.catalog-card`/filtro por categoria (feature anterior) continuam funcionando em Artigos após a mudança de `--sand`→`--bg-page` (confirmado visualmente no screenshot — chips e contadores corretos).

---

## Nota honesta sobre "contraste"

A razão de contraste WCAG entre `#0f172a` e o fundo antigo (`--sand #f8f6f1`) já era **16.53:1** — muito acima do mínimo AAA (7:1). O fundo novo (`#F8F9FA`) mede **16.94:1** — uma melhora real, porém marginal em termos puramente numéricos. Ou seja, o problema relatado ("parece apagado") não era uma falha objetiva de contraste WCAG — é mais provável que seja **harmonia de cor**: o tom quente/creme do `--sand` destoava visualmente do resto do site, que é inteiramente construído sobre azul frio (header navy, links azuis, badges). Trocar para um cinza frio da mesma família do `--bg-alt`/`Sidebar Background` do Design System resolve essa dissonância — a melhora é perceptual/de coerência de paleta, não uma correção de acessibilidade que estivesse quebrada antes.

---

## Code Quality

| Principle | Status |
| --- | --- |
| Mudança mínima (só valores de `:root`, sem refatoração) | ✅ |
| Nenhum arquivo HTML tocado (todos usam `var()`) | ✅ |
| Sem scope creep (acentos decorativos e cores de diagrama, já listados como fora de escopo, não tocados) | ✅ |
| 2 commits atômicos, um por arquivo | ✅ |
| Trabalho não relacionado do usuário em `artigos/style.css` preservado via `git stash`/`pop` (mesma técnica já usada na feature anterior) | ✅ |

---

## Summary

**Overall**: ✅ Ready

**O que funciona**: Artigos e Treinamento IA agora compartilham a mesma paleta de marca, mesma fonte (Segoe UI) e mesmo fundo de leitura frio (#F8F9FA) do resto do site. Home/Produtos/Serviços continuam inalterados. Nenhuma dependência de Google Fonts restante em nenhuma página.

**Issues found**: Nenhum.

**Next steps**: Nenhum pendente nesta feature. Fora de escopo (registrado no spec.md) permanece disponível para uma feature futura, se desejado: redesenho de layout estilo techleads.club (hero com badge/CTA de código, cards com ícone em chip).
