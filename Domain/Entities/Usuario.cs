namespace Domain.Entities;

public class Usuario : BaseEntity
{
    public string Email { get; set; }    
    public string Senha { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}
