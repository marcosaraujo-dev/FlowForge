using CygnusFlow.Application.DTOs.Projeto;
using CygnusFlow.Domain.Entities;
using CygnusFlow.Domain.Interfaces.Repositories;
using CygnusFlow.Domain.Shared;
using CygnusFlow.Domain.ValueObjects;
using System.Threading.Tasks;

namespace CygnusFlow.Application.UseCases.Projetos
{
    public class CriarProjetoUseCase
    {
        private readonly IProjetoRepository _projetoRepository;

        public CriarProjetoUseCase(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<Result<ProjetoResponseDto>> ExecuteAsync(CreateProjetoDto dto)
        {
            // 1. Criar entidade a partir do DTO (código definitivo é gerado abaixo)
            var projeto = new Projeto(string.Empty, dto.Nome, dto.ModuloId, (int)dto.CriticidadeId,
                dto.DataInicioPO, dto.DataFimPO, dto.EstimativaHoras);

            // 2. Gerar próximo código
            var proximoNumeroResult = await _projetoRepository.GetProximoNumeroAsync();
            if (!proximoNumeroResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(proximoNumeroResult.Notifications);

            var codigoResult = CodigoProjeto.GerarProximoCodigo(proximoNumeroResult.Data);
            if (!codigoResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(codigoResult.Notifications);

            // 3.Verificar se código já existe
            var existeCodigoResult = await _projetoRepository.ExisteCodigoAsync(codigoResult.Data!.Value);
            if (!existeCodigoResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(existeCodigoResult.Notifications);

            if (existeCodigoResult.Data)
            {
                var notification = new NotificationResult();
                notification.AddError("Codigo", "Código já existe", "CODIGO_DUPLICADO");
                return Result<ProjetoResponseDto>.Failure(notification);
            }

            projeto.AlterarCodigo(codigoResult.Data!.Value);

            // 4. Validar entidade
            var validationResult = projeto.Validate();
            if (!validationResult.IsValid)
                return Result<ProjetoResponseDto>.Failure(validationResult);
            // Validar prazos
            var prazoValidationResult = projeto.ValidarPrazos();
            if (!prazoValidationResult.IsValid)
                return Result<ProjetoResponseDto>.Failure(prazoValidationResult);

            // 5. Salvar no repositório
            var saveResult = await _projetoRepository.CreateAsync(projeto);
            if (!saveResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(saveResult.Notifications);

            // 6. Retornar DTO de resposta
            var responseDto = ProjetoDtoMapper.ToResponseDto(saveResult.Data!);
            return Result<ProjetoResponseDto>.Success(responseDto);
        }
    }
}
