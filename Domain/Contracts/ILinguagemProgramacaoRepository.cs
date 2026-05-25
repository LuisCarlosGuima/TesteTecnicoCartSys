using Domain.Contracts.BaseRepository;
using Domain.Entities;

namespace Domain.Contracts;

public interface ILinguagemProgramacaoRepository : IBaseRepository<LinguagemProgramacao>
{
    Task<List<LinguagemProgramacao>> GetByManyIdsAsync(IEnumerable<int> ids);
}
