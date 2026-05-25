using Domain.Contracts.BaseRepository;
using Domain.Entities;

namespace Domain.Contracts;

public interface IDesenvolvedorRepository : IBaseRepository<Desenvolvedor>
{
    Task<Desenvolvedor?> GetByIdWithLinguagensAsync(int id);
    Task<IEnumerable<Desenvolvedor>> GetAllWithRelationshipsAsync();
}
