using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class LimiteCategoriaConfiguration : IEntityTypeConfiguration<LimiteCategoria>
    {
        public void Configure(EntityTypeBuilder<LimiteCategoria> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Limite)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.PlanejamentoMensal)
                .WithMany(p => p.LimitesPorCategoria)
                .HasForeignKey(x => x.PlanejamentoMensalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.PlanejamentoMensalId, x.CategoriaId })
                .IsUnique();
        }
    }
}
