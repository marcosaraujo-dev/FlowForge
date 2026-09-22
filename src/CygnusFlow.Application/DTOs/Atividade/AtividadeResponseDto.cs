using System;

namespace CygnusFlow.Application.DTOs.Atividade
{

    public class AtividadeResponseDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int ProjetoId { get; set; }
        public string ProjetoNome { get; set; } = string.Empty;
        public string ProjetoCodigo { get; set; } = string.Empty;
        public int ResponsavelId { get; set; }
        public string ResponsavelNome { get; set; } = string.Empty;
        public int TipoAtividadeId { get; set; }
        public string TipoAtividadeNome { get; set; } = string.Empty;
        public string? TipoAtividadeCor { get; set; }
        public DateTime DataInicioEstimada { get; set; }
        public DateTime DataFimEstimada { get; set; }
        public DateTime? DataInicioPlanejada { get; set; }
        public DateTime? DataFimPlanejada { get; set; }
        public DateTime? DataInicioReal { get; set; }
        public DateTime? DataFimReal { get; set; }
        public int StatusAtividadeId { get; set; }
        public string StatusAtividadeNome { get; set; } = string.Empty;
        public string? StatusAtividadeCor { get; set; }
        public int EstimativaHoras { get; set; }
        public int? HorasTrabalhadasReal { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string? Observacoes { get; set; }
        public string? Impedimentos { get; set; }
        public int AtrasoEmDias { get; set; }
    }

}
