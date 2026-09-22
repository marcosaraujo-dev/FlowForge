# Seções de Formulário

Formulários longos devem ser divididos em grupos lógicos de campos, chamados de **seções**. Cada seção agrupa campos com relacionamento semântico, facilitando a leitura e o preenchimento.

> Exemplo visual interativo: [Exemplos-HTML/secoes-formulario.html](../Exemplos-HTML/secoes-formulario.html)

---

## Índice

1. [Quando dividir em seções](#1-quando-dividir-em-seções)
2. [Variantes de seção](#2-variantes-de-seção)
3. [Espaçamentos padrão](#3-espaçamentos-padrão)
4. [Separadores](#4-separadores)
5. [Layouts de coluna dentro de uma seção](#5-layouts-de-coluna-dentro-de-uma-seção)
6. [Regras obrigatórias](#6-regras-obrigatórias)

---

## 1. Quando dividir em seções

Divida um formulário em seções quando:

- O formulário tem **mais de 6 campos**
- Os campos podem ser agrupados em **categorias distintas** (dados pessoais, dados contratuais, endereço, etc.)
- O usuário pode preencher cada grupo de forma independente

**Não crie seções** para formulários curtos (até 4 campos) — a separação aumenta a complexidade sem benefício.

---

## 2. Variantes de seção

### Variante 1 — Separador + título (padrão)

A mais comum. Uma linha divisória separa as seções e um título indica o grupo.

```
─────────────────────────────────────
Dados Contratuais
Vínculo empregatício e cargo

[ Empresa * ]          [ Matrícula ]
[ Data Admissão * ]    [ Cargo ]
[ Salário Base ]       [ Jornada ]
```

**Especificações:**
- `margin-top` acima do separador: **28px** (`Space7`)
- `padding-top` após o separador: **24px** (`Space6`)
- Margem abaixo do título: **18px** (`Space4 + Space1`)
- Cor do separador: `BorderColor` (`#DEE2E6`)
- Título da seção: 14px, SemiBold, `TextPrimaryColor`
- Subtítulo (opcional): 12px, `TextSecondaryColor`
- Ícone ao lado do título: 14px, `TextSecondaryColor` (Segoe MDL2)

---

### Variante 2 — Seção em cartão (fundo cinza)

Cada seção é um card independente com fundo `BackgroundColor`. Usa mais espaço mas dá distinção visual máxima entre grupos.

```
┌──────────────────────────────────┐
│ 📋 Identificação da Empresa      │  ← título com ícone
│                                   │
│ [ Razão Social ]  [ CNPJ ]       │
└──────────────────────────────────┘

┌──────────────────────────────────┐
│ 📍 Endereço                      │
│                                   │
│ [ Logradouro + Nº ]   [ CEP ]    │
│ [ Município ]  [ UF ] [ Bairro ] │
└──────────────────────────────────┘
```

**Especificações:**
- Fundo: `BackgroundColor` (`#F8F9FA`)
- Borda: 1px `BorderColor`
- Border radius: `CornerRadius.Medium` (8px)
- Padding interno: **18px**
- Gap entre cartões: **12px**
- Título: 14px, SemiBold, `PrimaryColor`

---

### Variante 3 — Seções numeradas

Quando a ordem importa e o usuário deve preencher sequencialmente (fluxo guiado, não wizard).

```
① Seleção de Empresa
   Qual empresa vai emitir este documento?

   [ Empresa * ]    [ Competência * ]

② Seleção de Template
   Qual modelo de documento será gerado?

   [ Template * ]   [ Modo de Geração ]

③ Confirmação  ← (bloqueado até ① e ② estarem completos)
```

**Especificações:**
- Numeração: círculo 26px, fundo `PrimaryColor`, texto branco, 12px Bold
- Seções pendentes/bloqueadas: numeração com fundo `SecondaryColor`
- Gap entre seções numeradas: **24px**

---

## 3. Espaçamentos padrão

| Elemento | Token | Valor |
|----------|-------|-------|
| Padding do card/formulário | `Padding.Card` | 20px |
| Padding do formulário completo (tela) | `Padding.Container` | 24px |
| Entre seções (margin-top do separador) | `Space7` | 28px |
| Entre título de seção e campos | 18px | — |
| Gap entre campos (grid) | `Space4` | 16px |
| Gap entre linhas de campos | `Space4` | 16px |
| Padding do card de seção cinza | 18px | — |
| Gap entre cards de seção | `Space3` | 12px |

---

## 4. Separadores

### Linha simples

Separação discreta entre grupos de campos. Uso padrão.

```xml
<!-- WPF -->
<Separator Margin="0,28,0,24" Background="{StaticResource BorderColor}"/>
```

```css
/* CSS */
.divider { border-top: 1px solid var(--color-border); margin: 28px 0 24px; }
```

---

### Linha com rótulo

Quando a seção precisa de um nome mas sem o peso de um título grande. Útil para seções opcionais ou complementares.

```
─────── Informações Adicionais ───────
```

**Especificações:**
- Texto: 11px, SemiBold, `TextSecondaryColor`, uppercase
- Linha: 1px `BorderColor` de cada lado do texto
- Margin: 24px acima e abaixo

---

## 5. Layouts de coluna dentro de uma seção

Use grades para organizar os campos horizontalmente. Sempre usando `HorizontalScrollBarVisibility="Disabled"` no WPF.

### 1 coluna — campos que ocupam largura total

Use para: campos de texto longo (nome completo, descrição, endereço), campos únicos em um grupo.

```
[ Nome Completo                           ]
[ Descrição do Cargo                      ]
```

### 2 colunas — o layout mais comum

Use para: pares de campos relacionados (CPF + RG, data admissão + data demissão, cidade + UF).

```
[ Razão Social              ]  [ CNPJ              ]
[ Data de Admissão          ]  [ Data de Demissão  ]
```

### 3 colunas — campos curtos e independentes

Use para: campos numéricos curtos, códigos, datas, valores monetários simples.

```
[ Banco         ]  [ Agência    ]  [ Conta Corrente ]
[ Competência   ]  [ Salário    ]  [ CBO            ]
```

### Misto — largura proporcional

Quando os campos têm importâncias diferentes na linha.

```
[ Logradouro                  ]  [ Número  ]  [ Complemento ]
[ Município              2fr  ]  [ UF 1fr  ]
```

### Regras de layout

- **Nunca misture** campos de 1 e 3 colunas na mesma linha sem razão visual clara
- **Campos obrigatórios** (`*`) ficam preferencialmente na coluna da esquerda
- **`col-span-full`** para campos que precisam de toda a largura dentro de um grid de N colunas
- Evite colunas com mais de 4 campos na mesma linha — dificulta a leitura em telas menores

---

## 6. Regras obrigatórias

### Nomenclatura dos títulos de seção

- **Curtos:** 2–4 palavras, substantivos no plural quando for lista ("Dados Pessoais", "Informações Bancárias")
- **Consistentes:** use o mesmo padrão em todas as telas do módulo
- **Sem verbos no imperativo** — título de seção não é instrução ("Preencha os dados..." → ❌)

### Campos obrigatórios

- Marcar sempre com `*` em vermelho (`DangerColor`) ao lado do label
- Incluir legenda no rodapé do formulário: `* Campos obrigatórios`
- Nunca omitir a marcação mesmo que "todos os campos sejam obrigatórios"

### Seções opcionais

- Indicar com badge: `Opcional` em cinza (`BadgeSecondaryBackground`)
- O usuário deve poder enviar o formulário sem preencher seções opcionais

### Campos desabilitados

- Fundo `#F8F9FA`, texto `TextDisabledColor`, borda `#E9ECEF`
- Incluir texto de ajuda explicando por que está desabilitado quando não for óbvio

### Rodapé de ações

- Sempre posicionado **no rodapé** do formulário ou card
- Alinhado à **direita**: [Cancelar] [Secundária?] [Ação Principal]
- Separado do último grupo de campos por borda `BorderColor`
- Padding: 14px vertical, igual ao do header do card

---

*Anterior: [12-Dialogs.md](12-Dialogs.md)*
