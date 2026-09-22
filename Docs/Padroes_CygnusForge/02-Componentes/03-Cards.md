# Cards e Containers

Cards sao os principais containers de conteudo da interface. Eles agrupam informacoes relacionadas e criam separacao visual entre diferentes areas da tela.

> Ver exemplos visuais: [Exemplos-HTML/cards.html](../Exemplos-HTML/cards.html)

---

## Indice

1. [Anatomia de um Card](#1-anatomia-de-um-card)
2. [Variantes](#2-variantes)
3. [Card de Estatistica](#3-card-de-estatistica)
4. [Info Box](#4-info-box)
5. [Espacamentos e Tokens](#5-espacamentos-e-tokens)
6. [Regras de Uso](#6-regras-de-uso)

---

## 1. Anatomia de um Card

```
+--------------------------------------------------+
|                   [Header opcional]              |  <- Padding: 12px, border-bottom: 1px
|--------------------------------------------------|
|                                                  |
|   Conteudo principal                             |  <- Padding: 10-16px
|   Texto, formularios, tabelas, etc               |
|                                                  |
|--------------------------------------------------|
|   [Footer opcional com acoes]                    |  <- Padding: 12px, border-top: 1px
+--------------------------------------------------+
```

**Especificacoes base:**
- Fundo: Branco `#FFFFFF`
- Borda: 1px solid `#DEE2E6`
- Border Radius: 8px (`CornerRadius.Large`)
- Padding padrao: 10px (`Padding.Card`)
- Sombra: `0 1px 3px rgba(0,0,0,0.05)` — sutil, apenas para elevar

---

## 2. Variantes

### Card Simples

Sem header, sem footer. So o conteudo com padding.

**Uso:** Agrupamento simples de campos, informacoes de leitura.

```
+------------------------+
| Nome: Joao da Silva    |
| CPF: 000.000.000-00   |
| Cargo: Analista       |
+------------------------+
```

---

### Card com Header

Header com titulo, separado por borda inferior fina.

**Uso:** Secoes dentro de uma pagina, quando o agrupamento precisa de nome.

```
+------------------------+
| Dados Pessoais         |  <- Header: SectionTitle + padding 12px
|------------------------|
| Nome: Joao da Silva    |
| CPF: 000.000.000-00   |
+------------------------+
```

**Especificacoes do header:**
- Padding: 12px
- Borda inferior: 1px solid `#E9ECEF`
- Titulo: estilo `SectionTitle` (20px SemiBold)

---

### Card com Header e Footer de Acoes

Header + conteudo + footer com botoes de acao.

**Uso:** Formularios dentro de cards, itens de lista com acoes.

```
+------------------------+
| Configuracao X         |
|------------------------|
| Campo 1: ___________   |
| Campo 2: ___________   |
|------------------------|
| [Cancelar] [Salvar]    |  <- Footer: padding 12px, borda superior
+------------------------+
```

---

### Card de Background Colorido (sidebar/area secundaria)

Fundo levemente colorido para diferenciar areas auxiliares.

- **Fundo:** `#F8F9FA` (Sidebar Background)
- **Borda:** 1px solid `#DEE2E6`
- **Uso:** Filtros laterais, paineis de configuracao, areas de resumo

---

## 3. Card de Estatistica

Cards clicaveis usados em dashboards para mostrar numeros resumidos com acesso rapido a informacoes.

### Estrutura

```
+---------------------------+
| [Icone]   Titulo          |  <- 16px padding
|           do Card         |
|                           |
|   123                     |  <- Numero grande (24-28px Bold)
|   Descricao curta         |  <- Subtexto (12-13px, cinza)
+---------------------------+
  | Borda esquerda colorida (4px) como acento visual
```

### Especificacoes

- Borda esquerda: 4px com cor de acento (Primary, Success, Info, Warning)
- Hover: leve sombra e deslocamento de 1px para cima
- Cursor: pointer (e clicavel)
- Fundo: Branco

---

## 4. Info Box

Caixas informativas que complementam o conteudo principal com contexto ou instrucoes.

### Variantes

| Tipo | Fundo | Borda esquerda | Uso |
|------|-------|----------------|-----|
| **Neutro** | `#F8F9FA` | Nenhuma | Informacao complementar simples |
| **Info** | `#D1ECF1` | `#17A2B8` (4px left) | Dica ou instrucao importante |
| **Warning** | `#FFF3CD` | `#FFC107` (4px left) | Aviso que requer atencao |
| **Danger** | `#F8D7DA` | `#DC3545` (4px left) | Alerta critico |

### Exemplo de Info Box

```
+-------------------------------------------+
|   | Esta informacao sera usada no calculo  |
|   | do imposto de renda. Verifique antes   |  <- Borda left = 4px Info
|   | de confirmar.                          |
+-------------------------------------------+
```

---

## 5. Espacamentos e Tokens

| Elemento | Token | Valor |
|----------|-------|-------|
| Padding do card | `Padding.Card` | 10px |
| Padding do header | `Padding.Card.Header` | 12px |
| Margin entre cards | `Margin.Between.Cards` | 0,0,0,24px |
| Borda | `BorderThickness.Thin` | 1px |
| Border radius | `CornerRadius.Large` | 8px |

---

## 6. Regras de Uso

### Obrigatorio

- Cards sempre tem fundo branco (exceto variante sidebar)
- O padding interno deve ser consistente — nao misture 10px e 20px no mesmo card
- Cards nao devem ficar sem padding (o conteudo nao pode tocar a borda)

### Aninhamento

Evite cards dentro de cards. Se precisar de sub-agrupamento, use:
- Linhas divisoras (border-bottom)
- Accordions/Expanders para conteudo colapsavel
- Grupos de campos com apenas um label de secao

### Cards vs Secoes

Use **card** quando o grupo de conteudo precisa de separacao fisica clara (fundo, borda, sombra).
Use apenas um **titulo de secao** (sem card) quando o agrupamento e logico mas nao precisa de container visual.

---

*Anterior: [02-Badges-Status.md](02-Badges-Status.md) | Proximo: [04-Formularios.md](04-Formularios.md)*
