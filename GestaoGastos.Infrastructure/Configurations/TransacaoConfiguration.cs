using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Data)
                .IsRequired();

            builder.Property(x => x.TipoTransacao)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasMaxLength(300);

            builder.Property(x => x.Parcelas)
                .IsRequired();

            builder.Property(x => x.ParcelaAtual)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Transacoes)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Conta)
                .WithMany()
                .HasForeignKey(x => x.ContaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.ContaDestinoId)
                .IsRequired(false);

            builder.HasIndex(x => new { x.UsuarioId, x.Data });
        }
    }
}
