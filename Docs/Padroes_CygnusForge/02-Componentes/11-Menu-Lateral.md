# Menu Lateral (Sidebar Navigation)

O menu lateral e a principal estrutura de navegacao entre modulos e funcionalidades do sistema.

> Implementacao tecnica: [WPF/](../WPF/) | [React/](../React/)

---

## Indice

1. [Estrutura e Anatomia](#1-estrutura-e-anatomia)
2. [Estados do Menu](#2-estados-do-menu)
3. [Itens de Menu](#3-itens-de-menu)
4. [Hierarquia de Navegacao](#4-hierarquia-de-navegacao)
5. [Tokens de Cor](#5-tokens-de-cor-sidebar)
6. [Regras de Uso](#6-regras-de-uso)

---

## 1. Estrutura e Anatomia

```
+-- Sidebar (240px) --+     +-- Conteudo Principal --+
|                     |     |                         |
|  [Logo / Titulo]    |     |  [Header da pagina]     |
|  ________________   |     |                         |
|                     |     |  [Conteudo da modulo]   |
|  [Item de Menu 1]   |     |                         |
|  [Item de Menu 2]   |     |                         |
|  [>> Item Ativo]    |     |                         |
|  [Item de Menu 4]   |     |                         |
|     [Sub-item]      |     |                         |
|     [Sub-item]      |     |                         |
|                     |     |                         |
|  ________________   |     |                         |
|  [Info Usuario]     |     |                         |
+---------------------+     +-------------------------+
```

**Especificacoes:**
- Largura expandida: 220px
- Largura recolhida (so icones): 52px
- Fundo: `#2E3748` (`SidebarBackgroundBrush`)
- Sem borda direita separadora (transicao visual pelo contraste de cor)

---

## 2. Estados do Menu

### Expandido (padrao)

Mostra icone + texto de todos os itens.

### Recolhido (compact)

Mostra apenas icones. Tooltip aparece no hover com o nome do item.

### Responsivo

Em telas estreitas, o menu pode:
- Ficar recolhido por padrao
- Sobrepor o conteudo (drawer) ao ser aberto
- Ser controlado por um botao de hamburguer

---

## 3. Itens de Menu

### Item simples

```
[ Icone ]  Texto do Item
```

### Item ativo

```
[>> Icone ]  Texto do Item   <- destaque visual (fundo Primary light, texto Primary, borda esquerda)
```

**Estados de item:**

| Estado | Fundo | Texto | Detalhe |
|--------|-------|-------|---------|
| Normal | Transparente | `#94A3B8` (`SidebarTextBrush`) | Sem destaque |
| Hover | `#3A4660` (`SidebarHoverBrush`) | `#FFFFFF` | Cursor pointer |
| Ativo | `#1E3A6E` (`SidebarActiveBrush`) | `#FFFFFF` + SemiBold | Borda esquerda 3px `#4A90E2` |
| Desabilitado | Transparente | `#94A3B8` opacidade reduzida | Cursor not-allowed |

### Especificacoes de item

- Altura: ~44px (padding 12px vertical)
- Padding: 16px horizontal
- Icone: 16px, coluna fixa 20px
- Texto: 13px, Normal (inativo) / SemiBold (ativo)
- Borda esquerda do item ativo: 3px `#4A90E2` (`SidebarActiveAccentBrush`)

---

## 4. Hierarquia de Navegacao

### 1 nivel (simples)

```
Home
Funcionarios
Empresas
Relatorios
Configuracoes
```

### 2 niveis (com sub-itens)

```
Folha de Pagamento
  └─ Calcular
  └─ Relatorios
  └─ Parametros
Funcionarios
  └─ Cadastro
  └─ Ferias
```

**Sub-itens:**
- Aparecem quando o item pai e clicado (accordion)
- Indentados em 16-20px a direita
- Fonte levemente menor (13px) ou mesma (14px)

---

## 5. Tokens de Cor (Sidebar)

Todos os tokens estao definidos em `Brushes.xaml` (WPF) e devem ser replicados fielmente no HTML/CSS.

| Token WPF | Hex | Uso |
|-----------|-----|-----|
| `SidebarBackgroundBrush` | `#2E3748` | Fundo da sidebar |
| `SidebarHoverBrush` | `#3A4660` | Fundo do item no hover |
| `SidebarActiveBrush` | `#1E3A6E` | Fundo do item selecionado |
| `SidebarActiveAccentBrush` | `#4A90E2` | Borda esquerda do item ativo |
| `SidebarTextBrush` | `#94A3B8` | Texto e icones normais |
| `SidebarTextActiveBrush` | `#FFFFFF` | Texto e icones do item ativo |
| `HeaderBackgroundBrush` | `#184194` | Header global (logo area) |

---

## 6. Regras de Uso

- No maximo 2 niveis de hierarquia no menu lateral
- Itens com sub-menu mostram icone de seta (▼/▶) indicando que ha mais opcoes
- O item ativo deve sempre refletir a pagina atual — nunca deixe o menu sem item selecionado
- Grupos relacionados podem ter um separador ou titulo de secao entre eles
- Mantenha o numero de itens gerenciavel: idealmente menos de 10 itens no nivel principal

---

*Anterior: [10-Loading-Estados.md](10-Loading-Estados.md) | Proximo: [12-Dialogs.md](12-Dialogs.md)*
