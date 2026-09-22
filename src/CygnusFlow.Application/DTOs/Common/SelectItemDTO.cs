namespace CygnusFlow.Application.DTOs.Common
{
    public class SelectItemDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public bool Ativo { get; set; } = true;
    }

}
