using System.ComponentModel.DataAnnotations;

namespace GestaoGastos.Application.InputModels
{
    public class LoginUsuarioInputModel
    {

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Senha { get; set; }
    }
}
