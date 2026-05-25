using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Contexts;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<Cidade> Cidades => Set<Cidade>();
    public DbSet<Desenvolvedor> Desenvolvedores => Set<Desenvolvedor>();
    public DbSet<LinguagemProgramacao> Linguagens => Set<LinguagemProgramacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        modelBuilder.Entity<Desenvolvedor>()
                    .HasMany(d => d.Linguagens)
                    .WithMany(l => l.Desenvolvedores)
                    .UsingEntity(j => j.ToTable("DesenvolvedorLinguagemProgramacao"));

        base.OnModelCreating(modelBuilder);
    }
}