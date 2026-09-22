# Performance React

Tecnicas de performance usadas em projetos CygnusForge.

---

## Lazy loading de paginas

Toda pagina e carregada sob demanda com `React.lazy`. Nao importe paginas direto no App.tsx.

```typescript
// App.tsx
import { lazy, Suspense } from 'react'
import { Routes, Route } from 'react-router-dom'

const DashboardPage   = lazy(() => import('./features/dashboard/components/DashboardPage').then(m => ({ default: m.DashboardPage })))
const EmployeesPage   = lazy(() => import('./features/employees/components/EmployeesPage').then(m => ({ default: m.EmployeesPage })))
const VacationsPage   = lazy(() => import('./features/vacations/components/VacationsPage').then(m => ({ default: m.VacationsPage })))

function App() {
  return (
    <Suspense fallback={<Center h="100vh"><Loader color="brand" /></Center>}>
      <Routes>
        <Route element={<AppLayout />}>
          <Route path="/dashboard"  element={<DashboardPage />} />
          <Route path="/employees"  element={<EmployeesPage />} />
          <Route path="/vacations"  element={<VacationsPage />} />
        </Route>
      </Routes>
    </Suspense>
  )
}
```

---

## staleTime — evitar re-fetches desnecessarios

Configure `staleTime` baseado na frequencia de mudanca dos dados:

```typescript
// Dados de configuracao — quase nunca mudam
useQuery({
  queryKey: ['settings'],
  queryFn: fetchSettings,
  staleTime: 24 * 60 * 60 * 1000,  // 24 horas
})

// Lista de equipes — muda raramente
useQuery({
  queryKey: ['teams'],
  queryFn: fetchTeams,
  staleTime: 5 * 60 * 1000,  // 5 minutos
})

// Dashboard — muda frequentemente
useQuery({
  queryKey: ['dashboard', teamId],
  queryFn: () => fetchDashboard(teamId),
  staleTime: 0,  // sempre fresco
})
```

---

## refetchInterval — polling para dados em tempo real

Para contadores e metricas que precisam de atualizacao periodica:

```typescript
// Contador de notificacoes nao lidas
useQuery({
  queryKey: ['notifications-count'],
  queryFn: fetchNotificationCount,
  refetchInterval: 60_000,    // a cada 60 segundos
  staleTime: 0,
  retry: false,               // nao travar em erros de auth
})
```

---

## memo e useCallback — quando usar

Use `memo` e `useCallback` apenas quando houver problema mensuravel de performance, nao preventivamente.

```typescript
// Justificado: lista grande com re-renders frequentes do pai
const EmployeeRow = memo(function EmployeeRow({ employee, onDelete }: EmployeeRowProps) {
  return (...)
})

// Justificado: callback passado para componente memoizado
const handleDelete = useCallback((id: number) => {
  deleteEmployee.mutate(id)
}, [deleteEmployee])
```

**Nao use memo/useCallback** em:
- Componentes simples com props primitivas
- Callbacks que mudam junto com o estado que dependem
- Componentes que nao re-renderizam frequentemente

---

## PWA (Progressive Web App)

Projetos web CygnusForge devem ser instaláveis como PWA.

### manifest.json (em public/)

```json
{
  "name": "Nome do Sistema",
  "short_name": "Sistema",
  "description": "Descricao do sistema",
  "start_url": "/",
  "display": "standalone",
  "background_color": "#111827",
  "theme_color": "#184194",
  "icons": [
    { "src": "/logo.png", "sizes": "192x192", "type": "image/png" },
    { "src": "/logo.png", "sizes": "512x512", "type": "image/png" }
  ]
}
```

### Service Worker minimo (em public/sw.js)

```javascript
const CACHE = 'cygnus-v1'
const STATIC = ['/', '/index.html']

self.addEventListener('install', e => {
  e.waitUntil(caches.open(CACHE).then(c => c.addAll(STATIC)))
  self.skipWaiting()
})

self.addEventListener('fetch', e => {
  const { request } = e
  if (request.url.includes('/api/')) {
    // Network-first para API
    e.respondWith(fetch(request).catch(() => caches.match(request)))
  } else {
    // Cache-first para estaticos
    e.respondWith(caches.match(request).then(r => r || fetch(request)))
  }
})
```

### Registro no main.tsx

```typescript
if ('serviceWorker' in navigator) {
  window.addEventListener('load', () => {
    navigator.serviceWorker.register('/sw.js').catch(() => {})
  })
}
```

---

## Bundle — o que nao importar desnecessariamente

| Biblioteca | Problema | Alternativa |
|-----------|----------|-------------|
| `lodash` | Bundle enorme | Metodos nativos (`Array.from`, `Object.entries`) |
| `moment` | 230kb minificado | `date-fns` (tree-shakeable) |
| `chart.js` completo | Pesado | `recharts` com imports seletivos |
| Icone de conjunto inteiro | Gera bundle de todos os icones | `@tabler/icons-react` (tree-shakeable por import nomeado) |
