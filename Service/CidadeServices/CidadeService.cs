using Domain.Command.Request;
using Domain.Contracts;
using Domain.Entities;
using Service.DesenvolvedorServices;
using Service.NotificationService;

public class CidadeService : ICidadeService
{
    private readonly ICidadeRepository _cidadeRepository;
    private readonly INotifications _notifications;

    public CidadeService(ICidadeRepository cidadeRepository, INotifications notifications)
    {
        _cidadeRepository = cidadeRepository;
        _notifications = notifications;
    }

    public async Task<IEnumerable<Cidade>> GetAllCidadesAsync() => await _cidadeRepository.GetAllAsync();

    public async Task<Cidade> GetCidadeByIdAsync(int id)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null) _notifications.AddError($"Cidade com ID {id} não encontrada.");
        return cidade;
    }

    public async Task<Cidade> CreateCidadeAsync(CidadeRequest request)
    {
        var cidade = new Cidade { Nome = request.Nome, EstadoId = request.EstadoId };
        return await _cidadeRepository.AddAsync(cidade);
    }

    public async Task UpdateCidadeAsync(int id, CidadeRequest request)
    {
        var cidadeExiste = await _cidadeRepository.GetByIdAsync(id);
        if (cidadeExiste == null)
        {
            _notifications.AddError($"Cidade com ID {id} não encontrada.");
            return;
        }

        cidadeExiste.Nome = request.Nome;
        cidadeExiste.EstadoId = request.EstadoId;
        await _cidadeRepository.UpdateAsync(cidadeExiste);
    }

    public async Task DeleteCidadeAsync(int id)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null)
        {
            _notifications.AddError($"Cidade com ID {id} não encontrada.");
            return;
        }
        await _cidadeRepository.RemoveAsync(cidade);
    }
}