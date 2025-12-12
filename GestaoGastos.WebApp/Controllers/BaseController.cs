using GestaoGastos.Domain.Enums;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace GestaoGastos.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Guid UsuarioId =>
        Guid.Parse(User.FindFirstValue("UsuarioId"));

        protected ERole UserRole =>
        Enum.TryParse(User.FindFirstValue(ClaimTypes.Role), out ERole role)
            ? role
            : throw new Exception("Role inválida no token.");
    }
}
