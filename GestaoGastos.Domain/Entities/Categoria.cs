using GestaoGastos.Domain.Core;

namespace GestaoGastos.Domain.Entities
{
    public class Categoria : Entity
    {
        protected Categoria() { }

        public Categoria(string nome, string icone, string cor, Guid? usuarioId = null)
        {
            Validar(nome, icone, cor);

            Nome = nome;
            Icone = icone;
            Cor = cor;
            UsuarioId = usuarioId;

            Ativo = true;
            PadraoSistema = !usuarioId.HasValue;
        }

        public string Nome { get; private set; }
        public string Icone { get; private set; }
        public string Cor { get; private set; }
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

        public void Atualizar(string nome, string icone, string cor)
        {
            Validar(nome, icone, cor);

            Nome = nome;
            Icone = icone;
            Cor = cor;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        private void Validar(string nome, string icone, string cor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cor))
                throw new DomainException("Cor é obrigatória.");

            if (string.IsNullOrWhiteSpace(icone))
                throw new DomainException("Ícone é obrigatório.");
        }
    }
}
