using GestaoGastos.Application.InputModels;
using GestaoGastos.Domain.Entities;

using System.Linq.Expressions;

namespace GestaoGastos.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task CadastrarUsuario(CadastroUsuarioInputModel inputModel);
        Task<string> LoginUsuario(LoginUsuarioInputModel inputModel);
        Task<Usuario> GetUsuarioPorEmailESenha(string email, string senhaHash);
        Task<IEnumerable<Usuario>> GetUsuariosQuery(Expression<Func<Usuario, bool>> query);
    }
}
