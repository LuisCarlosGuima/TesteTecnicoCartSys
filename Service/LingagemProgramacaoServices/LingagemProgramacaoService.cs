using Domain.Command.Request;
using Domain.Contracts;
using Domain.Entities;
using Service.NotificationService;

public class LinguagemProgramacaoService : ILinguagemProgramacaoService
{
    private readonly ILinguagemProgramacaoRepository _repository;
    private readonly INotifications _notifications;

    public LinguagemProgramacaoService(ILinguagemProgramacaoRepository repository, INotifications notifications)
    {
        _repository = repository;
        _notifications = notifications;
    }

    public async Task<IEnumerable<LinguagemProgramacao>> GetAllLinguagensAsync() => await _repository.GetAllAsync();

    public async Task<LinguagemProgramacao> GetLinguagemByIdAsync(int id)
    {
        var linguagem = await _repository.GetByIdAsync(id);
        if (linguagem == null) _notifications.AddError($"Linguagem com ID {id} não encontrada.");
        return linguagem;
    }

    public async Task<LinguagemProgramacao> CreateLinguagemAsync(LinguagemProgramacaoRequest request)
    {
        var linguagem = new LinguagemProgramacao { Nome = request.Nome, Tipo = request.Tipo };
        return await _repository.AddAsync(linguagem);
    }

    public async Task UpdateLinguagemAsync(int id, LinguagemProgramacaoRequest request)
    {
        var linguagemExiste = await _repository.GetByIdAsync(id);
        if (linguagemExiste == null)
        {
            _notifications.AddError($"Linguagem com ID {id} não encontrada.");
            return;
        }

        linguagemExiste.Nome = request.Nome;
        linguagemExiste.Tipo = request.Tipo;
        await _repository.UpdateAsync(linguagemExiste);
    }

    public async Task DeleteLinguagemAsync(int id)
    {
        var linguagem = await _repository.GetByIdAsync(id);
        if (linguagem == null)
        {
            _notifications.AddError($"Linguagem com ID {id} não encontrada.");
            return;
        }
        await _repository.RemoveAsync(linguagem);
    }
}