# Stack e Configuracao Base

Define a stack obrigatoria para projetos React da CygnusForge e como configurar o ambiente inicial.

---

## Stack obrigatoria

| Camada | Biblioteca | Versao minima | Motivo |
|--------|-----------|---------------|--------|
| Framework | React | 19 | Concurrent features, use() hook |
| Linguagem | TypeScript | 5.x | Strict mode obrigatorio |
| Build | Vite | 6.x | HMR rapido, Rolldown |
| UI Kit / Estilos | Mantine (`@mantine/core` + `@mantine/hooks`) | 7.x | Componentes acessiveis prontos, theming via `theme.ts`, zero CSS-in-JS proprietario pago |
| Estado servidor | TanStack Query | 5.x | Cache, refetch, loading/error |
| Estado cliente | Zustand | 5.x | Simples, TypeScript-first |
| Rotas | React Router | 6.x | Nested routes, loaders |
| Icones | `@tabler/icons-react` | latest | Tree-shakeable, estilo de traco combina com Mantine |
| Formularios | `@mantine/form` + Zod | latest | Integrado ao Mantine, validacao |
| Dialogs/Overlays | Mantine (`Modal`, `Drawer`, `Popover`) | — | Ja acessiveis (foco, Escape, aria-modal) — nao precisa de Radix |
| Datas | date-fns + ptBR (e `@mantine/dates` para date pickers) | latest | Imutavel, locale |
| HTTP | Axios | 1.x | Interceptors, instancia compartilhada |

