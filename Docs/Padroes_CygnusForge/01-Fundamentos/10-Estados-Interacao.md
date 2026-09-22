# Estados de Interacao

Todo componente interativo tem estados visuais que comunicam ao usuario se ele pode ser acionado, se esta sendo acionado e o resultado da interacao. Este documento unifica os padroes de estado para TODOS os componentes.

---

## Indice

1. [Os 6 estados](#1-os-6-estados)
2. [Cores por estado](#2-cores-por-estado)
3. [Transicoes entre estados](#3-transicoes-entre-estados)
4. [Botoes](#4-botoes)
5. [Inputs e campos de formulario](#5-inputs-e-campos-de-formulario)
6. [Linhas de tabela](#6-linhas-de-tabela)
7. [Links e itens de menu](#7-links-e-itens-de-menu)
8. [Cards clicaveis](#8-cards-clicaveis)
9. [Tabs e segmented controls](#9-tabs-e-segmented-controls)
10. [Estados compostos](#10-estados-compostos)
11. [Regras gerais](#11-regras-gerais)

---

## 1. Os 6 estados

Todo elemento interativo pode estar em um destes estados:

| Estado | Descricao | Como ativar |
|--------|-----------|-------------|
| **Normal** | Estado padrao, em repouso | Nenhuma interacao |
| **Hover** | Mouse sobre o elemento | Mover cursor para cima |
| **Focus** | Elemento selecionado via teclado | Tab ou clique |
| **Active / Pressed** | Elemento sendo pressionado | Mousedown ou Enter/Space |
| **Disabled** | Elemento inativo, nao clicavel | Propriedade `disabled` ou `IsEnabled=false` |
| **Loading** | Elemento processando uma acao | Apos clique que dispara operacao assincrona |

### Diagrama de transicao

```
                    Tab / Click
    Normal ─────────────────────────► Focus
      │                                 │
      │ Mouse enter                     │ Mouse enter
      ▼                                 ▼
    Hover ◄─────────────────────── Focus + Hover
      │                                 │
      │ Mousedown                       │ Mousedown / Enter
      ▼                                 ▼
    Active ◄───────────────────── Focus + Active
      │                                 │
      │ Mouseup                         │ Mouseup / Keyup
      ▼                                 ▼
    Normal ◄───────────── Focus (permanece ate Tab/Click fora)
```

---

## 2. Cores por estado

### Principio geral

| Estado | Transformacao visual |
|--------|---------------------|
| **Normal** | Cor base do componente |
| **Hover** | Fundo escurece levemente ou ganha cor sutil |
| **Focus** | Borda azul de foco (2px #93C5FD) — sem mudar fundo |
| **Active** | Fundo escurece mais que hover |
| **Disabled** | Opacidade reduzida ou fundo cinza, cursor `not-allowed` |
| **Loading** | Cor base mantida, conteudo substituido por spinner |

### Cores especificas por variante

#### Fundos neutros (cards, linhas de tabela, inputs)

| Estado | Cor de fundo | Cor de borda |
|--------|-------------|-------------|
| Normal | `#FFFFFF` | `#DEE2E6` |
| Hover | `#F5F8FF` (azulado sutil) | `#CBD5E1` |
| Focus | `#FFFFFF` (sem mudanca) | `#93C5FD` (2px) |
| Active | `#E3EFFF` | `#184194` |
| Disabled | `#F5F5F5` | `#E9ECEF` |

#### Fundos Primary (botoes, badges ativos)

| Estado | Cor de fundo | Cor de texto |
|--------|-------------|-------------|
| Normal | `#184194` | `#FFFFFF` |
| Hover | `#0F2D6B` | `#FFFFFF` |
| Focus | `#184194` + borda foco | `#FFFFFF` |
| Active | `#0A1F4A` | `#FFFFFF` |
| Disabled | `#184194` com 50% opacidade | `#FFFFFF` com 50% opacidade |

#### Fundos Danger (botoes de exclusao)

| Estado | Cor de fundo |
|--------|-------------|
| Normal | `#DC3545` |
| Hover | `#C82333` |
| Active | `#A71D2A` |
| Disabled | `#DC3545` com 50% opacidade |

#### Fundos Success (botoes de aprovacao)

| Estado | Cor de fundo |
|--------|-------------|
| Normal | `#28A745` |
| Hover | `#218838` |
| Active | `#1E7E34` |
| Disabled | `#28A745` com 50% opacidade |

---

## 3. Transicoes entre estados

Todas as transicoes seguem os tokens de [08-Animacoes.md](08-Animacoes.md):

| Transicao | Duracao | Easing | Propriedades animadas |
|-----------|---------|--------|----------------------|
| Normal → Hover | 100ms (Fast) | ease-out | `background-color`, `border-color`, `box-shadow` |
| Hover → Normal | 100ms (Fast) | ease-out | `background-color`, `border-color`, `box-shadow` |
| Normal → Focus | 100ms (Fast) | ease-out | `border-color`, `outline` |
| Any → Active | 100ms (Fast) | ease-out | `background-color`, `transform` |
| Any → Disabled | Instantaneo | — | Todas as propriedades mudam sem transicao |

---

## 4. Botoes

### Botao Primary (acao principal)

```
Normal:    Fundo #184194, texto branco
Hover:     Fundo #0F2D6B (escurece)
Focus:     Fundo #184194 + outline 2px #93C5FD offset 2px
Active:    Fundo #0A1F4A + transform scale(0.98)
Disabled:  Fundo #184194 opacidade 50%, cursor not-allowed, sem hover/focus
Loading:   Fundo #184194, texto substituido por spinner, cliques bloqueados
```

### Botao Outline/Ghost (acao secundaria)

```
Normal:    Fundo transparente, borda 1px #DEE2E6, texto #495057
Hover:     Fundo #F5F8FF, borda #184194, texto #184194
Focus:     Borda #93C5FD 2px
Active:    Fundo #E3EFFF
Disabled:  Borda #E9ECEF, texto #ADB5BD, opacidade 50%
```

### Botao Danger (acao destrutiva)

```
Normal:    Fundo #DC3545, texto branco
Hover:     Fundo #C82333
Active:    Fundo #A71D2A
Disabled:  Opacidade 50%
```

### Botao desabilitado com motivo

Quando um botao esta desabilitado, um Tooltip deve explicar **por que** ele esta desabilitado.

```
Botao [Salvar] (disabled)
    Tooltip: "Preencha todos os campos obrigatorios para salvar"
```

---

## 5. Inputs e campos de formulario

### TextBox / Input

```
Normal:    Borda 1px #DEE2E6, fundo branco
Hover:     Borda 1px #CBD5E1 (escurece levemente)
Focus:     Borda 2px #93C5FD (azul de foco), sem sombra
Preenchido: Borda 1px #DEE2E6, texto #212529
Erro:      Borda 2px #DC3545, mensagem vermelha abaixo
Disabled:  Fundo #F5F5F5, borda #E9ECEF, texto #ADB5BD, cursor not-allowed
Read-only: Fundo #F5F5F5, borda #DEE2E6, texto #495057, cursor default (sem not-allowed)
```

### ComboBox / Select

```
Normal:    Igual TextBox + seta dropdown
Hover:     Borda escurece + seta destaca
Focus:     Borda azul 2px
Aberto:    Dropdown visivel com Shadow.Medium, item hovered com fundo #F5F8FF
```

### Checkbox

```
Normal:    Borda 1px #DEE2E6, fundo branco
Hover:     Borda #184194
Focus:     Outline 2px #93C5FD
Checked:   Fundo #184194, icone check branco
Disabled:  Opacidade 50%
```

---

## 6. Linhas de tabela

As 4 cores de linha sao **obrigatorias** em toda tabela interativa:

| Estado | Cor de fundo | Token |
|--------|-------------|-------|
| Normal | `#FFFFFF` | — |
| Hover | `#F5F8FF` | `DataGridRowHoverBrush` |
| Selecionada | `#E3EFFF` | `DataGridRowSelectedBrush` |
| Hover + Selecionada | `#D6E8FF` | `DataGridRowHoverSelectedBrush` |

### Comportamento

- Hover e imediato (sem delay)
- Selecao persiste ao mover o mouse para fora
- Hover sobre linha selecionada mostra cor composta (mais escura que ambas)
- Linha desabilitada: texto com opacidade 50%, sem hover, nao selecionavel

---

## 7. Links e itens de menu

### Links de texto

```
Normal:    Cor #184194, sem underline
Hover:     Cor #0F2D6B, underline
Focus:     Outline 2px #93C5FD
Active:    Cor #0A1F4A
Visited:   Nao diferenciar (manter mesma cor) — exceto em documentacao
```

### Itens do menu lateral

```
Normal:    Fundo transparente, texto rgba(255,255,255,0.6), icone idem
Hover:     Fundo rgba(255,255,255,0.05), texto rgba(255,255,255,0.8)
Ativo:     Fundo rgba(255,255,255,0.1), texto branco, borda esquerda 3px Primary, icone branco
Focus:     Outline 2px dentro do item (nao fora, por causa do fundo escuro)
```

---

## 8. Cards clicaveis

Cards que funcionam como botao/link de navegacao:

```
Normal:    Borda 1px #DEE2E6, Shadow.Small
Hover:     Borda #184194, Shadow.Medium, transform translateY(-2px)
Focus:     Outline 2px #93C5FD offset 2px
Active:    Shadow.XSmall, transform translateY(0) (afunda)
Disabled:  Opacidade 60%, sem hover/focus/shadow
```

Cards que sao **apenas containers** (nao clicaveis) nao tem hover/focus/active.

---

## 9. Tabs e segmented controls

```
Normal:    Texto #6C757D (Secondary), sem underline
Hover:     Texto #184194 (Primary)
Active:    Texto #184194 SemiBold + underline 2px #184194 (variante Underline)
Focus:     Outline 2px #93C5FD dentro da tab
Disabled:  Texto #ADB5BD, cursor not-allowed, sem hover
```

---

## 10. Estados compostos

Elementos podem estar em mais de um estado simultaneamente. A prioridade visual e:

```
Prioridade (maior para menor):
    1. Disabled  — sobrescreve tudo
    2. Loading   — sobrescreve interacoes
    3. Error     — borda vermelha permanece durante hover/focus
    4. Active    — momentaneo (mousedown)
    5. Focus     — borda de foco permanece durante hover
    6. Hover     — efeito mais sutil
    7. Normal    — estado base
```

### Exemplos de composicao

| Estado composto | Resultado visual |
|----------------|-----------------|
| Focus + Hover | Borda de foco #93C5FD + fundo hover #F5F8FF |
| Focus + Error | Borda vermelha #DC3545 (erro tem prioridade sobre foco) |
| Hover + Disabled | Sem efeito (disabled bloqueia hover) |
| Selecionado + Hover (tabela) | Fundo composto #D6E8FF |
| Focus + Loading | Spinner dentro do botao, borda de foco visivel |

---

## 11. Regras gerais

| Regra | Descricao |
|-------|-----------|
| Todo interativo tem todos os estados | Nunca implementar hover sem focus, ou focus sem disabled |
| Hover nunca e a unica indicacao | Tooltips, labels e feedback funcionam sem mouse (teclado, touch) |
| Disabled nunca bloqueia informacao | Se um campo desabilitado tem valor, o usuario deve conseguir le-lo |
| Disabled explica o motivo | Tooltip no elemento disabled explicando por que esta inativo |
| Focus visivel e obrigatorio | Nunca remover o indicador de foco. Ver [09-Acessibilidade.md](09-Acessibilidade.md) |
| Cores de hover sao azuladas | Em todo o DS, hover usa tons azulados (#F5F8FF, #E3EFFF), conectando com a Primary |
| Active e momentaneo | O estado Active dura apenas enquanto o mousedown esta pressionado |
| Loading bloqueia interacao | Elemento em loading nao aceita cliques adicionais (previne double-submit) |
| Transicoes consistentes | Todos os hovers usam 100ms ease-out, todos os focus usam 100ms ease-out |
| Cursores semanticos | `pointer` (clicavel), `not-allowed` (disabled), `text` (input), `default` (read-only) |

### Tabela resumo de cursores

| Contexto | Cursor |
|---------|--------|
| Botao, link, card clicavel | `pointer` |
| Input editavel | `text` |
| Input read-only | `default` |
| Elemento disabled | `not-allowed` |
| Resize handle | `ew-resize` / `ns-resize` |
| Arrastar | `grab` → `grabbing` |
| Carregando (area toda) | `wait` |
