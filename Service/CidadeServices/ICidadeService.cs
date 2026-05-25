using Domain.Command.Request;
using Domain.Entities;

namespace Service.DesenvolvedorServices;

public interface ICidadeService
{
    Task<IEnumerable<Cidade>> GetAllCidadesAsync();
    Task<Cidade> GetCidadeByIdAsync(int id);
    Task<Cidade> CreateCidadeAsync(CidadeRequest request);
    Task UpdateCidadeAsync(int id, CidadeRequest request);
    Task DeleteCidadeAsync(int id);
}