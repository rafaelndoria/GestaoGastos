using GestaoGastos.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace GestaoGastos.Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<LimiteCategoria> LimiteCategorias { get; set; }
        public DbSet<MetaFinanceira> MetaFinanceiras { get; set; }
        public DbSet<PlanejamentoMensal> PlanejamentoMensais { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<TransacaoRecorrente> TransacaoRecorrentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
