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
