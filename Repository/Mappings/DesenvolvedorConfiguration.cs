using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class DesenvolvedorConfiguration : IEntityTypeConfiguration<Desenvolvedor>
{
    public void Configure(EntityTypeBuilder<Desenvolvedor> builder)
    {
        builder.ToTable("Desenvolvedores");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Nome).IsRequired().HasMaxLength(150);

        builder.Property(d => d.Email).IsRequired().HasMaxLength(150);
        builder.HasIndex(d => d.Email).IsUnique();

        builder.Property(d => d.Senioridade).IsRequired();

        builder.Property(d => d.Observacoes).HasMaxLength(500);

        builder.HasOne(d => d.Cidade).WithMany(c => c.Desenvolvedores).HasForeignKey(d => d.CidadeId).OnDelete(DeleteBehavior.Restrict);
    }
}