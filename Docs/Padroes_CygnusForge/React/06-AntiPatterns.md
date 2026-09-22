# Anti-Patterns React — O Que Nao Fazer

Erros comuns em projetos React. Para cada anti-pattern: o problema, o sintoma e a correcao.

---

## 1. Estilo inline para aplicar design tokens

**Problema:** Bypassa o `theme.ts` do Mantine e cria valores magic fora da paleta.

```tsx
// ❌ ERRADO
<button style={{ backgroundColor: '#184194', color: 'white', padding: '8px 16px' }}>
  Salvar
</button>

// ✅ CORRETO
<Button color="brand">Salvar</Button>
```

---

## 2. Cor padrao do Mantine ou hexadecimal hardcoded

**Problema:** O Mantine vem com paleta propria (`blue`, `red`, `teal`...) sem relacao com a marca; usar hex cru inventa cor fora da paleta e impede busca/substituicao global se o token mudar.

```tsx
// ❌ ERRADO — cor padrao do Mantine, nunca passou pelo Design System
<Badge color="blue">Novo</Badge>

// ❌ ERRADO — hex cru
<Box bg="#184194">

// ✅ CORRETO — nome definido em theme.ts (ver 00-Stack.md)
<Badge color="brand">Novo</Badge>
```

---

## 3. Template literal para classes condicionais

**Problema:** Gera classes com espaco vazio; dificulta ler qual classe se aplica em cada estado.

```tsx
// ❌ ERRADO
<div className={`${styles.item} ${isActive ? styles.itemActive : ''}`}>

// ✅ CORRETO
<div className={cn(styles.item, isActive && styles.itemActive)}>
```

---

## 4. Buscar dados com useEffect + fetch

**Problema:** Sem cache, sem deduplicacao, sem loading/error automatico, race conditions.

```typescript
// ❌ ERRADO
function EmployeesPage() {
  const [employees, setEmployees] = useState<Employee[]>([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    fetch('/api/employees')
      .then(r => r.json())
      .then(data => {
        setEmployees(data)
        setLoading(false)
      })
  }, [])
}

// ✅ CORRETO
function EmployeesPage() {
  const { data: employees = [], isLoading } = useEmployees()
}
```

---

## 5. Estado do servidor no Zustand

**Problema:** Zustand nao sabe quando o servidor mudou; dados ficam desatualizados.

```typescript
// ❌ ERRADO
const useAppStore = create((set) => ({
  employees: [],
  loadEmployees: async () => {
    const data = await api.get('/employees')
    set({ employees: data })
  },
}))

// ✅ CORRETO
// Dados do servidor sempre via TanStack Query
const { data: employees } = useEmployees()
// Zustand apenas para estado de cliente (auth, preferencias de UI)
```

---

## 6. Export default em componentes

**Problema:** Dificulta refactoring; ferramentas de busca perdem o nome.

```typescript
// ❌ ERRADO
export default function EmployeeModal() { ... }

// ✅ CORRETO
export function EmployeeModal() { ... }
```

---

## 7. Props nao tipadas / uso de any

**Problema:** Perde o valor do TypeScript; bugs de runtime silenciosos.

```typescript
// ❌ ERRADO
function StatCard({ label, value, icon, color }: any) { ... }

// ❌ ERRADO
function StatCard(props: any) { ... }

// ✅ CORRETO
interface StatCardProps {
  label: string
  value: number | string
  icon: React.ComponentType<{ size?: number | string; stroke?: number }>  // icone de @tabler/icons-react
  color?: 'brand' | 'success' | 'warning' | 'danger'
}
function StatCard({ label, value, icon: Icon, color = 'brand' }: StatCardProps) { ... }
```

---

## 8. God Component — componente com multiplas responsabilidades

**Problema:** Dificil testar, entender e manter.

