# Sombras e Elevacao

Sombras criam a percepcao de profundidade e hierarquia. Elementos mais importantes ou que flutuam sobre o conteudo tem sombras mais pronunciadas.

---

## Indice

1. [Niveis de elevacao](#1-niveis-de-elevacao)
2. [Tokens de sombra](#2-tokens-de-sombra)
3. [Quando usar cada nivel](#3-quando-usar-cada-nivel)
4. [Sombras em estados interativos](#4-sombras-em-estados-interativos)
5. [Regras](#5-regras)

---

## 1. Niveis de elevacao

O Design System define 5 niveis de elevacao, do mais baixo (colado na superficie) ao mais alto (flutuando sobre tudo).

```
Nivel 0 — Superficie (sem sombra)
Nivel 1 — Sutil (elementos levemente elevados)
Nivel 2 — Medio (cards, paineis)
Nivel 3 — Alto (dropdowns, popovers)
Nivel 4 — Overlay (dialogs, modais)
```

Cada nivel tem um valor de sombra definido. A cor da sombra e sempre preta com opacidade variavel — nunca colorida.

---

## 2. Tokens de sombra

| Token | Valor | Uso principal |
|-------|-------|---------------|
| `Shadow.None` | nenhuma | Elementos na superficie (tabelas, inputs, badges) |
| `Shadow.XSmall` | `0 1px 2px rgba(0, 0, 0, 0.05)` | Bordas sutis, separacao leve entre areas |
| `Shadow.Small` | `0 2px 8px rgba(0, 0, 0, 0.08)` | Cards, paineis laterais, containers elevados |
| `Shadow.Medium` | `0 4px 16px rgba(0, 0, 0, 0.12)` | Dropdowns, tooltips, popovers, cards em hover |
| `Shadow.Large` | `0 8px 32px rgba(0, 0, 0, 0.20)` | Dialogs, modais, drawers |

### Valores detalhados

```
Shadow.XSmall
  offset-x: 0
  offset-y: 1px
  blur:     2px
  spread:   0
  cor:      rgba(0, 0, 0, 0.05)

Shadow.Small
  offset-x: 0
  offset-y: 2px
  blur:     8px
  spread:   0
  cor:      rgba(0, 0, 0, 0.08)

Shadow.Medium
  offset-x: 0
  offset-y: 4px
  blur:     16px
  spread:   0
  cor:      rgba(0, 0, 0, 0.12)

Shadow.Large
  offset-x: 0
  offset-y: 8px
  blur:     32px
  spread:   0
  cor:      rgba(0, 0, 0, 0.20)
```

---

## 3. Quando usar cada nivel

| Componente | Nivel | Token |
|-----------|-------|-------|
| Inputs, TextBoxes, Badges | 0 | `Shadow.None` |
| Cards em repouso | 1 | `Shadow.Small` |
| Cards em hover | 2 | `Shadow.Medium` |
| Tooltips | 2 | `Shadow.Medium` |
| Dropdowns (ComboBox aberto, menus) | 2 | `Shadow.Medium` |
| Popovers, paineis flutuantes | 2 | `Shadow.Medium` |
| Dialogs e Modais | 3 | `Shadow.Large` |
| Overlay de loading | 3 | `Shadow.Large` (no container central) |

### Diagrama de camadas

```
+------------------------------------------------------------------+
|  Nivel 4 — Dialogs e Modais              Shadow.Large             |
|  +------------------------------------------------------------+  |
|  |  Nivel 3 — Dropdowns, Tooltips       Shadow.Medium          |  |
|  |  +--------------------------------------------------------+|  |
|  |  |  Nivel 2 — Cards hover            Shadow.Medium         ||  |
|  |  |  +----------------------------------------------------+||  |
|  |  |  |  Nivel 1 — Cards repouso       Shadow.Small         |||  |
|  |  |  |  +------------------------------------------------+|||  |
|  |  |  |  |  Nivel 0 — Superficie        Shadow.None        ||||  |
|  |  |  |  +------------------------------------------------+|||  |
|  |  |  +----------------------------------------------------+||  |
|  |  +--------------------------------------------------------+|  |
|  +------------------------------------------------------------+  |
+------------------------------------------------------------------+
```

---

## 4. Sombras em estados interativos

Sombras podem mudar em resposta a interacao do usuario para reforcar feedback visual.

| Estado | Transicao de sombra | Exemplo |
|--------|--------------------|---------|
| Repouso → Hover | `Shadow.Small` → `Shadow.Medium` | Card clicavel ganha profundidade ao passar o mouse |
| Hover → Pressed | `Shadow.Medium` → `Shadow.XSmall` | Card "afunda" levemente ao clicar |
| Repouso → Focus | Sem mudanca de sombra — usar borda de foco | Input com foco ganha borda azul, nao sombra |

```
Card em repouso:    Shadow.Small    (0 2px 8px rgba(0,0,0,0.08))
Card em hover:      Shadow.Medium   (0 4px 16px rgba(0,0,0,0.12))
Card pressionado:   Shadow.XSmall   (0 1px 2px rgba(0,0,0,0.05))
```

A transicao de sombra segue a mesma duracao dos demais estados interativos: **150ms ease-out** (ver [08-Animacoes.md](08-Animacoes.md)).

---

## 5. Regras

| Regra | Descricao |
|-------|-----------|
| Nunca sombra colorida | Sombras sao sempre pretas com opacidade. Nunca `rgba(24, 65, 148, 0.3)` |
| Nunca sombra interna | Usar apenas `box-shadow` / `DropShadowEffect` externa. Sombras internas (`inset`) nao fazem parte do DS |
| Sombra acompanha o nivel | Se o componente sobe de nivel (ex: hover), a sombra sobe junto. Nunca pular niveis |
| Inputs nao tem sombra | Campos de formulario usam borda para indicar estado, nunca sombra |
| Sombra nao substitui borda | Cards tem borda `1px solid #DEE2E6` **e** sombra. A sombra e complementar |
| Consistencia entre frameworks | Os mesmos valores de sombra devem ser usados em WPF (DropShadowEffect) e React (box-shadow) |

### Implementacao por framework

**CSS (React):**
```css
.card {
  box-shadow: var(--shadow-small);
}
.card:hover {
  box-shadow: var(--shadow-medium);
}
```

**WPF (XAML):**
```xml
<Border Effect="{StaticResource Shadow.Small}">
    <!-- conteudo -->
</Border>
```

> Nota: No WPF, `DropShadowEffect` tem limitacoes (nao suporta multiplas sombras). Usar `DropShadowEffect` com `BlurRadius` e `Opacity` equivalentes aos tokens.
