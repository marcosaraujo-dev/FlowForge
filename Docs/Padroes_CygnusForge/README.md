# Design System da Empresa — Documentacao Central

Guia oficial de padroes visuais e de desenvolvimento para todos os sistemas da empresa.

> **Para analistas e produto:** Leia as secoes de Fundamentos e Componentes — nao e necessario conhecer codigo.
> **Para desenvolvedores:** Leia tudo. Use a secao WPF para implementacao em .NET/XAML.

---

## O que e o Design System?

O Design System da Empresa e o conjunto de decisoes de design compartilhadas entre todos os produtos da empresa. Ele define:

- Quais cores usar e em que situacoes
- Como organizar o texto em uma tela
- Como os botoes, formularios e tabelas devem se comportar
- Quais espacamentos e bordas usar

Seguir o Design System garante que todos os sistemas da empresa tenham a mesma aparencia e comportamento, facilitando o aprendizado dos usuarios e a manutencao pelos times de desenvolvimento.

---

## Publico-alvo

| Perfil | O que consultar |
|--------|----------------|
| **Analista / Produto** | Fundamentos, Componentes, Layout |
| **Desenvolvedor Frontend (WPF)** | Tudo + pasta WPF/ |
| **Desenvolvedor Frontend (React/Web)** | Fundamentos, Componentes, Exemplos-HTML + pasta React/ |
| **Code Reviewer (WPF)** | WPF/03-PadroesObrigatorios.md, WPF/04-AntiPatterns.md |
| **Code Reviewer (React)** | React/05-PadroesObrigatorios.md, React/06-AntiPatterns.md |

---

## Estrutura da Documentacao

```
Padroes_Empresa/
|
+-- 01-Fundamentos/          Design tokens — aplicavel a qualquer framework
|   +-- 00-Tokens.md         Tokens disponiveis (cores, espacamentos, tipografia)
|   +-- 01-Principios.md     Filosofia e principios do Design System
|   +-- 02-Cores.md          Paleta completa de cores
|   +-- 03-Tipografia.md     Escala tipografica e estilos de texto
|   +-- 04-Espacamentos.md   Grid, padding, margin e border radius
|   +-- 05-Icones.md         Catalogo de icones com codigos
|   +-- 06-Redacao-UX.md     Linguagem, textos, formatacao de dados
|   +-- 07-Sombras-Elevacao.md  Tokens de sombra e niveis de elevacao
|   +-- 08-Animacoes.md      Duracoes, easing, transicoes por componente
|   +-- 09-Acessibilidade.md WCAG AA, contraste, teclado, foco, leitor de tela
|   +-- 10-Estados-Interacao.md  Hover, Focus, Active, Disabled — padroes globais
|
+-- 02-Componentes/          Componentes UI — comportamento e visual
|   +-- 01-Botoes.md
|   +-- 02-Badges-Status.md
|   +-- 03-Cards.md
|   +-- 04-Formularios.md
|   +-- 05-Tabelas.md
|   +-- 06-Alertas.md
|   +-- 07-Tabs.md
|   +-- 08-Acordeoes.md
|   +-- 09-Tooltips.md
|   +-- 10-Loading-Estados.md
|   +-- 11-Menu-Lateral.md
|   +-- 12-Dialogs.md
|   +-- 13-Secoes-Formulario.md
|   +-- 14-Breadcrumb.md      Caminho de navegacao com links e item atual
|   +-- 15-Cards-Catalogo.md  Card de catalogo/conteudo (produto, artigo) — distinto do card de app
|
+-- 03-Layout/               Padroes de estrutura de tela
|   +-- 01-Estrutura-Paginas.md
|   +-- 02-Navegacao.md
|   +-- 03-Receitas-Tela.md  Blueprints completos por tipo de tela
|   +-- 04-Agrupamento-Filtro.md  Chips de categoria + filtro funcional para catalogos
|
+-- Exemplos-HTML/           Galeria visual interativa (abrir no navegador)
|   +-- index.html           Portal com todos os exemplos
|   +-- cores.html
|   +-- tipografia.html
|   +-- botoes.html
|   +-- badges.html
|   +-- cards.html
|   +-- formularios.html
|   +-- tabelas.html
|   +-- alertas.html
|   +-- icones.html
|
+-- WPF/                     Especifico WPF — .NET Framework 4.8 + XAML
|   +-- README.md
|   +-- 01-MVVM.md
|   +-- 02-CicloDeVida.md
|   +-- 03-PadroesObrigatorios.md
|   +-- 04-AntiPatterns.md
|   +-- 05-Converters.md
|   +-- 06-Templates.md
|   +-- 07-Componentes-Especificos.md
|
+-- React/                   Especifico React — TypeScript + Vite + Tailwind v4
    +-- README.md
    +-- 00-Stack.md              Stack obrigatoria, Vite, Tailwind v4, config base
    +-- 01-Componentizacao.md    Estrutura de pastas, SRP, categorias de componente
    +-- 02-Hooks.md              Custom hooks, TanStack Query, regras
    +-- 03-GerenciamentoEstado.md  Server state vs client state, Zustand
    +-- 04-Performance.md       Lazy loading, memo, PWA, staleTime
    +-- 05-PadroesObrigatorios.md  Checklist de PR, Tailwind-first, TypeScript strict
    +-- 06-AntiPatterns.md       Errado vs correto
    +-- templates/index.css      Template pronto de tokens CSS (@theme do Tailwind v4)
```

