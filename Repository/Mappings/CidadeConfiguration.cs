using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mappings;

public class CidadeConfiguration : IEntityTypeConfiguration<Cidade>
{
    public void Configure(EntityTypeBuilder<Cidade> builder)
    {
        builder.ToTable("Cidades");

        builder.HasKey(c => c.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);

        builder.HasOne(c => c.Estado).WithMany(e => e.Cidades).HasForeignKey(c => c.EstadoId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Desenvolvedores).WithOne(d => d.Cidade).HasForeignKey(d => d.CidadeId).OnDelete(DeleteBehavior.Restrict);
    }
}