> **Por que Mantine e nao Tailwind+Radix?** Decisao de padrao (2026-09): Mantine assume o papel que antes era dividido entre Tailwind (utilitarios) e Radix (headless). Um unico sistema de tema (`theme.ts`) evita ter duas fontes de verdade de cor — a raiz do risco descrito na secao [Cores do Design System sao a fonte da verdade](#cores-do-design-system-sao-a-fonte-da-verdade-nunca-o-default-do-mantine) abaixo.

### Bibliotecas proibidas

- ❌ Moment.js — usar `date-fns`
- ❌ Lodash — usar metodos nativos ES2022+
- ❌ styled-components / emotion — usar o sistema de estilos do Mantine (`style`/`styles`/CSS Modules)
- ❌ FluentAssertions — usar Shouldly (testes)
- ❌ Redux — usar Zustand + TanStack Query
- ❌ Ant Design / MUI completo — ja temos Mantine como UI kit; nao duplicar
- ❌ Tailwind CSS / Radix UI em projeto novo — substituidos por Mantine (ver acima). Nao misturar os dois sistemas de reset/estilo no mesmo projeto
- ❌ Usar as cores padrao do Mantine (`color="blue"`, `color="red"`, `color="teal"` etc., os nomes que vem prontos no Mantine) — ver secao de cores abaixo

---

## Configuracao Mantine

### 1. Instalacao

```bash
npm install @mantine/core @mantine/hooks @mantine/form @mantine/notifications @tabler/icons-react
npm install -D postcss postcss-preset-mantine postcss-simple-vars
```

### 2. postcss.config.cjs — obrigatorio (breakpoints do Mantine)

```javascript
module.exports = {
  plugins: {
    'postcss-preset-mantine': {},
    'postcss-simple-vars': {
      variables: {
        'mantine-breakpoint-xs': '36em',
        'mantine-breakpoint-sm': '48em',
        'mantine-breakpoint-md': '62em',
        'mantine-breakpoint-lg': '75em',
        'mantine-breakpoint-xl': '88em',
      },
    },
  },
}
```

### 3. theme.ts — Template oficial CygnusForge

Copie este arquivo para `src/theme.ts` em todo novo projeto. **Nao altere os valores sem aprovacao do Design System** — eles sao a traducao direta dos tokens de [01-Fundamentos/00-Tokens.md](../01-Fundamentos/00-Tokens.md) para o formato de tupla de 10 tons que o Mantine exige (`MantineColorsTuple`).

```typescript
import { createTheme, type MantineColorsTuple } from '@mantine/core'

// PrimaryColor (#184194) fica no indice 6 — e o indice que os componentes
// "filled" do Mantine usam por padrao (Button, Badge, etc.)
const brand: MantineColorsTuple = [
  '#eef3fb', '#d9e5f4', '#b3cbe9', '#8babd9', '#5d85cb',
  '#3464bb', '#184194', '#0f2d6b', '#0a1e4a', '#061332',
]

const success: MantineColorsTuple = [
  '#f0fdf4', '#dcfce7', '#bbf7d0', '#86efac', '#4ade80',
  '#22c55e', '#16a34a', '#15803d', '#166534', '#14532d',
]

const danger: MantineColorsTuple = [
  '#fef2f2', '#fee2e2', '#fecaca', '#fca5a5', '#f87171',
  '#ef4444', '#dc2626', '#b91c1c', '#991b1b', '#7f1d1d',
]

// WarningColor oficial (#F59E0B) fica no indice 5, nao no 6 — use
// color="warning.5" explicitamente quando precisar do tom exato do token.
const warning: MantineColorsTuple = [
  '#fffbeb', '#fef3c7', '#fde68a', '#fcd34d', '#fbbf24',
  '#f59e0b', '#d97706', '#b45309', '#92400e', '#78350f',
]

const info: MantineColorsTuple = [
  '#eff6ff', '#dbeafe', '#bfdbfe', '#93c5fd', '#60a5fa',
  '#3b82f6', '#2563eb', '#1d4ed8', '#1e40af', '#1e3a8a',
]

const gray: MantineColorsTuple = [
  '#f9fafb', '#f3f4f6', '#e5e7eb', '#d1d5db', '#9ca3af',
  '#6b7280', '#4b5563', '#374151', '#1f2937', '#111827',
]

export const theme = createTheme({
  colors: { brand, success, danger, warning, info, gray },
  primaryColor: 'brand',
  primaryShade: 6,
  defaultRadius: 'md',
  fontFamily: 'Segoe UI, -apple-system, BlinkMacSystemFont, sans-serif',
  headings: { fontFamily: 'Segoe UI, -apple-system, BlinkMacSystemFont, sans-serif' },
})
```

### 4. main.tsx — MantineProvider

```tsx
import { MantineProvider } from '@mantine/core'
import '@mantine/core/styles.css'
import { theme } from './theme'

createRoot(document.getElementById('root')!).render(
  <MantineProvider theme={theme}>
    <App />
  </MantineProvider>
)
```

> Nunca instancie um segundo `MantineProvider` nem passe `theme` inline em componentes — o tema e global e unico, definido em `theme.ts`.

---

## Cores do Design System sao a fonte da verdade — nunca o default do Mantine

O Mantine vem com sua propria paleta padrao (`blue`, `red`, `green`, `teal`, `grape`...) que **nao tem nenhuma relacao com a marca CygnusForge**. Usar essas cores prontas do Mantine sem passar pelo `theme.ts` acima e a forma mais facil de uma tela sair da paleta oficial sem ninguem perceber no code review (o nome `color="red"` parece generico e passa despercebido).

**Regra:** todo uso de cor em componente Mantine usa um dos 6 nomes definidos em `theme.ts` (`brand`, `success`, `danger`, `warning`, `info`, `gray`) — nunca os nomes padrao do Mantine.

```tsx
// ❌ ERRADO — cor padrao do Mantine, fora da paleta CygnusForge
<Button color="blue">Salvar</Button>
<Badge color="red">Inativo</Badge>

// ✅ CORRETO — nomes do theme.ts, mapeados para os tokens do Design System
<Button color="brand">Salvar</Button>
<Badge color="danger">Inativo</Badge>
```

### Mapeamento: Token → Mantine color

| Token do Design System | Mantine `color` | CSS var equivalente | Hex |
|-----------------------|------------------|----------------------|-----|
| `PrimaryColor` | `brand` (shade 6, default) | `var(--mantine-color-brand-6)` | `#184194` |
| `PrimaryHoverColor` | `brand.7` | `var(--mantine-color-brand-7)` | `#0F2D6B` |
| `BackgroundColor` | `gray.0` | `var(--mantine-color-gray-0)` | `#F9FAFB` |
| `SurfaceColor` | `white` (token nativo do Mantine) | `var(--mantine-color-white)` | `#FFFFFF` |
| `SidebarDarkBackground` | `gray.9` | `var(--mantine-color-gray-9)` | `#111827` |
| `BorderColor` | `gray.2` | `var(--mantine-color-gray-2)` | `#E5E7EB` |
| `BorderInput` | `gray.3` | `var(--mantine-color-gray-3)` | `#D1D5DB` |
| `TextPrimaryColor` | `gray.9` | `var(--mantine-color-gray-9)` | `#111827` |
| `TextSecondaryColor` | `gray.7` | `var(--mantine-color-gray-7)` | `#374151` |
| `TextMuted` | `gray.5` | `var(--mantine-color-gray-5)` | `#6B7280` |
| `TextLight` | `gray.4` | `var(--mantine-color-gray-4)` | `#9CA3AF` |
| `SuccessColor` | `success` (shade 6, default) | `var(--mantine-color-success-6)` | `#16A34A` |
| `BadgeSuccessBackground` | `success.1` | `var(--mantine-color-success-1)` | `#DCFCE7` |
| `BadgeSuccessText` | `success.7` | `var(--mantine-color-success-7)` | `#15803D` |
| `DangerColor` | `danger` (shade 6, default) | `var(--mantine-color-danger-6)` | `#DC2626` |
| `BadgeErrorBackground` | `danger.1` | `var(--mantine-color-danger-1)` | `#FEE2E2` |
| `BadgeErrorText` | `danger.6` | `var(--mantine-color-danger-6)` | `#DC2626` |
| `WarningColor` | `warning.5` **(nao usar o default 6)** | `var(--mantine-color-warning-5)` | `#F59E0B` |
| `BadgeWarningBackground` | `warning.1` | `var(--mantine-color-warning-1)` | `#FEF3C7` |
| `BadgeWarningText` | `warning.7` | `var(--mantine-color-warning-7)` | `#B45309` |
| `InfoColor` | `info` (shade 6, default) | `var(--mantine-color-info-6)` | `#2563EB` |
| `BadgeInfoBackground` | `info.1` | `var(--mantine-color-info-1)` | `#DBEAFE` |
| `BadgeInfoText` | `info.7` | `var(--mantine-color-info-7)` | `#1D4ED8` |

> Componentes que aceitam `color` (Button, Badge, Alert, ThemeIcon, Loader...) usam o nome sozinho para pegar o shade default (6, exceto Warning). Para um shade especifico, use `color="brand.7"`. Fora de um componente Mantine (ex.: um SVG customizado), use a CSS var.

### 5. vite.config.ts padrao

```typescript
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: { '@': '/src' },
  },
})
```

### 6. tsconfig.json (strict obrigatorio)

```json
{
  "compilerOptions": {
    "target": "ES2022",
    "lib": ["ES2022", "DOM", "DOM.Iterable"],
    "module": "ESNext",
    "moduleResolution": "bundler",
    "strict": true,
    "noUnusedLocals": true,
    "noUnusedParameters": true,
    "noImplicitReturns": true,
    "jsx": "react-jsx",
    "baseUrl": ".",
    "paths": { "@/*": ["src/*"] }
  }
}
```

---

## Variantes de sidebar

O Design System suporta duas variantes de sidebar, montadas com `AppShell.Navbar` + `NavLink` do Mantine — nunca com `<div>`/`<a>` manuais.

### Dark (padrao web — OffWork)
Sidebar escura contrasta com o conteudo branco. Preferida para aplicacoes SaaS.
```tsx
<AppShell.Navbar bg="gray.9" c="gray.1">
  <NavLink
    label="Colaboradores"
    active={isActive}
    color="brand"          // ativo: fundo brand.6 (#184194), texto branco — automatico via variant="filled"
    variant="filled"
    styles={{ root: { '&:not([data-active]):hover': { backgroundColor: 'var(--mantine-color-gray-8)' } } }}
  />
</AppShell.Navbar>
```

### Light (padrao WPF / admin leve)
Sidebar clara, mesma familia de fundo que o conteudo. Preferida para ferramentas internas.
```tsx
<AppShell.Navbar bg="gray.0" c="gray.7" withBorder>
  <NavLink
    label="Colaboradores"
    active={isActive}
    color="brand"
    variant="light"         // ativo: fundo brand.0, texto brand.7 — automatico via variant="light"
  />
</AppShell.Navbar>
```

> Escolha a variante por projeto e mantenha consistente. Nao misture dentro do mesmo sistema. O estado hover/ativo do `NavLink` ja vem do `theme.ts` — nao redefina cores de hover fora do componente.

---

## Estrutura de pacote recomendada

```
web/
├── public/
│   ├── logo.png          # Logo CygnusForge
│   ├── favicon.ico
│   └── manifest.json     # PWA
├── src/
│   ├── theme.ts           # Paleta CygnusForge para o Mantine (ver acima)
│   ├── main.tsx          # Entry point + MantineProvider
│   ├── App.tsx           # Router
│   ├── components/
│   │   ├── layout/       # AppLayout, Sidebar, Header
│   │   ├── shared/       # PageHeader, StatCard, StatusBadge
│   │   └── ui/           # Wrappers finos sobre Mantine (defaults do Design System)
│   ├── features/         # Dominios de negocio
│   ├── hooks/            # Hooks globais
│   ├── lib/
│   │   └── utils.ts      # cn(), formatters
│   ├── services/
│   │   └── api.ts        # Instancia Axios
│   ├── store/            # Stores Zustand
│   └── types/
│       └── index.ts      # Tipos globais
└── package.json
```

Veja detalhes em [01-Componentizacao.md](01-Componentizacao.md).
