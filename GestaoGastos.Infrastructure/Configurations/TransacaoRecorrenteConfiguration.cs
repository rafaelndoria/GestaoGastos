using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class TransacaoRecorrenteConfiguration : IEntityTypeConfiguration<TransacaoRecorrente>
    {
        public void Configure(EntityTypeBuilder<TransacaoRecorrente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.DataInicio)
                .IsRequired();

            builder.Property(x => x.DataAtual)
                .IsRequired();

            builder.Property(x => x.ProximaExecucao)
                .IsRequired();

            builder.Property(x => x.Periodicidade)
                .IsRequired();

            builder.Property(x => x.TipoTransacao)
                .IsRequired();

            builder.Property(x => x.IntervaloDia)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.TransacoesRecorrentes)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Conta)
                .WithMany()
                .HasForeignKey(x => x.ContaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.UsuarioId, x.Ativo });
        }
    }
}
