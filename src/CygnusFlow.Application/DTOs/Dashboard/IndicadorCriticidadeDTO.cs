namespace CygnusFlow.Application.DTOs.Dashboard
{
    public class IndicadorCriticidadeDTO
    {
        public string CriticidadeNome { get; set; } = string.Empty;
        public string? Cor { get; set; }
        public int Quantidade { get; set; }
        public double Percentual { get; set; }
    }
}
