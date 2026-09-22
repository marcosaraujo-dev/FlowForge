# Componentizacao e Estrutura de Pastas

Define como organizar componentes React em projetos CygnusForge.

---

## Estrutura de pastas

```
src/
├── components/              # Componentes sem dependencia de dominio
│   ├── layout/              # Estrutura da pagina (nao muda por rota)
│   │   ├── AppLayout.tsx    # Shell: <Sidebar> + <Header> + <Outlet>
│   │   ├── Sidebar.tsx      # Menu lateral de navegacao
│   │   └── Header.tsx       # Barra superior
│   ├── shared/              # Reutilizaveis em qualquer feature
│   │   ├── PageHeader.tsx   # Titulo da pagina + botao de acao
│   │   ├── StatCard.tsx     # Card de metrica do dashboard
│   │   └── StatusBadge.tsx  # Badge de status com mapa de cores
│   └── ui/                  # Wrappers finos sobre componentes Mantine
│       ├── Button.tsx
│       ├── Input.tsx
│       └── Dialog.tsx
│
├── features/                # Um diretorio por dominio de negocio
│   └── employees/
│       ├── components/      # Componentes exclusivos desta feature
│       │   ├── EmployeesPage.tsx
│       │   ├── EmployeeModal.tsx
│       │   └── ImportEmployeesModal.tsx
│       ├── hooks/           # Custom hooks da feature
│       │   └── useEmployees.ts
│       ├── services/        # Chamadas de API da feature
│       │   └── employeeService.ts
│       └── .gitkeep         # Garante pastas vazias no git
│
├── hooks/                   # Hooks globais (nao ligados a uma feature)
├── lib/
│   └── utils.ts             # cn(), formatDate(), etc.
├── services/
│   └── api.ts               # Instancia Axios compartilhada
├── store/                   # Stores Zustand
│   └── authStore.ts
└── types/
    └── index.ts             # Tipos e enums globais
```

---

## Categorias de componente

### 1. Layout (`components/layout/`)
Definem a estrutura permanente da tela. Carregados uma vez, existem em toda navegacao.

**Regras:**
- Nao recebem dados de negocio diretamente (apenas do store de auth)
- Nao fazem chamadas de API proprias — exceto contadores de notificacao
- Um arquivo = um componente = uma responsabilidade

```tsx
// AppLayout.tsx — shell da aplicacao
export function AppLayout() {
  return (
    <AppShell navbar={{ width: 260, breakpoint: 'sm' }} padding={0}>
      <Sidebar />
      <AppShell.Main bg="gray.0">
        <Header />
        <Box p="lg">
          <Outlet />
        </Box>
      </AppShell.Main>
    </AppShell>
  )
}
```

### 2. Shared (`components/shared/`)
Componentes reutilizaveis sem acoplamento a dominio de negocio.

**Regras:**
- Props tipadas com interface explicita
- Sem imports de features especificas
- Sem chamadas de API
- Sem estado global (apenas props)

```tsx
// PageHeader.tsx — padrao para todas as paginas
interface PageHeaderProps {
  title: string
  description?: string
  action?: React.ReactNode
}

export function PageHeader({ title, description, action }: PageHeaderProps) {
  return (
    <Group justify="space-between" align="flex-start" mb="lg">
      <Stack gap={2}>
        <Title order={1} size="h3" c="gray.9">{title}</Title>
        {description && <Text size="sm" c="gray.5">{description}</Text>}
      </Stack>
      {action}
    </Group>
  )
}
```

### 3. Feature (`features/{dominio}/components/`)
Componentes que pertencem a um dominio especifico.

**Regras:**
- Podem importar shared/ e ui/ mas nao outras features
- Pages sao componentes de feature (sufixo `Page`)
- Modais sao componentes de feature (sufixo `Modal`)

```tsx
// EmployeesPage.tsx — componente de pagina de uma feature
export function EmployeesPage() {
  const { employees, isLoading } = useEmployees()

  return (
    <div>
      <PageHeader
        title="Colaboradores"
        action={<Button color="brand">Novo colaborador</Button>}
      />
      {/* conteudo */}
    </div>
  )
}
```

### 4. UI Primitivos (`components/ui/`)
Wrappers finos sobre componentes Mantine com defaults do Design System ja aplicados (cor, radius, tamanho) — evita repetir `color="brand"` em toda tela.

**Regras:**
- Sem logica de negocio
- Apenas defaults de estilizacao (nunca reimplementa acessibilidade — o Mantine ja cobre foco/aria)
- Nomeados como o componente Mantine correspondente (`PrimaryButton` = `Button` com `color="brand"`, `DangerButton` = `Button` com `color="danger"`)

---

## Regras de nomenclatura

| Tipo | Convencao | Exemplo |
|------|-----------|---------|
| Arquivo de componente | `PascalCase.tsx` | `EmployeeModal.tsx` |
| Arquivo de hook | `camelCase.ts` com prefixo `use` | `useEmployees.ts` |
| Arquivo de service | `camelCase.ts` com sufixo `Service` | `employeeService.ts` |
| Arquivo de store | `camelCase.ts` com sufixo `Store` | `authStore.ts` |
| Arquivo de tipo | `camelCase.ts` ou `index.ts` | `types/index.ts` |
| Interface de props | Nome do componente + `Props` | `StatCardProps` |
| Export do componente | Named export (nao default) | `export function StatCard` |

---

## Principio de Single Responsibility

Cada componente tem **uma razao para existir**. Quando um componente faz mais de uma coisa, extraia.

```tsx
// ❌ RUIM — componente fazendo tudo
export function EmployeesPage() {
  const [employees, setEmployees] = useState([])
  const [loading, setLoading] = useState(false)
  const [search, setSearch] = useState('')

  useEffect(() => {
    fetch('/api/employees').then(r => r.json()).then(setEmployees)
  }, [])

  const filtered = employees.filter(e => e.name.includes(search))

  return (
    <div>
      <input value={search} onChange={e => setSearch(e.target.value)} />
      <table>{/* 50 linhas de tabela */}</table>
    </div>
  )
}

// ✅ BOM — responsabilidades separadas
export function EmployeesPage() {
  const [search, setSearch] = useState('')
  const { employees, isLoading } = useEmployees()   // hook cuida dos dados
  const filtered = employees.filter(e => e.name.toLowerCase().includes(search.toLowerCase()))

  return (
    <div>
      <PageHeader title="Colaboradores" />
      <EmployeeSearch value={search} onChange={setSearch} />    // componente de busca
      <EmployeeTable employees={filtered} isLoading={isLoading} /> // componente de tabela
    </div>
  )
}
```

---

## Limites de tamanho

| Artefato | Limite recomendado |
|---------|-------------------|
| Componente | 150 linhas (JSX incluido) |
| Hook | 80 linhas |
| Service | 60 linhas por funcao |
| Funcao auxiliar | 20 linhas |

Acima do limite: extrair sub-componente, sub-hook ou utilitario.
