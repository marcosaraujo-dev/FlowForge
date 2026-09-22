# Gerenciamento de Estado

Define quando e como usar cada mecanismo de estado em projetos React CygnusForge.

---

## Arvore de decisao

```
O estado vem do servidor?
├── SIM → TanStack Query (useQuery / useMutation)
└── NAO → e compartilhado entre rotas diferentes?
           ├── SIM → Zustand store
           └── NAO → e compartilhado apenas com filhos diretos?
                      ├── SIM → useState + prop drilling (se < 3 niveis)
                      └── NAO → useState local
```

---

## 1. Estado do servidor — TanStack Query

Todo dado que vem da API e responsabilidade do TanStack Query.

```typescript
// ✅ BOM
function EmployeesPage() {
  const { data: employees = [], isLoading, isError } = useEmployees()
  // ...
}

// ❌ RUIM — estado de servidor no useState
function EmployeesPage() {
  const [employees, setEmployees] = useState([])
  useEffect(() => {
    api.get('/employees').then(r => setEmployees(r.data))
  }, [])
}
```

**Beneficios automaticos:** cache, deduplicacao de requests, refetch em foco, loading/error states.

---

## 2. Estado global de cliente — Zustand

Usado para estado que precisa ser acessado em qualquer lugar da arvore e nao vem da API.

**Casos de uso no CygnusForge:**
- Dados de autenticacao (user, token, companyId)
- Preferencias de UI persistidas

```typescript
// store/authStore.ts
import { create } from 'zustand'
import { persist } from 'zustand/middleware'

interface AuthStore {
  user: User | null
  login: (token: TokenResponse) => void
  logout: () => void
}

export const useAuthStore = create<AuthStore>()(
  persist(
    (set) => ({
      user: null,
      login: (token) => set({ user: parseJwt(token.accessToken) }),
      logout: () => set({ user: null }),
    }),
    { name: 'auth-storage' },
  ),
)
```

**Regras:**
- Um store por dominio (`authStore`, `settingsStore`, etc.)
- Selecione apenas o que usar: `useAuthStore((s) => s.user)` — evita re-renders
- Sem logica de negocio dentro do store (apenas setters simples)
- Nao armazene dados do servidor no Zustand — use TanStack Query

---

## 3. Estado local — useState

Para estado que nao precisa ser compartilhado fora do componente ou seus filhos diretos.

```typescript
// Estado de modal — local e certo
function EmployeesPage() {
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [selectedEmployee, setSelectedEmployee] = useState<Employee | null>(null)
  // ...
}

// Estado de formulario — gerenciado pelo React Hook Form, nao useState
function EmployeeModal() {
  const { register, handleSubmit } = useForm<EmployeeFormData>({
    resolver: zodResolver(schema),
  })
}
```

---

## 4. Formularios — React Hook Form + Zod

Todo formulario usa React Hook Form com validacao Zod. Nunca `useState` por campo.

```typescript
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { z } from 'zod'

const schema = z.object({
  name:  z.string().min(2, 'Nome deve ter no minimo 2 caracteres'),
  email: z.string().email('E-mail invalido'),
  role:  z.enum(['Employee', 'Manager', 'HR', 'CompanyAdmin']),
})

type FormData = z.infer<typeof schema>

function EmployeeModal() {
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<FormData>({
    resolver: zodResolver(schema),
    defaultValues: { role: 'Employee' },
  })

  const onSubmit = async (data: FormData) => {
    await createEmployee.mutateAsync(data)
    onClose()
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input {...register('name')} className="..." />
      {errors.name && <p className="text-xs text-red-500">{errors.name.message}</p>}
    </form>
  )
}
```

---

## Resumo rapido

| Situacao | Solucao |
|---------|---------|
| Buscar lista da API | `useQuery` em hook customizado |
| Criar/editar/deletar via API | `useMutation` em hook customizado |
| Usuario logado + token JWT | Zustand (`authStore`) com `persist` |
| Modal aberta/fechada | `useState` local no componente pai |
| Campos de formulario | React Hook Form + Zod |
| Filtros/search de uma lista | `useState` local na Page |
| Preferencias de tema | Zustand com `persist` |
