using Domain.Command.Request;
using Domain.Entities;

namespace Service.DesenvolvedorServices;

public interface IDesenvolvedorService
{
    Task<Desenvolvedor> GetDesenvolvedorByIdAsync(int id);
    Task<Desenvolvedor> CreateDesenvolvedorAsync(DesenvolvedorRequest request);
    Task UpdateDesenvolvedorAsync(int id, DesenvolvedorRequest request);
    Task<IEnumerable<Desenvolvedor>> GetAllDesenvolvedoresAsync();
    byte[] GerarRelatorioPdf();
}