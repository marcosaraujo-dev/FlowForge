# Agrupamento com Filtro por Categoria

Padrão para listagens/catálogos públicos que precisam ser navegados por categoria: uma barra de
chips (com contador) que filtra os itens de uma grade sem recarregar a página. Nasceu junto com o
[Card de Catálogo](../02-Componentes/15-Cards-Catalogo.md) na feature `card-catalogo-agrupamento`
(site institucional `cygnusforge`) e segue as decisões de projeto `AD-002` e `AD-003` registradas
em `.specs/STATE.md` deste repositório.

> Ver exemplo funcional: [Exemplos-HTML/cards-catalogo.html](../Exemplos-HTML/cards-catalogo.html)

---

## Índice

1. [Quando usar](#1-quando-usar)
2. [Princípios (AD-002 e AD-003)](#2-princípios-ad-002-e-ad-003)
3. [Contrato de Markup](#3-contrato-de-markup)
4. [Comportamento](#4-comportamento)
5. [Como aplicar num catálogo novo](#5-como-aplicar-num-catálogo-novo)
6. [Regras de Uso](#6-regras-de-uso)

---

## 1. Quando usar

Use este padrão quando uma página lista **3 ou mais itens agrupáveis por categoria** e o usuário
se beneficia de restringir a visualização a uma categoria por vez (produtos por tipo, artigos por
tema, skills por domínio). Não force o padrão em listas pequenas e homogêneas onde uma única
categoria já cobre tudo — nesse caso, um [Card de Catálogo](../02-Componentes/15-Cards-Catalogo.md)
simples, sem `.category-filter`, já resolve.

---

## 2. Princípios (AD-002 e AD-003)

Duas decisões de projeto (`.specs/STATE.md`) governam qualquer implementação deste padrão:

- **AD-002 — Contagem sempre em runtime.** O número ao lado de cada chip nunca é escrito à mão no
  HTML — é sempre calculado contando os itens presentes no DOM no momento do carregamento. Isso
  elimina o risco clássico de "esqueci de atualizar o contador quando adicionei um item novo".
- **AD-003 — Chips só existem via JS, nenhum card escondido por padrão no HTML.** A barra de
  filtro não tem markup próprio no HTML estático — é inteiramente gerada por JavaScript. Isso
  garante *progressive enhancement* de graça: sem JavaScript, não há chips para clicar, e como
  nenhum card é escondido por padrão, **todo o conteúdo continua visível e acessível**.

---

## 3. Contrato de Markup

O comportamento é dirigido por dados — três atributos `data-*` conectam o HTML ao script:

```html
<div data-catalog data-catalog-categories='[
  {"key":"categoria-1","label":"Categoria 1"},
  {"key":"categoria-2","label":"Categoria 2"}
]'>
  <div class="catalog-grid" data-catalog-grid>
    <article class="catalog-card ..." data-category="categoria-1">...</article>
    <article class="catalog-card ..." data-category="categoria-2">...</article>
  </div>
</div>
```

| Atributo | Onde | Obrigatório | Papel |
| --- | --- | --- | --- |
| `data-catalog` | Container externo | Sim | Marca o escopo de um catálogo independente na página |
| `data-catalog-categories` | Mesmo container | Sim | JSON com a lista de categorias: `[{"key": "...", "label": "..."}]`, na ordem em que os chips devem aparecer |
| `data-catalog-grid` | Div que envolve os cards | Sim | Escopo da contagem/filtro — só filhos diretos com `data-category` são considerados |
| `data-category="<key>"` | Cada card, filho direto do grid | Sim (para participar do filtro) | Liga o card a uma `key` declarada em `data-catalog-categories` |
| `data-catalog-filter` | Elemento vazio (opcional) | Não | Onde a barra de chips é inserida — se ausente, o script cria um `<div>` antes do grid |
| `data-catalog-empty` | Elemento com texto (opcional) | Não | Mensagem exibida quando o filtro ativo não tem nenhum item — se ausente, o script cria um `<p>` com texto padrão |

**A `key` é uma chave técnica** (sem acento, sem espaço — ex. `ferramenta-dev`), **o `label` é o
texto exibido no chip** (com acento e formatação normal — ex. "Ferramenta Dev").

Múltiplos `[data-catalog]` podem existir na mesma página — cada um filtra só os cards dentro do
seu próprio `data-catalog-grid`, sem interferir nos demais.

---

## 4. Comportamento

- No carregamento da página, o script varre cada `[data-catalog]`, lê `data-catalog-categories`,
  conta quantos cards de cada `key` existem, e gera os chips: primeiro **"Todos"** (contagem =
  total), depois um chip por categoria declarada, na ordem do JSON.
- Categoria declarada sem nenhum card correspondente ainda aparece como chip, mostrando `(0)`.
- Clicar num chip aplica `hidden` nos cards que não pertencem àquela categoria (chip "Todos"
  remove `hidden` de todos) e marca o chip clicado como ativo (`.is-active` + `aria-pressed="true"`).
- Se o filtro ativo resultar em zero cards visíveis, o elemento de estado vazio (`data-catalog-empty`)
  é exibido.
- Em telas `<480px`, os chips quebram linha (`flex-wrap`) — nunca scroll horizontal.
- `data-catalog-categories` com JSON inválido não quebra a página: o script registra um aviso no
  console e simplesmente não gera chips para aquele catálogo (mesmo efeito de não ter JavaScript).

---

## 5. Como aplicar num catálogo novo

1. Envolva a grade de cards com `<div data-catalog data-catalog-categories='[...]'>` e
   `<div class="catalog-grid" data-catalog-grid>` dentro dela.
2. Marque cada card com `data-category="<key>"` correspondente a uma das chaves declaradas.
3. Use a variante de [Card de Catálogo](../02-Componentes/15-Cards-Catalogo.md) adequada ao
   conteúdo (`--product` ou `--article`).
4. Garanta que a página carrega `catalog.css` e `catalog-filter.js` (ajuste o caminho relativo
   conforme a profundidade da página).
5. Não escreva markup de chip nem esconda nenhum card manualmente — isso é responsabilidade do
   script (AD-003).
6. **Obrigatório**: garanta que o CSS do catálogo tem a regra `.catalog-card[hidden] { display: none; }`
   (já presente em `catalog.css`, mas **cada cópia/adaptação independente do componente — como um
   exemplo standalone com `<style>` inline — precisa repeti-la**). Sem ela, o filtro marca o
   atributo `hidden` corretamente mas o card continua aparecendo na tela: qualquer variante do card
   (`--product`, `--article`, ou uma nova que você criar) que declare `display: flex`/`grid`/`block`
   **sempre vence** o `[hidden] { display: none }` padrão do navegador — CSS de autor tem
   precedência sobre CSS de user-agent, independente de especificidade do seletor. Este bug já
   aconteceu uma vez nesta feature (Produtos e Artigos, além do próprio exemplo em
   `Exemplos-HTML/cards-catalogo.html`) e passou despercebido porque a verificação só checava a
   propriedade `hidden` via JS, nunca o estilo computado — ao testar um filtro, sempre confira
   `getComputedStyle(card).display` (ou olhe a tela de verdade), não só se o atributo foi setado.

Nenhuma outra configuração é necessária — o script se inicializa sozinho em `DOMContentLoaded`
para qualquer `[data-catalog]` presente na página.

---

## 6. Regras de Uso

### Obrigatório

- Toda categoria usada em algum `data-category` deve estar declarada em `data-catalog-categories`
  do mesmo `[data-catalog]` — um card com uma `key` não declarada fica invisível em qualquer
  filtro exceto "Todos" (comportamento intencional, não é bug)
- O contador de cada chip é sempre calculado pelo script — nunca escreva o número manualmente no
  HTML (AD-002)
- Cards que participam do filtro devem ser filhos **diretos** de `[data-catalog-grid]` — filhos
  aninhados mais fundo não são contados nem filtrados

### Proibido

- Não crie markup de chip estático no HTML — isso quebra o mecanismo de *progressive enhancement*
  do AD-003
- Não esconda nenhum card via `hidden`/`display:none` diretamente no HTML — a visibilidade inicial
  é sempre "tudo visível", controlada apenas pelo script depois do clique

---

*Anterior: [03-Receitas-Tela.md](03-Receitas-Tela.md)*
