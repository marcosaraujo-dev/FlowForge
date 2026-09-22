# Custom Hooks e TanStack Query

Regras para criar e organizar hooks em projetos React CygnusForge.

---

## Onde ficam os hooks

| Tipo | Localizacao |
|------|------------|
| Hook de dados de uma feature | `features/{dominio}/hooks/use{Dominio}.ts` |
| Hook global (notificacoes, auth, layout) | `hooks/use{Nome}.ts` |
| Hook de formulario | Inline no componente (se simples) ou `features/{dominio}/hooks/use{Nome}Form.ts` |

---

## Padrao obrigatorio para hooks de dados (TanStack Query)

Todo hook que busca dados da API usa TanStack Query (`useQuery` ou `useMutation`). Nunca `useEffect` + `fetch`.

```typescript
// features/employees/hooks/useEmployees.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { api } from '@/services/api'
import type { Employee } from '@/types'

export function useEmployees() {
  return useQuery<Employee[]>({
    queryKey: ['employees'],
    queryFn: async () => {
      const { data } = await api.get('/employees')
      return data
    },
  })
}

export function useCreateEmployee() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: async (payload: CreateEmployeeDto) => {
      const { data } = await api.post('/employees', payload)
      return data
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['employees'] })
    },
  })
}
```

### QueryKeys — convencao de nomes

| Escopo | QueryKey | Exemplo |
|--------|----------|---------|
| Lista completa | `['{recurso}']` | `['employees']` |
| Item por ID | `['{recurso}', id]` | `['employees', 42]` |
| Lista filtrada | `['{recurso}', { filtro }]` | `['dashboard', teamId]` |
| Subrecurso | `['{recurso}', id, '{sub}']` | `['teams', 3, 'members']` |

### staleTime recomendado

| Tipo de dado | staleTime |
|-------------|-----------|
| Dados frequentemente alterados (ex: dashboard) | `0` (padrao) |
| Dados semi-estaticos (ex: times, cargos) | `5 * 60 * 1000` (5 min) |
| Dados praticamente estaticos (ex: feriados) | `24 * 60 * 60 * 1000` (24h) |
| Contador de notificacoes nao lidas | `0` + `refetchInterval: 60_000` |

---

## Hooks de mutacao com feedback

Padrao para mutacoes que precisam mostrar feedback ao usuario:

```typescript
export function useDeleteEmployee() {
  const queryClient = useQueryClient()

  return useMutation({
    mutationFn: (id: number) => api.delete(`/employees/${id}`),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['employees'] })
      // toast de sucesso aqui, se houver biblioteca de toast
    },
    onError: () => {
      // toast de erro aqui
    },
  })
}
```

---

## Hooks de UI (sem dados)

Para estado de UI complexo que se repete, extraia em hook:

```typescript
// Gerenciar abertura de modal + item selecionado
function useEmployeeModal() {
  const [isOpen, setIsOpen] = useState(false)
  const [selected, setSelected] = useState<Employee | null>(null)

  const open = (employee?: Employee) => {
    setSelected(employee ?? null)
    setIsOpen(true)
  }
  const close = () => {
    setSelected(null)
    setIsOpen(false)
  }

  return { isOpen, selected, open, close }
}
```

---

## Regras obrigatorias

1. **Nunca** buscar dados com `useEffect` + `fetch`/`axios` — sempre `useQuery`
2. **Nunca** invalidar `queryClient` fora de `onSuccess`/`onSettled` de mutacao
3. Nomeie sempre com prefixo `use`
4. Retorne objeto nomeado, nao array (exceto convencao do React: `useState`)
5. Um hook por arquivo
6. Nao use `any` — tipar sempre o retorno do `useQuery<T>`

---

## Exemplo completo

```typescript
// features/teams/hooks/useTeams.ts
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { api } from '@/services/api'
import type { Team, CreateTeamDto } from '@/types'

export function useTeams() {
  return useQuery<Team[]>({
    queryKey: ['teams'],
    queryFn: async () => {
      const { data } = await api.get('/teams')
      return data
    },
    staleTime: 5 * 60 * 1000,
  })
}

export function useTeam(id: number | null) {
  return useQuery<Team>({
    queryKey: ['teams', id],
    queryFn: async () => {
      const { data } = await api.get(`/teams/${id}`)
      return data
    },
    enabled: id !== null,
  })
}

export function useCreateTeam() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (dto: CreateTeamDto) => api.post('/teams', dto),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['teams'] }),
  })
}

export function useDeleteTeam() {
  const queryClient = useQueryClient()
  return useMutation({
    mutationFn: (id: number) => api.delete(`/teams/${id}`),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ['teams'] }),
  })
}
```
