using CygnusFlow.Application.DTOs.Atividade;
using CygnusFlow.Application.DTOs.Dashboard;
using CygnusFlow.Application.DTOs.Projeto;
using CygnusFlow.Domain.Enums;
using CygnusFlow.Domain.Interfaces.Repositories;
using CygnusFlow.Domain.Shared;
using CygnusFlow.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CygnusFlow.Application.Services
{
    public class DashboardService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IAtividadeRepository _atividadeRepository;

        public DashboardService(IProjetoRepository projetoRepository, IAtividadeRepository atividadeRepository)
        {
            _projetoRepository = projetoRepository;
            _atividadeRepository = atividadeRepository;
        }

        public async Task<Result<DashboardDTO>> GetDashboardDataAsync()
        {
            var filtro = new ProjetoFiltro();
            var projetosResult = await _projetoRepository.GetByFiltrosAsync(filtro);

            if (!projetosResult.IsSuccess)
                return Result<DashboardDTO>.Failure(projetosResult.Notifications);

            var projetos = projetosResult.Items;

            var atividadesTotalResult = await _atividadeRepository.GetByFiltrosAsync(new AtividadeFiltro());

            var dashboard = new DashboardDTO
            {
                TotalProjetos = projetos.Count,
                ProjetosAtivos = projetos.Count(p => p.StatusProjetoId == (int)StatusProjeto.EmAndamento),
                ProjetosConcluidos = projetos.Count(p => p.StatusProjetoId == (int)StatusProjeto.Concluido),
                ProjetosAtrasados = projetos.Count(p => p.EstaAtrasado),
                TotalAtividades = atividadesTotalResult.IsSuccess ? atividadesTotalResult.TotalCount : 0,
                ProjetosRecentes = projetos
                    .OrderByDescending(p => p.DataCadastro)
                    .Take(5)
                    .Select(p => new ProjetoRecenteDTO
                    {
                        Id = p.Id,
                        Codigo = p.Codigo,
                        Nome = p.Nome,
                        StatusNome = p.StatusProjeto?.Nome ?? string.Empty,
                        StatusCor = null, // StatusProjeto (entidade) ainda não tem campo Cor
                        ResponsavelNome = p.Responsavel?.Nome,
                        DataFimPO = p.DataFimPO,
                        AtrasoEmDias = p.CalcularAtrasoEmDias()
                    }).ToList(),
                // Pendente: Atividade (entidade de domínio) só expõe ProjetoId/ResponsavelId
                // (sem navegação para Projeto/Usuario), então ProjetoNome/ResponsavelNome não
                // têm de onde vir ainda sem um join adicional no repositório. Ver DEC-002 —
                // fica para o redesenho de domínio (Fase 2) em vez de resolver aqui por suposição.
                AtividadesAtrasadas = new List<AtividadeAtrasadaDTO>(),
                IndicadoresCriticidade = projetos
                    .Where(p => p.Criticidade != null)
                    .GroupBy(p => new { p.Criticidade.Nome, p.Criticidade.Cor })
                    .Select(g => new IndicadorCriticidadeDTO
                    {
                        CriticidadeNome = g.Key.Nome,
                        Cor = g.Key.Cor,
                        Quantidade = g.Count(),
                        Percentual = projetos.Count > 0
                            ? Math.Round((double)g.Count() / projetos.Count * 100, 1)
                            : 0
                    }).ToList()
            };

            // Calcular porcentagem de entregas no prazo
            var projetosConcluidos = projetos.Where(p => p.StatusProjetoId == (int)StatusProjeto.Concluido).ToList();
            if (projetosConcluidos.Any())
            {
                dashboard.PercentualEntregasNoPrazo = Math.Round(
                    (double)projetosConcluidos.Count(p => !p.EstaAtrasado) / projetosConcluidos.Count * 100, 1);
            }

            return Result<DashboardDTO>.Success(dashboard);
        }
    }
}
