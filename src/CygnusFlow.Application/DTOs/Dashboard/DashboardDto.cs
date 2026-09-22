using CygnusFlow.Application.DTOs.Atividade;
using CygnusFlow.Application.DTOs.Projeto;
using System.Collections.Generic;

namespace CygnusFlow.Application.DTOs.Dashboard
{
    public class DashboardDTO
    {
        public int TotalProjetos { get; set; }
        public int ProjetosAtivos { get; set; }
        public int ProjetosConcluidos { get; set; }
        public int ProjetosAtrasados { get; set; }
        public int TotalAtividades { get; set; }
        public double PercentualEntregasNoPrazo { get; set; }
        public List<ProjetoRecenteDTO> ProjetosRecentes { get; set; } = new();
        public List<AtividadeAtrasadaDTO> AtividadesAtrasadas { get; set; } = new();
        public List<IndicadorCriticidadeDTO> IndicadoresCriticidade { get; set; } = new();
    }
}
