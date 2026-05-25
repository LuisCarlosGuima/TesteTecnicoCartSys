using Domain.Command.Response;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.NotificationService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repo;
    private readonly IPasswordHasher<Usuario> _hasher;
    private readonly IConfiguration _config;
    private readonly INotifications _notifications;

    public AuthService(IUsuarioRepository repo,
                       IPasswordHasher<Usuario> hasher,
                       IConfiguration config,
                       INotifications notifications)
    {
        _repo = repo;
        _hasher = hasher;
        _config = config;
        _notifications = notifications;
    }

    public async Task<LoginResponse> Login(string email, string senha)
    {
        var user = await _repo.GetByEmailAsync(email);

        if (user == null || _hasher.VerifyHashedPassword(user, user.Senha, senha) == PasswordVerificationResult.Failed)
        {
            _notifications.AddError("E-mail ou senha inválidos.");
            return null;
        }

        var accessToken = GerarToken(user);
        var refreshToken = GerarRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _repo.UpdateAsync(user);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Nome = user.Nome,
            Email = user.Email
        };
    }

    private string GerarRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public async Task<TokenResponse> Refresh(string refreshToken)
    {
        var user = await _repo.GetByRefreshTokenAsync(refreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            _notifications.AddError("Token de atualização inválido ou expirado.");
            return null;
        }

        var newAccessToken = GerarToken(user);
        var newRefreshToken = GerarRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _repo.UpdateAsync(user);

        return new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
    private string GerarToken(Usuario user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("name", user.Nome),
        };

        var token = new JwtSecurityToken(
            issuer: "CartSys",
            audience: "CartSys",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}