using GestaoGastos.Domain.Entities;

namespace GestaoGastos.Domain
{
    public interface IUsuarioRepository
    {
        Task AdicionarUsuarioAsync(Usuario usuario);
    }
}
