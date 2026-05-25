using Domain.Command.Request;
using Domain.Entities;

public interface IUsuarioService
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario?> GetUsuarioByIdAsync(int id);
    Task<Usuario> CreateUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(int id, UsuarioRequest request);
    Task DeleteUsuarioAsync(int id);
}