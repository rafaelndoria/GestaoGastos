using GestaoGastos.Domain;
using GestaoGastos.Domain.Entities;
using GestaoGastos.Infra.Context;

namespace GestaoGastos.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
