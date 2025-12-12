using GestaoGastos.Application.ViewModels;

using System.ComponentModel.DataAnnotations;

namespace GestaoGastos.Application.InputModels
{
    public class CadastroCategoriaInputModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [RegularExpression("^#[0-9A-Fa-f]{6}$", ErrorMessage = "A cor deve estar no formato hexadecimal. Ex: #FF0000")]
        public string Cor { get; set; }

        [Required(ErrorMessage = "O ícone é obrigatório.")]
        public string Icone { get; set; }

        public string[] IconesDisponiveis { get; set; } = new[] { "fa-bus", "fa-car" };

        public IEnumerable<CategoriaViewModel> Categorias { get; set; } = new List<CategoriaViewModel>();
    }
}
