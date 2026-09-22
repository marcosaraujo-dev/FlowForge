# Estrutura de Paginas

Define a anatomia padrao de uma tela dos sistemas da empresa e como os elementos se organizam no espaco.

---

## Indice

1. [Anatomia Geral de uma Tela](#1-anatomia-geral-de-uma-tela)
2. [Header de Pagina](#2-header-de-pagina)
3. [Area de Conteudo](#3-area-de-conteudo)
4. [Footer de Acoes](#4-footer-de-acoes)
5. [Layouts de Grade](#5-layouts-de-grade)
6. [Hierarquia de Conteudo](#6-hierarquia-de-conteudo)

---

## 1. Anatomia Geral de uma Tela

```
+-- Menu Lateral --+-- Area de Conteudo (flex: 1) -----------+
|                  |                                          |
|  [Logo]          |  +-- Page Header ----------------------+ |
|                  |  | Titulo da Pagina                    | |
|  [Menu Item 1]   |  | [Breadcrumb opcional]               | |
|  [Menu Item 2]   |  +-------------------------------------+ |
|  [>> Item Ativo] |                                          |
|  [Menu Item 4]   |  [Alertas de contexto, se houver]       |
|                  |                                          |
|                  |  +-- Conteudo Principal ---------------+ |
|                  |  | (ScrollViewer)                      | |
|                  |  | Cards, formularios, tabelas, etc    | |
|                  |  |                                     | |
|                  |  |                                     | |
|                  |  +-------------------------------------+ |
|                  |                                          |
|                  |  +-- Footer de Acoes ------------------+ |
|                  |  | [Cancelar]       [Acao Principal]   | |
|  [Info Usuario]  |  +-------------------------------------+ |
+------------------+------------------------------------------+
```

---

## 2. Header de Pagina

O header e o primeiro elemento da area de conteudo, antes de qualquer card ou formulario.

### Componentes do Header

```
[ Titulo da Pagina ]     <- PageTitle (24px SemiBold)
[ Subtitulo opcional ]   <- SecondaryText
[ ________________________________________ ]  <- linha divisora inferior (2px)
```

**Especificacoes:**
- Margem inferior: 24-30px (separa do conteudo)
- Borda inferior: 2px solid `#E9ECEF`
- Padding inferior: 15px

### Header com Acoes

Quando a pagina tem acoes globais (ex: "Novo", "Exportar"), elas ficam alinhadas a direita no header.

```
[ Titulo da Pagina ]         [ Novo ]  [ Exportar ]
[ _________________________________________ ]
```

---

## 3. Area de Conteudo

A area de conteudo principal fica entre o header e o footer de acoes.

### Padding da pagina

- Container principal: 24-30px de margem em todos os lados
- Nao encosta o conteudo nas bordas da janela

### Scroll

- O conteudo deve ter scroll vertical quando necessario
- O header de pagina e o footer de acoes ficam **fixos** (nao rolam com o conteudo)
- O menu lateral fica fixo

---

## 4. Footer de Acoes

A barra de acoes principal fica fixada na parte inferior da area de conteudo.

```
+-----------------------------------------------------+
|                                             [Voltar] [Salvar] |
+-----------------------------------------------------+
```

**Quando usar footer de acoes:**
- Formularios de cadastro ou edicao
- Wizards com botoes de "Anterior" e "Proximo"
- Qualquer tela onde as acoes precisam estar sempre visiveis independente do scroll

**Especificacoes:**
- Padding: 12-16px
- Borda superior: 1px solid `#DEE2E6`
- Fundo: Branco (levemente diferente do conteudo para separacao visual)
- Acoes alinhadas a direita

---

## 5. Layouts de Grade

### Grade de 1 Coluna

Para listas de registros, resultados de busca.

```
+-- Conteudo Principal --+
|  [Toolbar]             |
|  [Tabela de dados]     |
|                        |
+------------------------+
```

### Grade de 2 Colunas

Para formularios, paineis com sidebar.

```
+-- Conteudo --+ +-- Sidebar --+
|  Form Fields | | Filtros     |
|              | | Info extra  |
|              | |             |
+--------------+ +-------------+
  ~65-70%           ~30-35%
```

### Grade de 3+ Colunas (Cards de Dashboard)

```
+--------+ +--------+ +--------+ +--------+
| Card 1 | | Card 2 | | Card 3 | | Card 4 |
+--------+ +--------+ +--------+ +--------+
```

---

## 6. Hierarquia de Conteudo

Em uma pagina complexa, o conteudo segue esta ordem de importancia:

```
1. Header (contexto)
2. Alertas criticos (se houver)
3. Conteudo de maior prioridade para a tarefa atual
4. Conteudo complementar / detalhes
5. Footer de acoes
```

### Agrupamento

Conteudo relacionado deve estar visualmente proximo e agrupado em um mesmo card ou secao. Nao distribua informacoes do mesmo contexto em areas distantes da tela.

---

*Proximo: [02-Navegacao.md](02-Navegacao.md)*
