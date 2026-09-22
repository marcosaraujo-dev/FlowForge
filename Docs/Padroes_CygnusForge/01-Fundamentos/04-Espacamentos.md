# Espacamentos

O sistema de espacamentos define as distancias entre elementos, o preenchimento interno de componentes e os raios de borda. Todos os valores sao multiplos de 4px.

---

## Indice

1. [Por que multiplos de 4?](#1-por-que-multiplos-de-4)
2. [Tokens de Espaco (Space)](#2-tokens-de-espaco-space)
3. [Padding — Espaco Interno](#3-padding---espaco-interno)
4. [Margin — Espaco Externo](#4-margin---espaco-externo)
5. [Corner Radius — Arredondamento](#5-corner-radius---arredondamento)
6. [Border Thickness — Espessura de Borda](#6-border-thickness---espessura-de-borda)
7. [Alturas de Componentes](#7-alturas-de-componentes)
8. [Regras de Ouro](#8-regras-de-ouro)

---

## 1. Por que multiplos de 4?

Usar multiplos de 4 (4, 8, 12, 16, 20, 24, 32...) cria ritmo visual consistente. Elementos alinhados em um grid de 4px parecem organizados e profissionais. Valores aleatorios (7px, 13px, 19px) criam inconsistencia visual dificil de perceber mas facil de sentir.

**Regra simples:** Se precisar de um espaco, escolha o multiplo de 4 mais proximo do que voce imaginou.

---

## 2. Tokens de Espaco (Space)

Valores numericos usados em Width, Height e dimensoes.

| Token | Valor | Uso tipico |
|-------|-------|-----------|
| `Space0` | 0px | Sem espaco |
| `Space1` | 4px | Espaco minimo, gaps pequenos |
| `Space2` | 8px | Espaco compacto |
| `Space3` | 12px | Espaco interno pequeno |
| `Space4` | 16px | **Espaco padrao** |
| `Space5` | 20px | Espaco confortavel |
| `Space6` | 24px | Entre secoes |
| `Space7` | 28px | Entre grupos |
| `Space8` | 32px | Entre areas |
| `Space10` | 40px | Espacamento grande |
| `Space12` | 48px | Espacamento maximo |

---

## 3. Padding — Espaco Interno

O padding e o espaco entre o conteudo e a borda do componente.

### Padding Generico

| Token | Valor | Uso |
|-------|-------|-----|
| `Padding.None` | 0 | Sem padding |
| `Padding.XSmall` | 4px | Badges, elementos muito compactos |
| `Padding.Small` | 8px | Compacto |
| `Padding.Medium` | 16px | **Padding padrao** |
| `Padding.Large` | 24px | Containers grandes |
| `Padding.XLarge` | 32px | Containers muito grandes |

### Padding por Componente

| Token | Valor | Componente |
|-------|-------|-----------|
| `Padding.Button.Small` | 10px horizontal, 3px vertical | Botao pequeno |
| `Padding.Button.Medium` | 16px horizontal, 8px vertical | Botao medio |
| `Padding.Button.Large` | 24px horizontal, 12px vertical | Botao grande |
| `Padding.Button.Default` | 20px horizontal, 10px vertical | **Botao padrao** |
| `Padding.Badge` | 8px horizontal, 3px vertical | Badges |
| `Padding.Card` | 10px todos os lados | Cards |
| `Padding.Card.Header` | 12px todos os lados | Cabecalho de card |
| `Padding.Input` | 8px horizontal, 6px vertical | Campos de formulario |
| `Padding.Input.Compact` | 6px horizontal, 4px vertical | Input compacto |
| `Padding.Alert` | 12px todos os lados | Caixas de alerta |

---

## 4. Margin — Espaco Externo

A margin e o espaco entre um componente e os elementos ao redor.

| Token | Valor | Quando usar |
|-------|-------|------------|
| `Margin.None` | 0 | Sem margem |
| `Margin.Between.Items` | 0, 0, 0, 15px (bottom) | Entre itens de uma lista |
| `Margin.Between.Sections` | 0, 0, 0, 20px (bottom) | Entre secoes de conteudo |
| `Margin.Between.Cards` | 0, 0, 0, 24px (bottom) | Entre cards na pagina |
| `Margin.Label.To.Input` | 0, 0, 0, 5px (bottom) | Entre label e campo abaixo |
| `Margin.Container` | 30px todos os lados | Container principal da pagina |
| `Margin.PageTitle` | 0, 0, 0, 30px (bottom) | Abaixo do titulo da pagina |

### Principio de Proximidade

Elementos relacionados ficam MAIS PROXIMOS entre si do que de elementos nao relacionados.

```
[ Titulo da Secao ]
<-- 4px --> (label esta proximo do campo — sao relacionados)
[ Label do Campo ]
<-- 6px -->
[ Campo de Input ]

<-- 20px --> (nova secao — mais distante)

[ Proximo Titulo de Secao ]
```

---

## 5. Corner Radius — Arredondamento

O arredondamento das bordas segue uma escala consistente.

| Token | Valor | Onde usar |
|-------|-------|-----------|
| `CornerRadius.None` | 0px | Sem arredondamento (tabelas, divisores) |
| `CornerRadius.Small` | 2px | Elementos muito pequenos |
| `CornerRadius.Medium` | 4px | Inputs, botoes pequenos, tags |
| `CornerRadius.Large` | 8px | **Cards, containers, modais** |
| `CornerRadius.Pill` | 10px | Badges, chips arredondados |

### Regra de Consistencia de Raio

Elementos dentro de um container nao devem ter raio maior que o container pai.

```
Card (CornerRadius.Large = 8px)
  └─ Botao dentro (CornerRadius.Medium = 4px)  <- OK
  └─ Badge (CornerRadius.Pill = 10px)           <- Cuidado — pode parecer "saindo" do card
```

---

## 6. Border Thickness — Espessura de Borda

| Token | Valor | Uso |
|-------|-------|-----|
| `BorderThickness.None` | 0 | Sem borda |
| `BorderThickness.Thin` | 1px | **Borda padrao** (inputs, cards, divisores) |
| `BorderThickness.Medium` | 2px | Destaque, borda de foco, underline de tab ativa |
| `BorderThickness.Thick` | 4px | Acento lateral (borda esquerda de alerta) |

### Bordas Direcionais

Para acento lateral (muito usado em alertas e cards de informacao):

| Token | Valor | Uso |
|-------|-------|-----|
| `BorderThickness.Left` | 4px esquerda | Acento lateral esquerdo |
| `BorderThickness.Bottom.Thin` | 1px inferior | Divisor sutil |
| `BorderThickness.Bottom.Medium` | 2px inferior | Underline de tab ativa |

---

## 7. Alturas de Componentes

Alturas padronizadas garantem que elementos na mesma linha se alinhem verticalmente.

### Botoes

| Token | Valor | Uso |
|-------|-------|-----|
| `Height.Button.Small` | 28px | Botao pequeno (dentro de grids) |
| `Height.Button.Medium` | 34px | **Botao padrao** |
| `Height.Button.Large` | 40px | Botao de destaque |

### Inputs

| Token | Valor | Uso |
|-------|-------|-----|
| `Height.Input.Small` | 28px | Input compacto |
| `Height.Input.Medium` | 34px | **Input padrao** |
| `Height.Input.Large` | 40px | Input de destaque |

> **Importante:** Botoes e inputs na mesma linha devem ter a mesma altura para alinhamento perfeito. Use sempre os tamanhos Medium (34px) como padrao.

### DataGrid

| Token | Valor | Uso |
|-------|-------|-----|
| `Height.DataGrid.Row` | 48px | Altura de cada linha |
| `Height.DataGrid.Header` | 40px | Altura do cabecalho |

---

## 8. Regras de Ouro

### Use sempre

- Multiplos de 4 para todos os espacamentos
- Os tokens — nunca valores numericos diretos no codigo
- Mais espaco ENTRE secoes do que DENTRO de secoes
- Padding interno de 16px ou 20px para containers

### Evite

- Valores quebrados como 7px, 13px, 19px
- Espacamentos menores que 4px (exceto em casos muito especificos)
- Espacamentos maiores que 48px sem justificativa visual clara
- Misturar tamanhos de botao e input na mesma linha sem alinhar as alturas

---

*Anterior: [03-Tipografia.md](03-Tipografia.md) | Proximo: [05-Icones.md](05-Icones.md)*