---

## Busca Rapida

### Por assunto

| Quero saber sobre... | Documento |
|---------------------|-----------|
| Qual cor usar para botao de acao principal | [02-Cores.md](01-Fundamentos/02-Cores.md) - Primary |
| Tamanho de fonte para titulos e texto normal | [03-Tipografia.md](01-Fundamentos/03-Tipografia.md) |
| Espacamento entre campos de formulario | [04-Espacamentos.md](01-Fundamentos/04-Espacamentos.md) |
| Codigo de um icone (editar, excluir, buscar...) | [05-Icones.md](01-Fundamentos/05-Icones.md) |
| Como criar botoes e quando usar cada variante | [01-Botoes.md](02-Componentes/01-Botoes.md) |
| Como mostrar status de um registro | [02-Badges-Status.md](02-Componentes/02-Badges-Status.md) |
| Como estruturar um card | [03-Cards.md](02-Componentes/03-Cards.md) |
| Como organizar campos em um formulario | [04-Formularios.md](02-Componentes/04-Formularios.md) |
| Como exibir dados tabulares | [05-Tabelas.md](02-Componentes/05-Tabelas.md) |
| Como mostrar erros e mensagens de sucesso | [06-Alertas.md](02-Componentes/06-Alertas.md) |
| Como organizar conteudo em abas | [07-Tabs.md](02-Componentes/07-Tabs.md) |
| Como criar secoes colapsaveis | [08-Acordeoes.md](02-Componentes/08-Acordeoes.md) |
| Como adicionar dicas ao passar o mouse | [09-Tooltips.md](02-Componentes/09-Tooltips.md) |
| Como mostrar carregamento ou tela vazia | [10-Loading-Estados.md](02-Componentes/10-Loading-Estados.md) |
| Como criar o menu lateral de navegacao | [11-Menu-Lateral.md](02-Componentes/11-Menu-Lateral.md) |
| Como criar janelas modais e confirmacoes | [12-Dialogs.md](02-Componentes/12-Dialogs.md) |
| Como usar breadcrumb de navegacao | [14-Breadcrumb.md](02-Componentes/14-Breadcrumb.md) |
| Como estruturar um card de catalogo/conteudo (produto, artigo, skill) | [15-Cards-Catalogo.md](02-Componentes/15-Cards-Catalogo.md) |
| Como estruturar uma pagina completa | [01-Estrutura-Paginas.md](03-Layout/01-Estrutura-Paginas.md) |
| Blueprint pronto para montar uma tela (Lista, Form, Wizard...) | [03-Receitas-Tela.md](03-Layout/03-Receitas-Tela.md) |
| Como criar chips de categoria com filtro funcional (catalogo) | [04-Agrupamento-Filtro.md](03-Layout/04-Agrupamento-Filtro.md) |
| Como escrever titulos, botoes, mensagens e labels | [06-Redacao-UX.md](01-Fundamentos/06-Redacao-UX.md) |
| Formato de datas, moeda, CPF, CNPJ | [06-Redacao-UX.md](01-Fundamentos/06-Redacao-UX.md#7-formatacao-de-dados) |
| Qual sombra usar em cards, modais, dropdowns | [07-Sombras-Elevacao.md](01-Fundamentos/07-Sombras-Elevacao.md) |
| Duracao e easing de animacoes e transicoes | [08-Animacoes.md](01-Fundamentos/08-Animacoes.md) |
| Contraste, navegacao por teclado, leitor de tela | [09-Acessibilidade.md](01-Fundamentos/09-Acessibilidade.md) |
| Cores de hover, focus, disabled de qualquer componente | [10-Estados-Interacao.md](01-Fundamentos/10-Estados-Interacao.md) |

### Para desenvolvedores WPF

| Preciso... | Documento |
|-----------|-----------|
| Entender o padrao MVVM | [WPF/01-MVVM.md](WPF/01-MVVM.md) |
| Entender o ciclo de vida de uma View | [WPF/02-CicloDeVida.md](WPF/02-CicloDeVida.md) |
| Checklist para submeter um PR | [WPF/03-PadroesObrigatorios.md](WPF/03-PadroesObrigatorios.md) |
| Saber o que nunca fazer | [WPF/04-AntiPatterns.md](WPF/04-AntiPatterns.md) |
| Criar um Converter ou Behavior | [WPF/05-Converters.md](WPF/05-Converters.md) |
| Template de View/ViewModel pronto | [WPF/06-Templates.md](WPF/06-Templates.md) |
| Usar DiagnosticPanel ou LookupComboBox | [WPF/07-Componentes-Especificos.md](WPF/07-Componentes-Especificos.md) |

### Para desenvolvedores React

| Preciso... | Documento |
|-----------|-----------|
| Montar a estrutura inicial de um projeto (Vite + Tailwind v4) | [React/00-Stack.md](React/00-Stack.md) |
| Entender componentizacao e SRP | [React/01-Componentizacao.md](React/01-Componentizacao.md) |
| Usar hooks corretamente (TanStack Query) | [React/02-Hooks.md](React/02-Hooks.md) |
| Decidir onde guardar estado (Zustand) | [React/03-GerenciamentoEstado.md](React/03-GerenciamentoEstado.md) |
| Otimizar performance (lazy loading, memo, PWA) | [React/04-Performance.md](React/04-Performance.md) |
| Checklist para submeter um PR | [React/05-PadroesObrigatorios.md](React/05-PadroesObrigatorios.md) |
| Saber o que nunca fazer | [React/06-AntiPatterns.md](React/06-AntiPatterns.md) |

> **Implementacao de referencia:** projeto `OffWork` (`web/`) — todos os exemplos de codigo do guia React foram extraidos/adaptados dele. Ver [React/README.md](React/README.md).

---

## Ver exemplos visuais

Abra `Exemplos-HTML/index.html` no navegador para ver todos os componentes renderizados com as cores e estilos corretos.

---

## Como contribuir

1. Identifique o documento a atualizar (ou criar, caso seja algo novo)
2. Mantenha a separacao: conteudo generico em `01-Fundamentos/` ou `02-Componentes/`, codigo WPF em `WPF/`
3. Ao adicionar componente novo, inclua tambem o exemplo HTML em `Exemplos-HTML/`
4. Atualize este README com a nova entrada na tabela de busca rapida
5. Comunique a mudanca ao time

---

*Ultima atualizacao: Setembro 2026 | Mantido por: Time de Desenvolvimento*