```tsx
// ❌ ERRADO — EmployeesPage com 300+ linhas fazendo tudo
function EmployeesPage() {
  // busca de dados
  // logica de paginacao
  // logica de modal
  // logica de importacao CSV
  // renderizacao da tabela
  // renderizacao de filtros
  // renderizacao de modal
}

// ✅ CORRETO — separado por responsabilidade
function EmployeesPage() {
  // apenas orquestracao
  return (
    <>
      <PageHeader title="Colaboradores" action={<EmployeeActions />} />
      <EmployeeFilters />
      <EmployeeTable />
      <EmployeeModal />
      <ImportEmployeesModal />
    </>
  )
}
```

---

## 9. Hardcoding de strings de status/role

**Problema:** Magic strings; erros de digitacao silenciosos; dificil refatorar.

```typescript
// ❌ ERRADO
if (user.role === 'superadmin') { ... }
if (status === 2) { ... }

// ✅ CORRETO — enum em types/index.ts
enum UserRole {
  SuperAdmin = 'SuperAdmin',
  CompanyAdmin = 'CompanyAdmin',
  Manager = 'Manager',
  HR = 'HR',
  Employee = 'Employee',
}

if (user.role === UserRole.SuperAdmin) { ... }
```

---

## 10. Invalidar query fora de onSuccess

**Problema:** Invalida antes da mutacao completar; pode buscar dados velhos.

```typescript
// ❌ ERRADO
async function handleDelete(id: number) {
  await deleteEmployee.mutateAsync(id)
  queryClient.invalidateQueries({ queryKey: ['employees'] }) // roda mesmo se falhar
}

// ✅ CORRETO — dentro do onSuccess da mutacao
const deleteEmployee = useMutation({
  mutationFn: (id: number) => api.delete(`/employees/${id}`),
  onSuccess: () => queryClient.invalidateQueries({ queryKey: ['employees'] }),
})
```

---

## 11. Botao de icone sem label de acessibilidade

**Problema:** Inutil para leitores de tela; falha em auditoria de acessibilidade.

```tsx
// ❌ ERRADO
<button onClick={handleDelete}>
  <Trash2 size={16} />
</button>

// ✅ CORRETO
<button onClick={handleDelete} title="Excluir colaborador" aria-label="Excluir colaborador">
  <Trash2 size={16} />
</button>
```

---

## 12. Import de feature em outra feature

**Problema:** Acoplamento cross-feature; impossivel modularizar ou mover features.

```typescript
// ❌ ERRADO — teams importando de employees
import { EmployeeModal } from '@/features/employees/components/EmployeeModal'

// ✅ CORRETO — componente compartilhado vai para components/shared/
import { EmployeeSelect } from '@/components/shared/EmployeeSelect'
```

---

## 13. Nao usar o locale ptBR para formatacao de datas

**Problema:** Datas em ingles em sistema em portugues.

```typescript
// ❌ ERRADO
format(new Date(date), 'dd/MM/yyyy')          // sem locale — pode ter comportamento errado

// ✅ CORRETO
import { format } from 'date-fns'
import { ptBR } from 'date-fns/locale'

format(new Date(date), 'dd/MM/yyyy', { locale: ptBR })
format(new Date(date), "d 'de' MMMM 'de' yyyy", { locale: ptBR }) // "16 de maio de 2026"
```

---

## 14. Nao tratar estados de loading e erro

**Problema:** Tela em branco ou crash para o usuario; experiencia ruim.

```tsx
// ❌ ERRADO
function EmployeesPage() {
  const { data: employees } = useEmployees()
  return <EmployeeTable employees={employees} /> // employees pode ser undefined
}

// ✅ CORRETO
function EmployeesPage() {
  const { data: employees = [], isLoading, isError } = useEmployees()

  if (isLoading) return <Center py="xl"><Loader color="brand" /></Center>
  if (isError)   return <Text ta="center" c="danger" py="xl">Erro ao carregar dados.</Text>

  return <EmployeeTable employees={employees} />
}
```
