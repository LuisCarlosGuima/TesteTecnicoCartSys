using Domain.Command.Request;
using Domain.Entities;

public interface IEstadoService
{
    Task<IEnumerable<Estado>> GetAllEstadosAsync();
    Task<Estado?> GetEstadoByIdAsync(int id);
    Task<Estado> CreateEstadoAsync(EstadoRequest request);
    Task UpdateEstadoAsync(int id, EstadoRequest request);
    Task DeleteEstadoAsync(int id);
}