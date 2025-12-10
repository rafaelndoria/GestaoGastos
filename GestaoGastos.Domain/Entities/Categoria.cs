using GestaoGastos.Domain.Core;
using GestaoGastos.Domain.Enums;

namespace GestaoGastos.Domain.Entities
{
    public class Categoria : Entity
    {
        protected Categoria() { }

        public Categoria(string nome, string icone, string cor, ETipoCategoria tipoCategoria, Guid? usuarioId = null)
        {
            Validar(nome, icone, cor);

            Nome = nome;
            Icone = icone;
            Cor = cor;
            TipoCategoria = tipoCategoria;
            UsuarioId = usuarioId;

            Ativo = true;
            PadraoSistema = usuarioId == null;
        }

        public string Nome { get; private set; }
        public string Icone { get; private set; }
        public string Cor { get; private set; }
        public ETipoCategoria TipoCategoria { get; private set; }
        public bool Ativo { get; private set; }
        public bool PadraoSistema { get; private set; }

        public Guid? UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public bool EhCategoriaUsuario() => UsuarioId.HasValue;

        public void VincularAoUsuario(Guid usuarioId)
        {
            UsuarioId = usuarioId;
            PadraoSistema = false;
        }

        public void Atualizar(string nome, string icone, string cor, ETipoCategoria tipo)
        {
            Validar(nome, icone, cor);

            Nome = nome;
            Icone = icone;
            Cor = cor;
            TipoCategoria = tipo;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        private void Validar(string nome, string icone, string cor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cor))
                throw new Exception("Cor é obrigatória.");
        }
    }
}
