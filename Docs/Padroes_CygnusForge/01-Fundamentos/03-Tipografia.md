# Tipografia

A escala tipografica define os tamanhos, pesos e estilos de texto usados em toda a interface.

> Ver exemplos visuais: [Exemplos-HTML/tipografia.html](../Exemplos-HTML/tipografia.html)

---

## Indice

1. [Familia de Fontes](#1-familia-de-fontes)
2. [Escala de Tamanhos](#2-escala-de-tamanhos)
3. [Estilos de Texto](#3-estilos-de-texto)
4. [Hierarquia Visual](#4-hierarquia-visual)
5. [Regras de Uso](#5-regras-de-uso)

---

## 1. Familia de Fontes

| Tipo | Fonte | Uso |
|------|-------|-----|
| **Interface** | Segoe UI (Windows) / System UI | Todo o texto da aplicacao |
| **Icones** | Segoe MDL2 Assets | Icones em botoes e menus |
| **Codigo / Monospace** | Consolas | Valores tecnicos, codigos, hex |

> A fonte principal segue o sistema operacional. Em Windows usa Segoe UI, em outros sistemas usa a fonte padrao do SO.

---

## 2. Escala de Tamanhos

Todos os tamanhos sao tokens — use sempre o nome do token, nunca o valor direto.

| Token | Tamanho | Uso principal |
|-------|---------|---------------|
| `AppFontXSmall` | 10px | Texto muito pequeno, rodapes |
| `AppFontSmall` | 12px | Texto auxiliar, captions, H5 |
| `AppFontCaption` | 13px | Labels de campo, texto secundario, H4 |
| `AppFontNormal` | 14px | **Texto padrao** — corpo de texto, botoes |
| `AppFontMedium` | 16px | Subtitulos, destaques |
| `AppFontLarge` | 18px | H3, titulos de secao menores |
| `AppFontXLarge` | 20px | H2, titulos de secao |
| `AppFontXXLarge` | 22px | Titulos importantes |
| `AppFont3XLarge` | 24px | **Titulo de pagina** |

**Escala de acessibilidade:** O sistema suporta ampliacao de fonte via F2 (1.0x / 1.25x / 1.5x). Os tokens acima sao escalados automaticamente — nunca use valores fixos de fonte.

---

## 3. Estilos de Texto

Cada estilo combina tamanho + peso + cor para um papel especifico na interface.

### PageTitle — Titulo da pagina

- **Tamanho:** 24px (`AppFont3XLarge`)
- **Peso:** SemiBold
- **Cor:** Text Primary (`#495057`)
- **Uso:** Uma vez por pagina, no topo. Ex: "Gerenciamento de Funcionarios"

### SectionTitle — Titulo de secao

- **Tamanho:** 20px (`AppFontXLarge`)
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Cabecalho de cards, grupos de conteudo. Ex: "Dados Pessoais"

### H2TextStyle

- **Tamanho:** 22px (`AppFontXXLarge`)
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Titulos intermediarios importantes

### H3TextStyle

- **Tamanho:** 18px (`AppFontLarge`)
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Subtitulos dentro de secoes

### H4TextStyle

- **Tamanho:** 14px (`AppFontNormal`)
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Titulos de menor hierarquia, agrupamentos

### H5TextStyle

- **Tamanho:** 12px (`AppFontSmall`)
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Micro-titulos, categorias

---

### Subtitle — Subtitulo

- **Tamanho:** 14px
- **Peso:** Bold
- **Cor:** Text Primary
- **Uso:** Subtitulo imediatamente abaixo de um titulo principal

### BodyText — Texto de corpo

- **Tamanho:** 14px
- **Peso:** Normal
- **Cor:** Text Primary
- **Uso:** Texto corrido, descricoes, conteudo principal

### BodySmallTextStyle

- **Tamanho:** 12px
- **Peso:** Normal
- **Cor:** Text Secondary (`#6C757D`)
- **Uso:** Texto de suporte, descricoes curtas, legenda de campos

### SecondaryText — Texto secundario

- **Tamanho:** 13px
- **Peso:** Normal
- **Cor:** Text Secondary
- **Uso:** Informacoes complementares, metadados

### CaptionText — Legenda

- **Tamanho:** 12px
- **Peso:** Normal
- **Cor:** Text Light (`#ADB5BD`)
- **Uso:** Texto de ajuda, placeholder descriptivo abaixo de campos

### FieldLabel — Label de campo

- **Tamanho:** 13px
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Margem inferior:** 6px (afastamento do campo abaixo)
- **Uso:** Label de todo e qualquer campo de formulario

### PrimaryLineText — Texto principal em grids

- **Tamanho:** 13px
- **Peso:** SemiBold
- **Cor:** Text Primary
- **Uso:** Primeira linha de celulas de DataGrid com mais de uma informacao

### IconText — Texto de icone

- **Familia:** Segoe MDL2 Assets
- **Tamanho:** 14px
- **Uso:** Icones dentro de botoes e menus

---

## 4. Hierarquia Visual

Uma pagina bem estruturada usa no maximo 3-4 niveis de hierarquia tipografica:

```
PageTitle (24px SemiBold)
  Subtitulo curto (14px Bold)

  SectionTitle (20px SemiBold)
    BodyText (14px Normal)

    H3 (18px SemiBold)
      BodyText (14px Normal)
      CaptionText (12px — ajuda)

  FormSection:
    FieldLabel (13px SemiBold)  <- Label do campo
    [campo de input]
    CaptionText (12px)          <- Texto de ajuda opcional
```

---

## 5. Regras de Uso

### Obrigatorio

- Sempre use os estilos definidos — nunca defina tamanho ou peso de fonte diretamente no componente
- `FieldLabel` em todo label de formulario, sem excecao
- `PageTitle` apenas uma vez por pagina

### Peso de fonte

| Peso | Quando usar |
|------|------------|
| **SemiBold** | Titulos, labels, dados principais em grids |
| **Bold** | Subtitulos, destaque de texto inline |
| **Normal** | Texto corrido, descricoes, valores em campos |

### O que evitar

- Nao use fonte menor que 12px para texto legivel pelo usuario
- Nao coloque todo o texto em negrito — negrito e um sinal de importancia, perde o significado se usado em excesso
- Nao misture mais de 2 estilos de fonte na mesma linha de conteudo
- Nao use `PageTitle` dentro de cards ou secoes — so no cabecalho da pagina

---

*Anterior: [02-Cores.md](02-Cores.md) | Proximo: [04-Espacamentos.md](04-Espacamentos.md)*
