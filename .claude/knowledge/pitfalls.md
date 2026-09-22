# Pitfalls — Erros a Não Repetir

> Ponto focal consultado **antes de implementar**. Fonte da verdade, versionada. Política em `.claude/steering/knowledge-base.md`. Gravado pela skill `capture-learning`, curado por `knowledge-curation`.

Cada entrada segue o formato:

```
### PIT-001 — Título curto do erro
- **Área**: (módulo/camada/tecnologia — usado para filtrar na consulta)
- **Sintoma**: o que se observa quando o erro acontece
- **Causa-raiz**: por que acontece
- **Jeito certo**: o que fazer em vez disso
- **Gatilho de detecção**: como perceber que estou prestes a repetir
- **Origem**: correção do usuário / rejeição de gate / gate vermelho — data
- **Reincidências**: 0   <!-- incrementado por capture-learning quando o erro se repete; 2+ vira ponto quente no tech-debt-tracker -->

```

<!-- Entradas abaixo. Mantenha ordenado por área quando possível. -->

### PIT-001 — Namespace com o mesmo nome do tipo de domínio causa CS0118
- **Área**: CygnusFlow.Application / DTOs
- **Sintoma**: arquivos em `DTOs/Projeto/*.cs` (namespace `CygnusFlow.Application.DTOs.Projeto`) que usam o tipo `CygnusFlow.Domain.Entities.Projeto` sem qualificar totalmente falham com `CS0118: "Projeto" é um namespace, mas é usado como um tipo` — o compilador resolve `Projeto` para o namespace, não para a classe. `DTOs/Atividade/*.cs` tem a mesma colisão com `Domain.Entities.Atividade`.
- **Causa-raiz**: a pasta/namespace de DTOs usa o nome singular da entidade (`DTOs.Projeto`, `DTOs.Atividade`), igual ao nome da classe de domínio.
- **Jeito certo**: dentro dessas pastas, referenciar a entidade de domínio pelo nome totalmente qualificado (`CygnusFlow.Domain.Entities.Projeto`) em vez de `using` + nome curto. Quando o redesenho de domínio (Fase 2) mexer nessas pastas, considerar renomear o namespace de DTOs para plural (`DTOs.Projetos`, `DTOs.Atividades`) e eliminar a colisão de raiz.
- **Gatilho de detecção**: ao criar um mapper/serviço dentro de `DTOs/Projeto` ou `DTOs/Atividade` que precisa do tipo de domínio homônimo.
- **Origem**: rejeição de gate (Stop hook / dotnet build) — 2026-09-22
- **Reincidências**: 0

### PIT-002 — DTOs renomeados sem atualizar todos os consumidores
- **Área**: CygnusFlow.Application / Dashboard e Relatórios
- **Sintoma**: build falhava com `CS0246` para `DashboardDto` e `AtividadeResponseDto` — os DTOs reais já tinham sido renomeados para `DashboardDTO`/`AtividadeResponseDTO` (sufixo maiúsculo) e o Dashboard reestruturado (novos DTOs `ProjetoRecenteDTO`/`AtividadeAtrasadaDTO`/`IndicadorCriticidadeDTO`), mas `DashboardService.cs` e `RelatorioAtividadesDto.cs` ainda referenciavam os nomes antigos.
- **Causa-raiz**: refatoração de nomenclatura de DTOs feita e deixada sem commit antes desta sessão, incompleta — só os arquivos de DTO foram atualizados, não os consumidores.
- **Jeito certo**: ao renomear um DTO, `grep` por todos os consumidores antes de considerar a task concluída — não confiar em o arquivo do próprio DTO compilar isoladamente.
- **Gatilho de detecção**: qualquer rename de classe DTO — rodar build da solution inteira antes de encerrar, não só do projeto tocado.
- **Origem**: rejeição de gate (Stop hook / dotnet build) — 2026-09-22. Pendência real deixada em aberto: `DashboardService.AtividadesAtrasadas` retorna lista vazia porque `Atividade` (entidade de domínio) não expõe navegação para `Projeto`/`Usuario` (só `ProjetoId`/`ResponsavelId`) — decidir a solução (join no repositório vs. navegação nova) fica para o redesenho de domínio (ver DEC-002 em decisions.md), não foi resolvido por suposição.
- **Reincidências**: 0

### PIT-003 — Build local (Debug) não reflete o gate real (Release /warnaserror)
- **Área**: CygnusFlow — build/CI, toda a solution
- **Sintoma**: `dotnet build CygnusFlow.sln` (Debug, configuração padrão) passa com só warnings (`CS8602`/`CS8603`/`CS8604`/`CS8629`, nullable reference types), mas o Stop hook roda `--configuration Release`, onde os mesmos warnings viram **erro** e bloqueiam — 30 erros de uma vez na primeira vez que isso foi descoberto.
- **Causa-raiz**: os `.csproj` não têm `TreatWarningsAsErrors` fixo — o comportamento vem de configuração condicional (Release trata como erro, Debug não), então rodar só `dotnet build` sem especificar `--configuration Release` dá falso-positivo de "build ok".
- **Jeito certo**: ao validar build localmente antes de encerrar uma task (ou antes de confiar em "buildou"), rodar `dotnet build CygnusFlow.sln --configuration Release` — é o que o hook realmente executa. Não confiar no build Debug como proxy do gate.
- **Gatilho de detecção**: qualquer alteração em código C# nesta solution, antes de reportar "build verde" ao usuário.
- **Origem**: rejeição de gate (Stop hook) — 2026-09-22, descoberto depois de já ter reportado build verde com base só no Debug.
- **Reincidências**: 0

### PIT-004 — Nullable warning em Mapper Infrastructure: usar `null!`, não mudar o retorno para `T?`
- **Área**: CygnusFlow.Infrastructure / Mappers
- **Sintoma**: os ~12 Mappers (`ProjetoMapper`, `AtividadeMapper`, `UsuarioMapper` etc.) têm o padrão `if (model == null) return null;` num método com retorno não-anulável — gera `CS8603` (erro em Release). A correção óbvia (`T? ToDomain(T? model)`) parece mais "correta" no papel, mas **espalha** `CS8604`/`CS8620` para todos os call sites (repositórios que fazem `.Add(entity)`, `Result<T>.Success(data)`, `.Select(Mapper.ToDomain).ToList()` passado pra `IEnumerable<T>` não-anulável) — 1 fix gerou dezenas de novos warnings em cascata.
- **Jeito certo**: manter a assinatura não-anulável e trocar `return null;` por `return null!;` (null-forgiving) só na guarda — comportamento em runtime idêntico, zero ripple. Só mudar a assinatura para `T?` de propósito, como parte de um redesenho deliberado desses Mappers (não como side-effect de silenciar um warning).
- **Gatilho de detecção**: antes de "corrigir" um warning de nulidade mudando um tipo de retorno público/estático usado em vários lugares — rodar build completo depois pra ver o raio de propagação antes de aceitar a mudança.
- **Origem**: rejeição de gate (Stop hook) — 2026-09-22, corrigido e revertido na mesma sessão.
- **Reincidências**: 0
