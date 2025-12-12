using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Icone)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Cor)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Ativo)
                .IsRequired();

            builder.Property(x => x.PadraoSistema)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Categorias)
                .HasForeignKey(x => x.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Nome);
        }
    }
}
