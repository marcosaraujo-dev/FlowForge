# React — Guia de Desenvolvimento CygnusForge

Documentacao especifica para projetos React/TypeScript da CygnusForge.
A implementacao de referencia e o projeto **OffWork** (`web/`).

> Leia primeiro os **Fundamentos** (01-Fundamentos/) e **Componentes** (02-Componentes/) — este guia pressupoe esses conceitos.

---

## Indice

| Arquivo | Conteudo |
|---------|----------|
| [00-Stack.md](00-Stack.md) | Stack obrigatoria, Vite, Mantine, configuracao base |
| [01-Componentizacao.md](01-Componentizacao.md) | Estrutura de pastas, SRP, categorias de componente |
| [02-Hooks.md](02-Hooks.md) | Custom hooks, TanStack Query, regras |
| [03-GerenciamentoEstado.md](03-GerenciamentoEstado.md) | Server state vs client state, Zustand, decisao |
| [04-Performance.md](04-Performance.md) | Lazy loading, memo, PWA, staleTime |
| [05-PadroesObrigatorios.md](05-PadroesObrigatorios.md) | Checklist de PR, Mantine-first, TypeScript strict |
| [06-AntiPatterns.md](06-AntiPatterns.md) | O que nao fazer — errado vs correto |

---

## Busca rapida

| Preciso... | Documento |
|-----------|-----------|
| Montar a estrutura inicial de um projeto | [00-Stack.md](00-Stack.md) — secao "index.css template" |
| Decidir onde colocar um arquivo | [01-Componentizacao.md](01-Componentizacao.md) |
| Criar um hook de dados | [02-Hooks.md](02-Hooks.md) |
| Decidir onde guardar estado | [03-GerenciamentoEstado.md](03-GerenciamentoEstado.md) |
| Saber o componente Mantine correto para cada caso | [05-PadroesObrigatorios.md](05-PadroesObrigatorios.md) |
| Verificar se meu codigo tem anti-patterns | [06-AntiPatterns.md](06-AntiPatterns.md) |

---

## Implementacao de referencia

Todos os exemplos de codigo neste guia sao extraidos ou adaptados de `OffWork/web/src/`.
Ao ter duvida sobre um padrao, consulte o arquivo correspondente no projeto.

| Padrao | Arquivo de referencia |
|--------|----------------------|
| Sidebar dark | `components/layout/Sidebar.tsx` |
| Header com notificacoes | `components/layout/Header.tsx` |
| Card de metrica | `components/shared/StatCard.tsx` |
| Badge de status | `components/shared/StatusBadge.tsx` |
| Cabecalho de pagina | `components/shared/PageHeader.tsx` |
| Hook de dados | `features/employees/hooks/useEmployees.ts` |
| Store de autenticacao | `store/authStore.ts` |
| Cliente HTTP | `services/api.ts` |
| Formulario com Zod | `features/auth/components/LoginPage.tsx` |
