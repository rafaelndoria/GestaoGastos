using GestaoGastos.Domain.Enums;

using System.Text.RegularExpressions;

namespace GestaoGastos.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario(string nome, string email, string senhaHash, ERole role)
        {
            Validar(nome, email, senhaHash);
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;

            DataCriacao = DateTime.UtcNow;
            Ativo = true;
            Id = Guid.NewGuid();
        }

        public Usuario(string nome, string email, string senhaHash, ERole role, Guid id)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
            Id = id;

            DataCriacao = DateTime.UtcNow;
            Ativo = true;
        }


        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public bool Ativo { get; private set; }
        public ERole Role { get; private set; }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Atualizar(string nome, string email, string senha)
        {
            Validar(nome, email, senha);

            Nome = nome;
            Email = email;
            SenhaHash = senha;
        }

        private void Validar(string nome, string email, string senha)
        {

            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("O nome não pode ser vazio.");
            if (nome.Length < 3)
                throw new Exception("O nome deve conter pelo menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("O email não pode ser vazio.");
            if (!EmailValido(email))
                throw new Exception("O email informado é inválido.");

            if (string.IsNullOrWhiteSpace(senha))
                throw new Exception("A senha não pode ser vazia.");
            if (senha.Length < 6)
                throw new Exception("A senha deve possuir no mínimo 6 caracteres.");
        }

        private bool EmailValido(string email)
        {
            return Regex.IsMatch(email,
                @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
                RegexOptions.IgnoreCase);
        }
    }
}
