# Conventions — Convenções do Projeto

> Convenções específicas deste projeto **não óbvias no código**. Consultado antes de assumir um default. Fonte da verdade, versionada. Política em `.claude/steering/knowledge-base.md`.

Não repita aqui o que já está no `CLAUDE.md`, nos steerings ou é evidente lendo o código. Registre só o não-óbvio (o "por quê" que se perde).

Cada entrada segue o formato:

```
### CONV-001 — Título da convenção
- **Área**: (módulo/camada/tecnologia)
- **Regra**: o que fazer
- **Por quê**: a razão não óbvia (o que quebra se ignorar)
- **Exemplo**: trecho ou referência
```

<!-- Entradas abaixo. -->

### CONV-001 — Mantine + Tabler Icons como padrao React (substitui Tailwind + Radix + Lucide)
- **Área**: React/TypeScript (todo o guia em `React/`)
- **Regra**: Todo projeto React novo usa **Mantine** (`@mantine/core`) como UI kit/estilizacao e **`@tabler/icons-react`** para icones. Tailwind CSS, Radix UI e Lucide React saem do padrao obrigatorio (continuam proibidos misturar com Mantine no mesmo projeto, para nao duplicar reset CSS). As cores da marca (indigo/success/danger/warning/info/gray) sao definidas uma unica vez em `src/theme.ts`, na tupla de 10 tons que o Mantine exige (`MantineColorsTuple`) — nunca as cores padrao que o Mantine ja vem com (`blue`, `red`, `teal`...), que nao tem relacao com o Design System.
- **Por quê**: Decisao do usuario (2026-09-15). O ponto critico e que o Mantine tem sua propria paleta pronta e, se um dev usar `color="blue"`/`color="red"` sem passar pelo `theme.ts`, a tela sai da paleta oficial da marca sem que isso apareca como hex cru no code review (o nome parece generico). Por isso o padrao exige que TODO uso de cor em componente Mantine use um dos 6 nomes semanticos do tema (`brand`, `success`, `danger`, `warning`, `info`, `gray`).
- **Como aplicar**: Ao gerar/revisar codigo React, checar `React/00-Stack.md` (stack, `theme.ts`, mapeamento token→cor Mantine), `React/05-PadroesObrigatorios.md` (checklist "Mantine-first") e `React/06-AntiPatterns.md` (anti-pattern de cor padrao do Mantine). Rejeitar em review qualquer `color="blue"`/`color="red"` etc. (nomes nativos do Mantine) e qualquer hex cru fora de `theme.ts`.
