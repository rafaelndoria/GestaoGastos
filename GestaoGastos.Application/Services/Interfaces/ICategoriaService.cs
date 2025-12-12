using GestaoGastos.Application.InputModels;
using GestaoGastos.Application.ViewModels;
using GestaoGastos.Domain.Enums;

namespace GestaoGastos.Application.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasDoUsuario(Guid usuarioId);
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPadraoDoSistema();
        Task CadastrarCategoria(Guid usuarioId, ERole roleUsuario, CadastroCategoriaInputModel inputModel);
        Task AtualizarCategoria(CadastroCategoriaInputModel inputModel);
        Task AlterarStatusCategoria(Guid categoriaId);
        string[] ObterIconesDisponiveis();
    }
}
