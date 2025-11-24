using Microsoft.AspNetCore.Mvc;

namespace GestaoGastos.WebApp.Controllers
{
   public class UsuarioController : Controller
   {
      public IActionResult Login()
      {
         return View();
      }

      public IActionResult Registrar()
      {
         return View();
      }
   }
}
