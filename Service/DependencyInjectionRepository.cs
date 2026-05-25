using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.DesenvolvedorServices;
using Service.NotificationService;

namespace Service;

public static class DependencyInjectionService
{
    public static void LoadDependencyInjectionFromService(this IServiceCollection services)
    {
        services.AddScoped<INotifications, Notifications>();
        services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IDesenvolvedorService, DesenvolvedorService>();
        services.AddScoped<ICidadeService, CidadeService>();
        services.AddScoped<IEstadoService, EstadoService>();
        services.AddScoped<ILinguagemProgramacaoService, LinguagemProgramacaoService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
    }
}