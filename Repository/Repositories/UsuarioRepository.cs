using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contexts;
using Repository.Repositories.BaseRepository;

namespace Repository.Repositories;

public class UsuarioRepository : BaseRepository<Usuario, Context>, IUsuarioRepository
{
    private readonly Context _context;

    public UsuarioRepository(Context context) : base(context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<Usuario?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}
