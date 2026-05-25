using Domain.Command.Request;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.NotificationService;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly INotifications _notifications;

    public UsuarioService(IUsuarioRepository repository, INotifications notifications)
    {
        _repository = repository;
        _notifications = notifications;
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync() => await _repository.GetAllAsync();

    public async Task<Usuario?> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) _notifications.AddError($"Usuário com ID {id} não encontrado.");
        return usuario;
    }

    public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        => await _repository.AddAsync(usuario);

    public async Task UpdateUsuarioAsync(int id, UsuarioRequest request)
    {
        var usuarioExiste = await _repository.GetByIdAsync(id);
        if (usuarioExiste == null)
        {
            _notifications.AddError($"Usuário com ID {id} não encontrado.");
            return;
        }

        usuarioExiste.Nome = request.Nome;
        usuarioExiste.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.Senha))
        {
            var hasher = new PasswordHasher<Usuario>();
            usuarioExiste.Senha = hasher.HashPassword(usuarioExiste, request.Senha);
        }

        await _repository.UpdateAsync(usuarioExiste);
    }

    public async Task DeleteUsuarioAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null)
        {
            _notifications.AddError($"Usuário com ID {id} não encontrado.");
            return;
        }
        await _repository.RemoveAsync(usuario);
    }
}