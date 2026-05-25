using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class LinguagemProgramacaoConfiguration : IEntityTypeConfiguration<LinguagemProgramacao>
{
    public void Configure(EntityTypeBuilder<LinguagemProgramacao> builder)
    {
        builder.ToTable("Linguagens");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Nome).IsRequired().HasMaxLength(100);

        builder.Property(l => l.Tipo).IsRequired();

        builder.HasMany(l => l.Desenvolvedores).WithMany(d => d.Linguagens).UsingEntity<Dictionary<string, object>>(
                "DesenvolvedorLinguagem",
                j => j
                    .HasOne<Desenvolvedor>()
                    .WithMany()
                    .HasForeignKey("DesenvolvedorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<LinguagemProgramacao>()
                    .WithMany()
                    .HasForeignKey("LinguagemId")
                    .OnDelete(DeleteBehavior.Cascade)
            );
    }
}