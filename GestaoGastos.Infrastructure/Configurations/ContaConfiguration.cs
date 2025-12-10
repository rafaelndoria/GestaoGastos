using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Cor)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Saldo)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Contas)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.TransacoesRecorrentes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UsuarioId, x.Nome })
                .IsUnique();
        }
    }
}
