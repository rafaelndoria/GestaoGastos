using GestaoGastos.Domain.Entities;
using GestaoGastos.Domain.Interfaces;
using GestaoGastos.Infra.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

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

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosQuery(Expression<Func<Usuario, bool>> query)
        {
            return await _context.Usuarios.Where(query).ToListAsync();
        }
    }
}
