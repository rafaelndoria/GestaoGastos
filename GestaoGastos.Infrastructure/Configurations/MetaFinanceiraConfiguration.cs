using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class MetaFinanceiraConfiguration : IEntityTypeConfiguration<MetaFinanceira>
    {
        public void Configure(EntityTypeBuilder<MetaFinanceira> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Icone)
                .HasMaxLength(50);

            builder.Property(x => x.ValorAtual)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ValorFinal)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataFim)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.MetasFinanceiras)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.UsuarioId, x.Nome });
        }
    }
}
