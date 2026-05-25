using Domain.Command.Response;
using Domain.Entities;

namespace Domain.Contracts;

public interface IAuthService
{
    Task<LoginResponse> Login(string email, string senha);
    Task<TokenResponse> Refresh(string refreshToken);
}