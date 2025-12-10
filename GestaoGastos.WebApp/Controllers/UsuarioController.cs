using GestaoGastos.Application.InputModels;
using GestaoGastos.Application.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace GestaoGastos.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Login(string? mensagemAcessoNegado = null)
        {
            if (mensagemAcessoNegado != null)
                ModelState.AddModelError(string.Empty, mensagemAcessoNegado);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(inputModel);

                var token = await _usuarioService.LoginUsuario(inputModel);
                HttpContext.Session.SetString("JWTToken", token);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel realizar o login: " + ex.Message);
                return View(inputModel);
            }

        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(CadastroUsuarioInputModel inputModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(inputModel);

                await _usuarioService.CadastrarUsuario(inputModel);

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Não foi possivel cadastrar o usuário: " + ex.Message);
                return View(inputModel);
            }
        }

        public IActionResult AcessoNegado(string codigoErro)
        {
            var mensagem = codigoErro switch
            {
                "401" => "Você precisa estar logado para acessar essa área.",
                "403" => "Você não tem permissão para acessar essa área.",
                _ => "Acesso negado."
            };
            return RedirectToAction("Login", new { mensagemAcessoNegado = mensagem });
        }
    }
}
