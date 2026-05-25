using Domain.Command.Request;
using Domain.Entities;

public interface ILinguagemProgramacaoService
{
    Task<IEnumerable<LinguagemProgramacao>> GetAllLinguagensAsync();
    Task<LinguagemProgramacao> GetLinguagemByIdAsync(int id);
    Task<LinguagemProgramacao> CreateLinguagemAsync(LinguagemProgramacaoRequest request);
    Task UpdateLinguagemAsync(int id, LinguagemProgramacaoRequest request);
    Task DeleteLinguagemAsync(int id);
}