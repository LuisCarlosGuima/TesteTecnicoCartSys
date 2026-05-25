using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Contexts;
using Repository.Repositories;

namespace Repository;

public static class DependencyInjectionRepository
{
    public static void LoadDependencyInjectionFromRepository(this IServiceCollection services)
    {
        services.AddScoped<ICidadeRepository, CidadeRepository>();
        services.AddScoped<IDesenvolvedorRepository, DesenvolvedorRepository>();
        services.AddScoped<IEstadoRepository, EstadoRepository>();
        services.AddScoped<ILinguagemProgramacaoRepository, LinguagemProgramacaoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }


    public static IServiceCollection LoadDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<Context>(options =>
        {
            options.UseSqlServer(connectionString);

            if (env.IsDevelopment())
            {
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine);
            }
        });

        return services;
    }
}