namespace Domain.Entities;

public class Estado : BaseEntity
{
    public required string Sigla { get; set; }
    public ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();
}