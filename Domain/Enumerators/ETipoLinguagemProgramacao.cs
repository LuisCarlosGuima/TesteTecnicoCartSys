using System.ComponentModel;

namespace Domain.Enumerators;

public enum ETipoLinguagemProgramacao
{
    [Description("Front-End")]
    FrontEnd = 1,
    [Description("Back-End")]
    BackEnd = 2,
    [Description("Mobile")]
    Mobile = 3,
    [Description("Database")]
    Database = 4,
    [Description("DevOps")]
    DevOps = 5,
}
