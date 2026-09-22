# Padroes Obrigatorios React

Checklist para todo Pull Request em projetos React CygnusForge.

---

## Mantine-first

### Nunca use inline styles com valores crus

```tsx
// ❌ PROIBIDO — hex cru, fora do theme.ts
<div style={{ backgroundColor: '#184194', padding: '20px' }}>

// ✅ CORRETO — componente Mantine, cor do theme.ts
<Box bg="brand.6" p="lg">
```

### Nunca use as cores padrao do Mantine — nem hardcode hexadecimais

O Mantine ja vem com uma paleta propria (`blue`, `red`, `teal`, `grape`...). Ela **nao existe** para efeitos deste projeto — so os 6 nomes definidos em `theme.ts` (`brand`, `success`, `danger`, `warning`, `info`, `gray`) sao validos. Ver [00-Stack.md](00-Stack.md#cores-do-design-system-sao-a-fonte-da-verdade--nunca-o-default-do-mantine).

```tsx
// ❌ PROIBIDO — cor padrao do Mantine, nunca passou pelo Design System
<Button color="blue">Salvar</Button>

// ❌ PROIBIDO — hex cru, inventa cor fora da paleta
<Box bg="#184194">

// ✅ CORRETO — nome definido em theme.ts
<Button color="brand">Salvar</Button>
```

### Use cn() para className condicional (composicao com CSS Modules)

Componentes Mantine resolvem a maior parte da estilizacao via props (`color`, `variant`, `bg`, `c`, `p`...), sem precisar de classes utilitarias. Quando um componente customizado ainda precisa de `className` condicional (ex.: combinando um CSS Module com um estado), use `clsx` — sem `tailwind-merge`, que so faz sentido com utilitarios Tailwind.

```typescript
// lib/utils.ts
import { type ClassValue, clsx } from 'clsx'

export function cn(...inputs: ClassValue[]) {
  return clsx(inputs)
}
```

```tsx
// ✅ CORRETO — props do Mantine resolvem estado, sem precisar de className
<Paper withBorder p="md" style={isHighlighted ? { borderColor: 'var(--mantine-color-brand-3)' } : undefined}>

// ✅ CORRETO — cn() apenas quando ha CSS Module customizado envolvido
<div className={cn(styles.card, isHighlighted && styles.cardHighlighted)}>

// ❌ RUIM — template literal com logica
<div className={`${styles.card} ${isHighlighted ? styles.cardHighlighted : ''}`}>
```

---

## Componentes Mantine por padrao

Sempre o componente Mantine correspondente, nunca um `<div>`/`<button>`/`<input>` cru estilizado na mao — o Mantine ja cobre foco, disabled, erro e acessibilidade.

### Cards

```tsx
<Paper withBorder radius="md" p="lg">
```

### Botao primario

```tsx
<Button color="brand">Salvar</Button>
```

### Botao secundario (outline)

```tsx
<Button variant="default">Cancelar</Button>
```

### Botao destrutivo

```tsx
<Button color="danger">Excluir</Button>
```

### Input de texto

```tsx
<TextInput label="Nome" placeholder="Digite o nome" />
```

### Input com erro

```tsx
<TextInput label="Email" error="Email invalido" />
```

O `error` do Mantine ja aplica a borda/cor de erro automaticamente — nao redefina manualmente.

### Label e mensagem de erro de campo

Nao criar `<label>`/`<span>` separados — usar as props `label` e `error` do proprio input (`TextInput`, `Select`, `Textarea`...). O Mantine ja associa `label` ao `htmlFor` e `error` ao `aria-describedby`.

### Badge (generico)

```tsx
<Badge color="success">Ativo</Badge>
<Badge color="danger">Inativo</Badge>
```

Cor semantica conforme o status (ver tabela em [00-Stack.md](00-Stack.md#cores-do-design-system-sao-a-fonte-da-verdade--nunca-o-default-do-mantine)).

### Alerta de erro (formulario)

```tsx
<Alert color="danger" variant="light" title="Erro ao salvar">
  Verifique os campos destacados.
</Alert>
```

### Tabela

```tsx
<Table highlightOnHover>
  <Table.Thead>
    <Table.Tr>
      <Table.Th>Nome</Table.Th>
    </Table.Tr>
  </Table.Thead>
  <Table.Tbody>{/* Table.Tr / Table.Td */}</Table.Tbody>
</Table>
```

`highlightOnHover` ja cobre o hover de linha — nao redefina com `style`/`className`.

### Item de navegacao

```tsx
<NavLink label="Colaboradores" active={isActive} color="brand" variant="filled" />
```

Ativo/inativo/hover ja vem do `theme.ts` via `color` + `variant` — ver [Variantes de sidebar](00-Stack.md#variantes-de-sidebar).

---

## TypeScript

- `strict: true` obrigatorio no tsconfig
- Nunca use `any` — use `unknown` se necessario e valide
- Toda interface de props tem nome explicito (`XxxProps`, nunca inline)
- Prefira `type` para uniao de tipos, `interface` para objetos com possibilidade de extensao
- Enums de dominio (status, papeis) ficam em `types/index.ts`

```typescript
// ✅ BOM
interface StatCardProps {
  label: string
  value: number | string
  icon: React.ComponentType<{ size?: number | string; color?: string; stroke?: number }>  // tipo dos icones do @tabler/icons-react
  color?: 'brand' | 'success' | 'warning' | 'danger'
}

// ❌ RUIM — sem interface
function StatCard({ label, value, icon, color = 'brand' }: { label: string; value: number | string; icon: any; color?: string }) {
```

---

## Exports

- Sempre named export (nunca `export default`)
- Um componente por arquivo
- O nome do export deve ser igual ao nome do arquivo

```typescript
// ✅ BOM — EmployeeModal.tsx
export function EmployeeModal() { ... }

// ❌ RUIM — export default
export default function EmployeeModal() { ... }
```

---

## Acessibilidade minima

- Botoes de icone tem `title` ou `aria-label`
- Inputs tem `<label>` associado com `htmlFor`
- Imagens decorativas tem `alt=""`
- Imagens informativas tem `alt` descritivo
- Dialogs usam `Modal`/`Drawer` do Mantine (foco, Escape, aria-modal automaticos)

---

## Checklist de PR

Antes de abrir PR, verifique:

- [ ] Zero `eslint` warnings (`npm run lint`)
- [ ] Zero erros TypeScript (`npm run type-check` ou `tsc --noEmit`)
- [ ] Nenhum `style={}` inline com valor cru (hex, px solto fora de spacing token)
- [ ] Nenhum hexadecimal hardcoded em `style`/`bg`/`c`
- [ ] Nenhuma cor padrao do Mantine (`color="blue"`, `"red"`, `"teal"`...) — so `brand`/`success`/`danger`/`warning`/`info`/`gray` do `theme.ts`
- [ ] `cn()` (clsx) apenas onde ha CSS Module condicional — nao inventar classes utilitarias
- [ ] Props tipadas com interface nomeada
- [ ] Nenhum `any`
- [ ] Dados da API via `useQuery` (nao `useEffect`)
- [ ] Formularios com `@mantine/form` + Zod
- [ ] Named exports em todos os componentes
- [ ] Icones vem de `@tabler/icons-react` (nenhum outro pacote de icones)
- [ ] Botoes de icone com `title` ou `aria-label`
