# Acordeoes e Expanders

Acordeoes permitem mostrar e ocultar secoes de conteudo, reduzindo a densidade visual da tela.

> Ver exemplos visuais: [Exemplos-HTML/acordeoes.html](../Exemplos-HTML/acordeoes.html)

---

## Quando usar Acordeoes

Use quando:
- A pagina tem muitas secoes de informacao e mostrar tudo de uma vez seria cansativo
- Parte do conteudo e opcional ou de uso pouco frequente
- O usuario precisa de uma visao geral antes de se aprofundar em uma secao

**Nao use quando:**
- O usuario provavelmente vai precisar de todas as secoes ao mesmo tempo
- A quantidade de informacao e pequena e nao justifica o componente
- As secoes sao etapas sequenciais (use Wizard/Stepper)

---

## Anatomia

```
+--Accordion Item-----------------------------+
|  [ v ]  Titulo da Secao              [open]  |  <- Header clicavel (PrimaryColor)
+---------------------------------------------+
|                                             |
|  Conteudo da secao (expandido)              |
|  Campos, texto, tabelas, etc               |
|                                             |
+---------------------------------------------+

+--Accordion Item-----------------------------+
|  [ > ]  Titulo da Secao            [closed] |  <- Header clicavel (fundo branco)
+---------------------------------------------+
```

**Especificacoes:**
- Altura do header: 44-48px
- Icone de toggle: seta (▼ aberto / ▶ fechado) ou +/-
- Padding interno do conteudo: 16px
- Animacao de abertura: 200-300ms ease-in-out

---

## Cores e Tokens

O accordion utiliza a cor primaria da empresa para destacar a secao ativa.

### Header Aberto (ativo)

| Propriedade       | Token              | Valor     |
|-------------------|--------------------|-----------|
| Fundo             | `PrimaryColor`     | `#184194` |
| Texto             | `PrimaryTextColor` | `#FFFFFF` |
| Icone             | `PrimaryTextColor` | `#FFFFFF` |

### Header Fechado (inativo)

| Propriedade | Token                | Valor     |
|-------------|----------------------|-----------|
| Fundo       | `SurfaceColor`       | `#FFFFFF` |
| Texto       | `TextPrimaryColor`   | `#495057` |
| Icone       | `TextSecondaryColor` | `#6C757D` |

### Header Hover

| Propriedade        | Token                    | Valor     |
|--------------------|--------------------------|-----------|
| Fundo (fechado)    | `SidebarBackgroundColor` | `#F8F9FA` |
| Fundo (aberto)     | `PrimaryHoverColor`      | `#0F2D6B` |

### Bordas e Corpo

| Propriedade           | Token          | Valor     |
|-----------------------|----------------|-----------|
| Borda do container    | `BorderColor`  | `#DEE2E6` |
| Separador entre itens | `BorderColor`  | `#DEE2E6` |
| Fundo do corpo        | `SurfaceColor` | `#FFFFFF` |

---

## Estados

| Estado | Visual |
|--------|--------|
| **Aberto** | Icone ▼, header com fundo `PrimaryColor` e texto branco, conteudo visivel |
| **Fechado** | Icone ▶, header com fundo branco e texto escuro, conteudo oculto |
| **Hover (fechado)** | Fundo `SidebarBackgroundColor` no header |
| **Hover (aberto)** | Fundo `PrimaryHoverColor` no header |

---

## Variantes

### Padrao

Header aberto com fundo `PrimaryColor` (`#184194`) e texto branco. Multiplas secoes podem estar abertas simultaneamente.

### Compacto

Mesma aparencia do padrao, com padding reduzido. Use dentro de cards ou paineis com espaco limitado.

---

## Comportamento

### Unico aberto por vez vs multiplos

- **Unico aberto:** Abrir um fecha o anterior automaticamente. Use quando as secoes sao mutuamente exclusivas.
- **Multiplos abertos:** O usuario controla cada secao independentemente. Use quando o usuario pode precisar comparar secoes.

### Estado inicial

- Por padrao, a primeira secao relevante comeca aberta
- Se nenhuma secao e mais relevante que as outras, todas podem comecar fechadas

---

## Regras de Uso

- O titulo do accordion deve ser descritivo — o usuario decide abrir sem ver o conteudo
- Nao aninha acordeoes dentro de acordeoes
- O conteudo de um accordion pode incluir tabelas, formularios e outros componentes
- Mantenha consistencia: se uma secao tem border, todas devem ter
- **Sempre** usar `PrimaryColor` no header da secao aberta — nunca cinza ou outra cor

---

*Anterior: [07-Tabs.md](07-Tabs.md) | Proximo: [09-Tooltips.md](09-Tooltips.md)*
