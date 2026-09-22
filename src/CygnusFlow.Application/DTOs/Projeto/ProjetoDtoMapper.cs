namespace CygnusFlow.Application.DTOs.Projeto
{
    public static class ProjetoDtoMapper
    {
        // Qualificado por extenso: o namespace deste arquivo (DTOs.Projeto) tem o mesmo
        // nome do tipo de domínio, então "Projeto" desqualificado resolveria para o
        // namespace, não para a entidade (CS0118).
        public static ProjetoResponseDto ToResponseDto(CygnusFlow.Domain.Entities.Projeto projeto)
        {
            return new ProjetoResponseDto
            {
                Id = projeto.Id,
                Codigo = projeto.Codigo,
                Nome = projeto.Nome,
                ModuloNome = projeto.Modulo?.Nome ?? string.Empty,
                CriticidadeNome = projeto.Criticidade?.Nome ?? string.Empty,
                StatusNome = projeto.StatusProjeto?.Nome ?? string.Empty,
                DataInicioPO = projeto.DataInicioPO,
                DataFimPO = projeto.DataFimPO,
                EstimativaHoras = projeto.EstimativaHoras,
                EstaAtrasado = projeto.EstaAtrasado,
                DiasAtraso = projeto.CalcularAtrasoEmDias(),
                DataCadastro = projeto.DataCadastro
            };
        }

        public static ProjetoListDto ToListDto(CygnusFlow.Domain.Entities.Projeto projeto)
        {
            return new ProjetoListDto
            {
                Id = projeto.Id,
                Codigo = projeto.Codigo,
                Nome = projeto.Nome,
                ModuloNome = projeto.Modulo?.Nome ?? string.Empty,
                CriticidadeNome = projeto.Criticidade?.Nome ?? string.Empty,
                StatusNome = projeto.StatusProjeto?.Nome ?? string.Empty,
                DataFimPO = projeto.DataFimPO,
                EstaAtrasado = projeto.EstaAtrasado
            };
        }
    }
}
