using System;

namespace CygnusFlow.Application.DTOs.Atividade
{
    public class AtividadeAtrasadaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string ProjetoNome { get; set; } = string.Empty;
        public string ResponsavelNome { get; set; } = string.Empty;
        public DateTime DataFimEstimada { get; set; }
        public int AtrasoEmDias { get; set; }
    }
}
