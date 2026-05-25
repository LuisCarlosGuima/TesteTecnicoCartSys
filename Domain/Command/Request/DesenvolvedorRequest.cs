using Domain.Enumerators;

namespace Domain.Command.Request;

public class DesenvolvedorRequest
{
    public required string Nome { get; set; }
    public string? Email { get; set; }
    public ESenioridade Senioridade { get; set; }
    public int CidadeId { get; set; }
    public string? Observacoes { get; set; }
    public ICollection<int> LinguagensIds { get; set; } = new List<int>();
}