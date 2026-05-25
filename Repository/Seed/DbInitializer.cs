using Domain.Entities;
using Domain.Enumerators;
using Microsoft.AspNetCore.Identity;
using Repository.Contexts;

namespace Repository.Seed;

public static class DbInitializer
{
    public static void Seed(Context context)
    {
        if (!context.Estados.Any())
        {
            var sc = new Estado { Nome = "Santa Catarina", Sigla = "SC" };
            var pr = new Estado { Nome = "Paraná", Sigla = "PR" };

            context.Estados.AddRange(sc, pr);
            context.SaveChanges();

            var itajai = new Cidade { Nome = "Itajaí", EstadoId = sc.Id };
            var bc = new Cidade { Nome = "Balneário Camboriú", EstadoId = sc.Id };
            var curitiba = new Cidade { Nome = "Curitiba", EstadoId = pr.Id };

            context.Cidades.AddRange(itajai, bc, curitiba);
            context.SaveChanges();
        }

        if (!context.Linguagens.Any())
        {
            context.Linguagens.AddRange(
                new LinguagemProgramacao { Nome = "C#", Tipo = ETipoLinguagemProgramacao.BackEnd },
                new LinguagemProgramacao { Nome = "React", Tipo = ETipoLinguagemProgramacao.FrontEnd },
                new LinguagemProgramacao { Nome = "Flutter", Tipo = ETipoLinguagemProgramacao.Mobile },
                new LinguagemProgramacao { Nome = "PostgreSQL", Tipo = ETipoLinguagemProgramacao.Database },
                new LinguagemProgramacao { Nome = "Docker", Tipo = ETipoLinguagemProgramacao.DevOps }
            );

            context.SaveChanges();
        }

        if (!context.Usuarios.Any())
        {
            var hasher = new PasswordHasher<Usuario>();

            var user = new Usuario
            {
                Nome = "Admin",
                Email = "admin@admin.com"
            };

            user.Senha = hasher.HashPassword(user, "123456");

            context.Usuarios.Add(user);
            context.SaveChanges();
        }
    }
}