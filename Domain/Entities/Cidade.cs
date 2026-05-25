namespace Domain.Entities;

public class Cidade : BaseEntity
{
    public int EstadoId { get; set; }
    public Estado Estado { get; set; }
    public ICollection<Desenvolvedor> Desenvolvedores { get; set; } = new List<Desenvolvedor>();
}
