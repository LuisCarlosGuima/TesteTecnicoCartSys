using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories;

public class DesenvolvedorRepository : BaseRepository<Desenvolvedor, Context>, IDesenvolvedorRepository
{
    public DesenvolvedorRepository(Context context) : base(context)
    {
    }

    public async Task<Desenvolvedor?> GetByIdWithLinguagensAsync(int id)
    {
        return await _context.Desenvolvedores
            .Include(d => d.Linguagens)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Desenvolvedor>> GetAllWithRelationshipsAsync()
    {
        return await _context.Desenvolvedores.AsNoTracking()
                                             .Include(d => d.Linguagens)
                                             .ToListAsync();
    }
}
