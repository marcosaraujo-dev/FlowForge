using CygnusFlow.Domain.Constants;
using CygnusFlow.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace CygnusFlow.Application.DTOs.Atividade
{
    public class CreateAtividadeDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 255 caracteres")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Descrição deve ter no máximo 1000 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Projeto é obrigatório")]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "Responsável é obrigatório")]
        public int ResponsavelId { get; set; }

        [Required(ErrorMessage = "Tipo de atividade é obrigatório")]
        public int TipoAtividadeId { get; set; }

        [Required(ErrorMessage = "Data início estimada é obrigatória")]
        public DateTime DataInicioEstimada { get; set; }

        [Required(ErrorMessage = "Data fim estimada é obrigatória")]
        public DateTime DataFimEstimada { get; set; }

        public DateTime? DataInicioPlanejada { get; set; }
        public DateTime? DataFimPlanejada { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        public int StatusAtividadeId { get; set; }

        [Required(ErrorMessage = "Estimativa de horas é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Estimativa deve ser maior que zero")]
        public int EstimativaHoras { get; set; }

        [StringLength(2000, ErrorMessage = "Observações deve ter no máximo 2000 caracteres")]
        public string? Observacoes { get; set; }

        [StringLength(2000, ErrorMessage = "Impedimentos deve ter no máximo 2000 caracteres")]
        public string? Impedimentos { get; set; }
    }

}
