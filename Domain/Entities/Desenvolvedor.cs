using Domain.Enumerators;

namespace Domain.Entities;

public class Desenvolvedor : BaseEntity
{
    public string? Email { get; set; }
    public ESenioridade Senioridade { get; set; }
    public int CidadeId { get; set; }
    public Cidade Cidade { get; set; }
    public ICollection<LinguagemProgramacao> Linguagens { get; set; } = new List<LinguagemProgramacao>();
    public string? Observacoes { get; set; }
}
