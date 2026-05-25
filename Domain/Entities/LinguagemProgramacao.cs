using Domain.Enumerators;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class LinguagemProgramacao : BaseEntity
{
    public ETipoLinguagemProgramacao Tipo { get; set; }

    [JsonIgnore]
    public ICollection<Desenvolvedor> Desenvolvedores { get; set; } = new List<Desenvolvedor>();
}
