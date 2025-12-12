using GestaoGastos.Domain.Entities;

using System.Linq.Expressions;

namespace GestaoGastos.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> ObterCategoriasQuery(Expression<Func<Categoria, bool>> query);
        Task Adicionar(Categoria categoria);
        Task<Categoria> ObterCategoriaPorId(Guid categoriaId);
        Task Atualizar(Categoria categoria);
    }
}
