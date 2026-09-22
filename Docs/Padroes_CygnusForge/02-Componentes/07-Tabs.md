# Tabs (Abas de Navegacao)

Tabs organizam conteudo relacionado em grupos, permitindo ao usuario alternar entre eles sem sair da pagina.

---

## Indice

1. [Quando usar Tabs](#1-quando-usar-tabs)
2. [Variantes](#2-variantes)
3. [Anatomia](#3-anatomia)
4. [Estados das Abas](#4-estados-das-abas)
5. [Regras de Uso](#5-regras-de-uso)

---

## 1. Quando usar Tabs

Use Tabs quando:
- O conteudo pode ser dividido em **categorias paralelas** (nao em sequencia)
- O usuario precisa **alternar frequentemente** entre as categorias
- O conteudo de cada aba e **independente** dos outros (nao e um wizard com passos)

**Nao use Tabs quando:**
- O conteudo e uma sequencia de passos (use Wizard/Stepper)
- Voce tem menos de 2 ou mais de 7 abas
- A diferenca entre as abas e muito pequena (considere acordeoes ou secoes)

---

## 2. Variantes

### Underline Tabs — Padrao

A variante padrao e recomendada. Tab ativa tem underline na cor Primary.

```
[ Aba 1 ]  [ Aba 2 ]  [ Aba Ativa ]  [ Aba 4 ]
                       ____________
```

- Fundo do header: transparente ou levemente cinza
- Texto ativo: Primary `#184194`, SemiBold
- Underline ativo: 2px solid `#184194`
- Texto inativo: Text Secondary `#6C757D`
- Hover: texto Primary, sem underline

**Uso:** Paginas principais, conteudo de modulos, telas de detalhe.

---

### Boxed Tabs

Abas com fundo preenchido. A tab ativa tem fundo branco, as inativas ficam sobre o fundo cinza.

```
+----------+  +----------+  +-----------+
|  Aba 1   |  |  Aba 2   |  |  Aba Ativa|
+----------+  +----------+  +-----------+
+------------------------------------------+
| Conteudo da aba ativa                    |
+------------------------------------------+
```

**Uso:** Abas dentro de cards, grupos de configuracao.

---

### Pills Tabs

Abas em formato de pilula. A ativa tem fundo colorido.

```
[ Aba 1 ]   [ Aba 2 ]   [Aba Ativa]   [ Aba 4 ]
                         (com fundo)
```

**Uso:** Filtros de listagem, segmentacao de dados.

---

## 3. Anatomia

```
+--Tab-Header--------------------------------------+
| [ Aba 1 ] [ Aba 2 ] [ Aba Ativa ] [ Aba 4 ]     |
|                     [___________]                |  <- underline 2px
+--------------------------------------------------+
|                                                  |
|   Conteudo da aba ativa                          |
|   (ScrollViewer se o conteudo for longo)         |
|                                                  |
+--------------------------------------------------+
```

**Especificacoes:**
- Altura do tab header: 40-44px
- Padding da aba: 12px horizontal, 8px vertical
- Fonte: 14px, Normal (inativo) / SemiBold (ativo)
- Separador entre header e conteudo: borda ou sombra sutil

---

## 4. Estados das Abas

| Estado | Visual |
|--------|--------|
| **Ativo** | Texto Primary + underline 2px (Underline variant) ou fundo destaque |
| **Inativo** | Texto Secondary, sem destaque |
| **Hover** | Texto Primary, cursor pointer |
| **Desabilitado** | Texto Disabled, cursor not-allowed, opacidade reduzida |

---

## 5. Regras de Uso

- O texto de cada aba deve ser curto: 1-2 palavras
- A aba padrao ao abrir a pagina deve ser a mais relevante para o fluxo do usuario
- Nunca use icones sozinhos em abas sem texto — a leitura e mais lenta
- O conteudo de uma aba nao deve ter referencia ao conteudo de outra aba
- Se o conteudo de uma aba requer acao que impacta outra aba, prefira uma pagina com scroll em vez de tabs

---

*Anterior: [06-Alertas.md](06-Alertas.md) | Proximo: [08-Acordeoes.md](08-Acordeoes.md)*
