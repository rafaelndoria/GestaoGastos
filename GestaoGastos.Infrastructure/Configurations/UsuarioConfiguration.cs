using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoGastos.Infra.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Role)
                .IsRequired();

            builder.Property(x => x.SenhaHash)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Nome)
                .IsUnique();

            builder.HasData(
                new Usuario("admin",
                            "admin@gmail.com",
                            "b8b8eb83374c0bf3b1c3224159f6119dbfff1b7ed6dfecdd80d4e8a895790a34",
                            Domain.Enums.ERole.Admin));
        }
    }
}
