# Card de Catálogo

Card para exibir itens de um catálogo/conteúdo navegável — produtos, artigos, treinamentos,
skills. **É um componente distinto do [Card de app](03-Cards.md)**: o Card de app agrupa dados
de formulário/dashboard dentro de uma tela de sistema; o Card de Catálogo é a unidade de uma
listagem pública, pensada para aparecer em grade, ser filtrada por categoria e levar a uma
página de detalhe. Nasceu da feature `card-catalogo-agrupamento` (site institucional
`cygnusforge`) e vive como padrão reutilizável em `.catalog-card`.

> Ver exemplo funcional: [Exemplos-HTML/cards-catalogo.html](../Exemplos-HTML/cards-catalogo.html)
> Ver o padrão de agrupamento/filtro que normalmente acompanha este card: [03-Layout/04-Agrupamento-Filtro.md](../03-Layout/04-Agrupamento-Filtro.md)

---

## Índice

1. [Quando usar (e quando não usar)](#1-quando-usar-e-quando-não-usar)
2. [Anatomia](#2-anatomia)
3. [Variante: Produto](#3-variante-produto)
4. [Variante: Artigo](#4-variante-artigo)
5. [Tokens](#5-tokens)
6. [Regras de Uso](#6-regras-de-uso)

---

## 1. Quando usar (e quando não usar)

| Situação | Componente |
| --- | --- |
| Item de um catálogo público navegável (produto, artigo, curso, skill) que aparece em grade e pode ser filtrado por categoria | **Card de Catálogo** (`.catalog-card`) — este documento |
| Agrupamento de dados dentro de uma tela de sistema (formulário, dashboard, painel) | [Card de app](03-Cards.md) (`.card`) |
| Conteúdo institucional estático, sem grade nem filtro (ex.: cards de "Serviços" de uma página institucional) | Nenhum dos dois — não force o padrão de catálogo onde não há navegação/filtro real |

---

## 2. Anatomia

```
+------------------------------------------+
| [Icone]  Titulo                          |  <- __header (varia por variante)
|          Tagline/subtitulo opcional      |
|--------------------------------------------|
|  Descricao                                |  <- __body / __desc
|  [x] Feature 1                            |  <- __features (opcional, só na variante Produto)
|  [x] Feature 2                            |
|  [tag] [tag] [tag]                        |  <- __tags (tech stack, opcional)
|--------------------------------------------|
|  [Status]                    [CTA ->]     |  <- __footer (opcional)
+------------------------------------------+
```

**Classes base (compartilhadas pelas duas variantes):**

| Classe | Papel |
| --- | --- |
| `.catalog-card` | Container base — fundo, borda, radius, transição |
| `.catalog-card__header` | Cabeçalho com ícone + título |
| `.catalog-card__icon` | Ícone/emoji do item |
| `.catalog-card__title` | Título do item |
| `.catalog-card__tagline` | Subtítulo curto (variante Produto) |
| `.catalog-card__body` | Miolo com descrição |
| `.catalog-card__desc` | Texto de descrição |
| `.catalog-card__tags` | Lista de tags (ex.: stack técnica) |
| `.catalog-card__status` | Badge de status (ex.: "Disponível") |
| `.catalog-card__footer` | Rodapé com status + CTA |
| `.catalog-card__meta` | Metadado curto (ex.: data — variante Artigo) |

Cada variante decide quais dessas classes usa e como as organiza no espaço — ver seções 3 e 4.

---

## 3. Variante: Produto

`.catalog-card.catalog-card--product` — card vertical (header → body → footer), para itens com
mais informação: descrição longa, lista de funcionalidades, tags de tecnologia e status.

```html
<article class="catalog-card catalog-card--product" data-category="<categoria>">
  <div class="catalog-card__header">
    <div class="catalog-card__icon">🏖️</div>
    <div>
      <h3 class="catalog-card__title">Nome do Produto</h3>
      <p class="catalog-card__tagline">Frase curta de posicionamento</p>
    </div>
  </div>
  <div class="catalog-card__body">
    <p class="catalog-card__desc">Descrição do produto em 2-3 frases.</p>
    <ul class="catalog-card__features">
      <li>Funcionalidade em destaque 1</li>
      <li>Funcionalidade em destaque 2</li>
    </ul>
    <div class="catalog-card__tags">
      <span>Tecnologia 1</span><span>Tecnologia 2</span>
    </div>
  </div>
  <div class="catalog-card__footer">
    <span class="catalog-card__status catalog-card__status--active">Disponível</span>
    <a href="..." class="btn">Ver Produto →</a>
  </div>
</article>
```

**Especificações:**
- `__header`: padding `2rem 2rem 1.5rem`, ícone `56x56px` com fundo translúcido sobre um header colorido
- Um modificador extra por item (ex.: `.catalog-card--<nome-do-item>`) define o gradiente de fundo do header — a identidade visual de cada produto, não parte do componente genérico
- `__features`: lista com marcador `✓`, uma linha por item — não é a mesma coisa que `__tags` (tecnologia) nem `__desc` (texto corrido)
- `__status`: dois estados prontos — `--dev` (âmbar, ponto pulsante) e `--active` (verde, ponto fixo)
- Hover: eleva o card (`translateY(-6px)`) com sombra ampliada

---

## 4. Variante: Artigo

`.catalog-card.catalog-card--article` — card horizontal e compacto (ícone + conteúdo lado a
lado), para itens de listagem textual: artigos, posts, notas.

```html
<a href="..." class="catalog-card catalog-card--article" data-category="<categoria>">
  <div class="catalog-card__icon">🤖</div>
  <div>
    <div class="catalog-card__title">Título do artigo</div>
    <div class="catalog-card__desc">Resumo em 1-2 frases do que o artigo aborda.</div>
    <div class="catalog-card__meta">Mês de Ano · Categoria</div>
  </div>
</a>
```

**Especificações:**
- Layout `flex`, `gap: 16px`, ícone fixo `40x40px`
- Sem `__footer`/`__status`/`__tags`/`__features` — a variante Artigo é deliberadamente mais enxuta
- `__meta`: linha final pequena, geralmente "mês/ano · categoria"
- Hover: eleva levemente (`translateY(-2px)`) com sombra sutil
- O card inteiro é o link (`<a>`), diferente da variante Produto onde só o CTA no footer é clicável

---

## 5. Tokens

Tokens próprios e autocontidos (prefixo `--cc-`), definidos no topo de `catalog.css` — **não leem
tokens de cor de nenhuma página específica** (ver decisão de projeto `AD-001` em `.specs/STATE.md`
deste repositório). Isso existe para o componente não quebrar se uma página renomear sua própria
variável de cor.

| Token | Valor | Uso |
| --- | --- | --- |
| `--cc-accent` | `#184194` | Cor de destaque (borda ativa, ícone, chip ativo) |
| `--cc-accent-dark` | `#0F2D6B` | Hover/estado escuro do destaque |
| `--cc-bg` | `#FFFFFF` | Fundo do card |
| `--cc-border` / `--cc-border-light` | `#DEE2E6` / `#E9ECEF` | Bordas |
| `--cc-text` / `--cc-text-secondary` / `--cc-text-light` | `#495057` / `#6C757D` / `#ADB5BD` | Hierarquia de texto |
| `--cc-success-bg` / `--cc-success-text` / `--cc-success-dot` | verde | Status "Disponível" |
| `--cc-warn-bg` / `--cc-warn-text` / `--cc-warn-dot` | âmbar | Status "Em desenvolvimento" |
| `--cc-radius` / `--cc-radius-sm` | `12px` / `8px` | Border radius |
| `--cc-shadow` / `--cc-shadow-hover` | sombras sutil/ampliada | Elevação padrão/hover |
| `--cc-transition` | `0.3s ease` | Transições de hover |

---

## 6. Regras de Uso

### Obrigatório

- Use `.catalog-card` só para conteúdo de catálogo/navegação pública — nunca para agrupar dados de formulário/dashboard (isso é papel do [Card de app](03-Cards.md))
- Todo card de catálogo que participa de um grid filtrável leva `data-category="<chave>"` — ver [03-Layout/04-Agrupamento-Filtro.md](../03-Layout/04-Agrupamento-Filtro.md)
- Escolha a variante pelo tipo de conteúdo: mais informação estruturada (features, tags, status) → `--product`; item de listagem textual compacto → `--article`
- Não misture elementos de uma variante na outra (ex.: não adicione `__footer` com CTA na variante Artigo — se o item precisa disso, provavelmente é a variante Produto)

### Proibido

- Não crie uma terceira variante sem necessidade real — se o conteúdo não se encaixa em Produto nem Artigo, revise se o Card de Catálogo é mesmo o componente certo antes de estender o CSS
- Não leia tokens de cor de uma página específica (`--primary`, `--blue`, etc.) dentro de `catalog.css` — use sempre `--cc-*` (ver `AD-001`)

---

*Anterior: [14-Breadcrumb.md](14-Breadcrumb.md) | Próximo: [03-Layout/04-Agrupamento-Filtro.md](../03-Layout/04-Agrupamento-Filtro.md)*
