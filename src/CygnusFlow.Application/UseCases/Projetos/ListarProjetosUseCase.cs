using CygnusFlow.Application.DTOs.Projeto;
using CygnusFlow.Application.DTOs.Specifications;
using CygnusFlow.Domain.Interfaces.Repositories;
using CygnusFlow.Domain.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace CygnusFlow.Application.UseCases.Projetos
{
    public class ListarProjetosUseCase
    {
        private readonly IProjetoRepository _projetoRepository;

        public ListarProjetosUseCase(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<ResultList<ProjetoListDto>> ExecuteAsync(ProjetoFiltroDto filtroDto)
        {
            var filtro = filtroDto.ToSpecification();

            var result = await _projetoRepository.GetByFiltrosAsync(filtro);
            if (!result.IsSuccess)
                return ResultList<ProjetoListDto>.Failure(result.Notifications);

            var dtos = result.Items.Select(ProjetoDtoMapper.ToListDto).ToList();
            return ResultList<ProjetoListDto>.Success(dtos, result.TotalCount);
        }
    }

}
