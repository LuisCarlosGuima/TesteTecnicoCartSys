using Domain.Contracts.BaseRepository;
using Domain.Entities;

namespace Domain.Contracts;

public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByRefreshTokenAsync(string refreshToken);
}
