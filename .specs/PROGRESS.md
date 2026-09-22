# Progresso da Sessão

> Vive em `.specs/PROGRESS.md` (um único arquivo por projeto, não por feature).
> Regra: SOBRESCREVA as seções abaixo a cada atualização — nunca acumule histórico
> (isso vira log infinito e estoura o orçamento de contexto). Máx. ~30 linhas no total.
> Atualizado como passo final da skill `implementation-validation` ao concluir uma task.
>
> Complementar a `.specs/project/STATE.md` (decisões `AD-NNN`, blockers, handoff —
> mantido pela skill `tlc-spec-driven`): STATE.md é o "porquê" que atravessa features
> e sessões; este arquivo é o "onde estou agora" da orquestração corrente. Ao carregar
> contexto (`orchestration-multiagent`, Passo 0), leia os dois — e mantenha a mesma
> história em ambos (mesma feature/fase) ao encerrar a sessão.

## Feito na última sessão

- Revalidação do projeto: levantamento em CygnusFlow (código atual), ForgeFinance
  (referência de padrão) e cygnusforge/site (onde o produto será apresentado).
- Produto renomeado para **FlowForge** (DEC-001) e decidido realinhar backend com
  o padrão ForgeFinance — Dapper/DbUp/PostgreSQL/.NET 9, sem AutoMapper (DEC-002).
- Site institucional atualizado com o card + página do FlowForge (status "Em
  Desenvolvimento") em `cygnusforge/site` (repo separado).

## Estado atual

- Ainda sem feature formal em `.specs/features/` — este repo (CygnusFlow) segue
  com nome de código/namespaces antigos até o redesenho de domínio começar.
- Fase: pré-Specify (revalidação/plano macro, ver decisions.md DEC-001/DEC-002)

## Próximo passo

- Fechar o desenho de status separados Projeto x Atividade e abrir a primeira
  spec formal (`tlc-spec-driven`) para o redesenho de domínio: status,
  `Atividade.ProjetoId` nullable, datas reais automáticas por status, vínculo
  de time em Projeto, entidade de link externo compartilhável, convite/acesso.

## Bloqueios

- (nenhum | descrição do bloqueio + o que falta para destravar)
- Usado também como fallback do contador de tentativas do loop de qualidade
  quando o MCP orchestrator não está ativo (ver `max_quality_retries`).
