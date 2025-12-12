using GestaoGastos.Application.InputModels;
using GestaoGastos.Application.Services.Interfaces;
using GestaoGastos.Application.ViewModels;
using GestaoGastos.Domain.Entities;
using GestaoGastos.Domain.Enums;
using GestaoGastos.Domain.Interfaces;

namespace GestaoGastos.Application.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task AlterarStatusCategoria(Guid categoriaId)
        {
            if (categoriaId == Guid.Empty)
                throw new ArgumentException("CategoriaId inválido.", nameof(categoriaId));

            var categoria = await _categoriaRepository.ObterCategoriaPorId(categoriaId);

            if (categoria == null)
                throw new KeyNotFoundException("Categoria não encontrada.");

            if (categoria.Ativo)
                categoria.Desativar();
            else
                categoria.Ativar();

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task AtualizarCategoria(CadastroCategoriaInputModel inputModel)
        {
            if (inputModel == null) throw new ArgumentNullException(nameof(inputModel));

            if (inputModel.Id == Guid.Empty)
                throw new ArgumentException("Id da categoria inválido para atualização.", nameof(inputModel.Id));

            var categoria = await _categoriaRepository.ObterCategoriaPorId(inputModel.Id);

            if (categoria == null)
                throw new KeyNotFoundException("Categoria não encontrada.");

            categoria.Atualizar(inputModel.Nome, inputModel.Icone, inputModel.Cor);

            await _categoriaRepository.Atualizar(categoria);
        }

        public async Task CadastrarCategoria(Guid usuarioId, ERole roleUsuario, CadastroCategoriaInputModel inputModel)
        {
            if (inputModel == null) throw new ArgumentNullException(nameof(inputModel));

            Categoria categoria;
            if (roleUsuario == ERole.Admin)
            {
                categoria = new Categoria(inputModel.Nome, inputModel.Icone, inputModel.Cor);
            }
            else
            {
                categoria = new Categoria(inputModel.Nome, inputModel.Icone, inputModel.Cor, usuarioId);
            }

            await _categoriaRepository.Adicionar(categoria);
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasDoUsuario(Guid usuarioId)
        {
            var categorias = await _categoriaRepository.ObterCategoriasQuery(x => x.UsuarioId == usuarioId);
            return categorias.Select(c => MapToViewModel(c));
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasPadraoDoSistema()
        {
            var categorias = await _categoriaRepository.ObterCategoriasQuery(x => x.PadraoSistema == true);
            return categorias.Select(c => MapToViewModel(c));
        }

        private static CategoriaViewModel MapToViewModel(Categoria c)
        {
            return new CategoriaViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Cor = c.Cor,
                Icone = c.Icone,
                Ativo = c.Ativo
            };
        }

        public string[] ObterIconesDisponiveis()
        {
            var icones = new[]
            {
                "fa-bus",
                "fa-car",
                "fa-shopping-cart",
                "fa-utensils",
                "fa-home",
                "fa-heart",
                "fa-film",
                "fa-book",
                "fa-medkit",
                "fa-plane",
                "fa-gift",
                "fa-coffee"
            };

            return icones;
        }
    }
}
