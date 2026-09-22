using System;

namespace CygnusFlow.Application.DTOs.Reports
{
    public class RelatorioFiltroDTO
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? ModuloId { get; set; }
        public int? CriticidadeId { get; set; }
        public int? StatusId { get; set; }
        public int? ResponsavelId { get; set; }
        public bool SomenteAtrasados { get; set; }
        public string TipoRelatorio { get; set; } = "Projetos"; // Projetos, Atividades, Gantt
    }
}
