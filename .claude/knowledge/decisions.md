# Decisions — Decisões Técnicas (ADR-lite)

> Consultado **antes de perguntar ao usuário** (a decisão pode já estar aqui) e antes de rever uma escolha arquitetural. Fonte da verdade, versionada. Política em `.claude/steering/knowledge-base.md`.

Cada entrada segue o formato:

```
### DEC-001 — Título da decisão
- **Contexto**: o problema/força que motivou a decisão
- **Decisão**: o que foi decidido
- **Alternativas descartadas**: e por quê
- **Consequências**: trade-offs aceitos
- **Data / Status**: YYYY-MM-DD — vigente / superada por DEC-NNN
```

<!-- Entradas abaixo. -->

### DEC-001 — Produto renomeado de CygnusFlow para FlowForge
- **Contexto**: revalidação do projeto (2026-09-22) para lançá-lo no site institucional cygnusforge com status "Em Desenvolvimento". Nome precisava seguir a convenção parcial de marca do site (2 de 5 produtos usam sufixo/prefixo "Forge": ForgeFinance, ReportForge).
- **Decisão**: nome público do produto passa a ser **FlowForge**. Site institucional já atualizado (`cygnusforge/site/index.html`, `cygnusforge/site/flow-forge/index.html`, `cygnusforge/site/css/catalog.css`) com status `Em Desenvolvimento`.
- **Alternativas descartadas**: manter "CygnusFlow" (soava redundante com a marca-mãe "Cygnus Forge"); "TaskForge" (menos alinhado à identidade de fluxo/gestão já usada no README).
- **Consequências**: o código deste repositório (namespaces `CygnusFlow.*`, nome da solution, pasta, imagens, README) **ainda não foi renomeado** — pendente para quando o redesenho de domínio (Fase 2 do plano) começar. Até lá, código e site usam nomes diferentes intencionalmente.
- **Data / Status**: 2026-09-22 — vigente

### DEC-002 — Realinhar stack backend com o padrão ForgeFinance/harness
- **Contexto**: `CygnusFlow.Infrastructure.csproj` e `CygnusFlow.Application.csproj` usam **AutoMapper** (proibido pela política do harness, `~/.claude/steering/coding-standards.md`) e **EF Core 8 + SQL Server**; README cita **FluentAssertions** (também proibido) nos testes. O produto-irmão ForgeFinance já resolveu essa mesma decisão com **.NET 9 + Dapper + DbUp (migrations SQL) + PostgreSQL + FluentValidation + Shouldly**, sem AutoMapper nem MediatR.
- **Decisão**: CygnusFlow/FlowForge vai realinhar para o mesmo padrão do ForgeFinance — remover AutoMapper (mapeamento manual/estático), remover EF Core em favor de Dapper + DbUp, trocar SQL Server por PostgreSQL, subir para .NET 9, e usar Shouldly em vez de FluentAssertions nos testes.
- **Alternativas descartadas**: manter EF Core e só trocar as libs proibidas pontualmente (menor esforço, mas mantém divergência de padrão entre os dois produtos do ecossistema).
- **Consequências**: reescrita de `CygnusFlow.Infrastructure` (repositórios, contexto/migrations) e ajustes em `CygnusFlow.Application` (remover `IMapper` de `CriarProjetoUseCase` e demais UseCases). Efeito grande — tratar como parte da Fase 4 (Backend/infra) do plano de revalidação, não uma mudança pontual.
- **Data / Status**: 2026-09-22 — vigente
