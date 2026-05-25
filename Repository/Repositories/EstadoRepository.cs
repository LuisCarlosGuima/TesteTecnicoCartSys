using Domain.Contracts;
using Domain.Entities;
using Repository.Contexts;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories;

public class EstadoRepository : BaseRepository<Estado, Context>, IEstadoRepository
{
    public EstadoRepository(Context context) : base(context)
    {

    }
}
