using GestaoGastos.Application.InputModels;
using GestaoGastos.Application.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoGastos.WebApp.Controllers
{
    [Authorize]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Criar()
        {
            var categoriasUsuario = await _categoriaService.ObterCategoriasDoUsuario(UsuarioId);
            var listaCategorias = new CadastroCategoriaInputModel
            {
                Categorias = categoriasUsuario,
                IconesDisponiveis = _categoriaService.ObterIconesDisponiveis()
            };
            return View(listaCategorias);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salvar(CadastroCategoriaInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    inputModel.Categorias = await _categoriaService.ObterCategoriasDoUsuario(UsuarioId);
                    inputModel.IconesDisponiveis = _categoriaService.ObterIconesDisponiveis();

                    return View("Criar", inputModel);
                }

                if (inputModel.Id != Guid.Empty)
                {
                    await _categoriaService.AtualizarCategoria(inputModel);
                    TempData["Sucesso"] = "Categoria atualizada com sucesso.";
                }
                else
                {
                    await _categoriaService.CadastrarCategoria(UsuarioId, UserRole, inputModel);
                    TempData["Sucesso"] = "Categoria cadastrada com sucesso.";
                }

                return RedirectToAction("Criar");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Não foi possível salvar a categoria: " + ex.Message;
                return RedirectToAction("Criar");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AlterarStatus(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    TempData["Erro"] = "Id inválido.";
                    return RedirectToAction("Criar");
                }

                await _categoriaService.AlterarStatusCategoria(id);

                TempData["Sucesso"] = "Status alterado com sucesso.";
                return RedirectToAction("Criar");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Não foi possível atualizar o status: " + ex.Message;
                return RedirectToAction("Criar");
            }
        }
    }
}
