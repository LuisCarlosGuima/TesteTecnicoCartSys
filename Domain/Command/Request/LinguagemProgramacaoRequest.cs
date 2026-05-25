using Domain.Enumerators;

namespace Domain.Command.Request;

public class LinguagemProgramacaoRequest
{
    public required string Nome { get; set; }
    public ETipoLinguagemProgramacao Tipo { get; set; }
}