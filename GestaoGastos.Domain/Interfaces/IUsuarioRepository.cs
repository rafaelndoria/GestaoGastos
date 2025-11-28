using GestaoGastos.Domain.Entities;

using System.Linq.Expressions;

namespace GestaoGastos.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid id);
        Task<IEnumerable<Usuario>> ObterUsuariosQuery(Expression<Func<Usuario, bool>> query);
    }
}
