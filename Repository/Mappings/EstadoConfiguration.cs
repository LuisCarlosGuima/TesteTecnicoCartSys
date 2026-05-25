using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("Estados");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Sigla).IsRequired().HasMaxLength(2);

        builder.HasIndex(e => e.Sigla).IsUnique();

        builder.HasMany(e => e.Cidades).WithOne(c => c.Estado).HasForeignKey(c => c.EstadoId).OnDelete(DeleteBehavior.Restrict);
    }
}