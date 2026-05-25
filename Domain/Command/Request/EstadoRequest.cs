namespace Domain.Command.Request;

public class EstadoRequest
{
    public required string Nome { get; set; }
    public required string Sigla { get; set; }
}