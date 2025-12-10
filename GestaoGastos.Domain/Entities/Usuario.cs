using GestaoGastos.Domain.Core;
using GestaoGastos.Domain.Enums;

using System.Text.RegularExpressions;

namespace GestaoGastos.Domain.Entities
{
    public class Usuario : Entity
    {
        protected Usuario() { }

        public Usuario(string nome, string email, string senhaHash, ERole role)
        {
            Validar(nome, email, senhaHash);

            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;

            Ativo = true;
            DataCriacao = DateTime.UtcNow;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }

        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        public ERole Role { get; private set; }

        public IEnumerable<Categoria> Categorias { get; private set; } = new List<Categoria>();
        public IEnumerable<Conta> Contas { get; private set; } = new List<Conta>();
        public IEnumerable<MetaFinanceira> MetasFinanceiras { get; private set; } = new List<MetaFinanceira>();
        public IEnumerable<TransacaoRecorrente> TransacoesRecorrentes { get; private set; } = new List<TransacaoRecorrente>();
        public IEnumerable<PlanejamentoMensal> PlanejamentoMensais { get; private set; } = new List<PlanejamentoMensal>();
        public IEnumerable<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        public void Atualizar(string nome, string email, string senha)
        {
            Validar(nome, email, senha);

            Nome = nome;
            Email = email;
            SenhaHash = senha;
        }

        public void Ativar() => Ativo = true;
        public void Desativar() => Ativo = false;

        private void Validar(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome é obrigatório.");

            if (!EmailValido(email))
                throw new Exception("Email inválido.");

            if (senha.Length < 6)
                throw new Exception("Senha deve ter no mínimo 6 caracteres.");
        }

        private bool EmailValido(string email)
        {
            return Regex.IsMatch(email,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                RegexOptions.IgnoreCase);
        }
    }
}
