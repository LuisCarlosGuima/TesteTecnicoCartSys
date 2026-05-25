using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories;

public class LinguagemProgramacaoRepository : BaseRepository<LinguagemProgramacao, Context>, ILinguagemProgramacaoRepository
{
    public LinguagemProgramacaoRepository(Context context) : base(context)
    {

    }
    public async Task<List<LinguagemProgramacao>> GetByManyIdsAsync(IEnumerable<int> ids)
    {
        return await _context.Linguagens.Where(l => ids.Contains(l.Id)).ToListAsync();
    }
}
