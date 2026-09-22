using System;

namespace CygnusFlow.Application.DTOs.Projeto
{
    public class ProjetoRecenteDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string StatusNome { get; set; } = string.Empty;
        public string? StatusCor { get; set; }
        public string? ResponsavelNome { get; set; }
        public DateTime DataFimPO { get; set; }
        public int AtrasoEmDias { get; set; }
    }
}
