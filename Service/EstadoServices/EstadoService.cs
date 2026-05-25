using Domain.Command.Request;
using Domain.Contracts;
using Domain.Entities;
using Service.NotificationService;

public class EstadoService : IEstadoService
{
    private readonly IEstadoRepository _estadoRepository;
    private readonly INotifications _notifications;

    public EstadoService(IEstadoRepository estadoRepository, INotifications notifications)
    {
        _estadoRepository = estadoRepository;
        _notifications = notifications;
    }

    public async Task<IEnumerable<Estado>> GetAllEstadosAsync() => await _estadoRepository.GetAllAsync();

    public async Task<Estado?> GetEstadoByIdAsync(int id)
    {
        var estado = await _estadoRepository.GetByIdAsync(id);
        if (estado == null) _notifications.AddError($"Estado com ID {id} não encontrado.");
        return estado;
    }

    public async Task<Estado> CreateEstadoAsync(EstadoRequest request)
    {
        var estado = new Estado { Sigla = request.Sigla, Nome = request.Nome };
        return await _estadoRepository.AddAsync(estado);
    }

    public async Task UpdateEstadoAsync(int id, EstadoRequest request)
    {
        var estadoExiste = await _estadoRepository.GetByIdAsync(id);
        if (estadoExiste == null)
        {
            _notifications.AddError($"Estado com ID {id} não encontrado.");
            return;
        }

        estadoExiste.Sigla = request.Sigla;
        estadoExiste.Nome = request.Nome;
        await _estadoRepository.UpdateAsync(estadoExiste);
    }

    public async Task DeleteEstadoAsync(int id)
    {
        var estado = await _estadoRepository.GetByIdAsync(id);
        if (estado == null)
        {
            _notifications.AddError($"Estado com ID {id} não encontrado.");
            return;
        }
        await _estadoRepository.RemoveAsync(estado);
    }
}