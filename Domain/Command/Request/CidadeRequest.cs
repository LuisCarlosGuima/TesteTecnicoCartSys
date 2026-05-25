namespace Domain.Command.Request;

public class CidadeRequest
{
    public required string Nome { get; set; }
    public int EstadoId { get; set; }
}