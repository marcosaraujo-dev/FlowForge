using CygnusFlow.Application.DTOs.Projeto;
using CygnusFlow.Domain.Interfaces.Repositories;
using CygnusFlow.Domain.Shared;
using System;
using System.Threading.Tasks;

namespace CygnusFlow.Application.UseCases.Projetos
{
    public class EditarProjetoUseCase
    {
        private readonly IProjetoRepository _projetoRepository;

        public EditarProjetoUseCase(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<Result<ProjetoResponseDto>> ExecuteAsync(UpdateProjetoDto dto)
        {
            // Buscar projeto existente
            var projetoExistenteResult = await _projetoRepository.GetByIdAsync(dto.Id);
            if (!projetoExistenteResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(projetoExistenteResult.Notifications);

            var projetoExistente = projetoExistenteResult.Data!;

            // Atualizar propriedades a partir do DTO
            projetoExistente.DefinirNome(dto.Nome);
            projetoExistente.Descricao = dto.Descricao;
            projetoExistente.ModuloId = dto.ModuloId;
            projetoExistente.CriticidadeId = (int)dto.CriticidadeId;
            projetoExistente.AlterarPeriodo(dto.DataInicioPO, dto.DataFimPO);
            projetoExistente.EstimativaHoras = dto.EstimativaHoras;
            projetoExistente.StatusProjetoId = (int)dto.StatusProjetoId;
            projetoExistente.DataUltimaAtualizacao = DateTime.Now;

            // Validar entidade
            var validationResult = projetoExistente.Validate();
            if (!validationResult.IsValid)
                return Result<ProjetoResponseDto>.Failure(validationResult);

            // Validar prazos
            var prazoValidationResult = projetoExistente.ValidarPrazos();
            if (!prazoValidationResult.IsValid)
                return Result<ProjetoResponseDto>.Failure(prazoValidationResult);

            // Atualizar no repositório
            var updateResult = await _projetoRepository.UpdateAsync(projetoExistente);
            if (!updateResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(updateResult.Notifications);

            // Buscar projeto completo para retorno
            var saveResult = await _projetoRepository.GetByIdAsync(projetoExistente.Id);
            if (!saveResult.IsSuccess)
                return Result<ProjetoResponseDto>.Failure(saveResult.Notifications);

            // Mapear para DTO de resposta
            var responseDto = ProjetoDtoMapper.ToResponseDto(saveResult.Data!);

            return Result<ProjetoResponseDto>.Success(responseDto);
        }
    }
}
