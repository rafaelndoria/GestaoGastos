using GestaoGastos.Domain.Entities;
using GestaoGastos.Domain.Interfaces;
using GestaoGastos.Infra.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace GestaoGastos.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Categoria categoria)
        {
            if (categoria == null) throw new ArgumentNullException(nameof(categoria));

            _context.Categoria.Add(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Categoria categoria)
        {
            if (categoria == null) throw new ArgumentNullException(nameof(categoria));

            _context.Categoria.Update(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid categoriaId)
        {
            if (categoriaId == Guid.Empty) return null;

            return await _context.Categoria
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == categoriaId);
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasQuery(Expression<Func<Categoria, bool>> query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return await _context.Categoria
                .AsNoTracking()
                .Where(query)
                .ToListAsync();
        }
    }
}
