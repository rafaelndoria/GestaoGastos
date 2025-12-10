using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class PlanejamentoMensalConfiguration : IEntityTypeConfiguration<PlanejamentoMensal>
    {
        public void Configure(EntityTypeBuilder<PlanejamentoMensal> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.Ano)
                .IsRequired();

            builder.Property(x => x.Mes)
                .IsRequired();

            builder.Property(x => x.LimiteGeral)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.PlanejamentoMensais)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasIndex(x => new { x.UsuarioId, x.Ano, x.Mes })
                .IsUnique();
        }
    }
}
