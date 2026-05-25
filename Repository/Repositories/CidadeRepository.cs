using Domain.Contracts;
using Domain.Entities;
using Repository.Contexts;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories;

public class CidadeRepository : BaseRepository<Cidade, Context>, ICidadeRepository
{
    public CidadeRepository(Context context) : base(context)
    {

    }
}
