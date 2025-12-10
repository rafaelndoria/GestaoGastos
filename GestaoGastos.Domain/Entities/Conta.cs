using GestaoGastos.Domain.Core;

namespace GestaoGastos.Domain.Entities
{
    public class Conta : Entity
    {
        protected Conta() { }

        public Conta(string nome, string cor, Guid usuarioId, decimal saldoInicial = 0)
        {
            Validar(nome, cor);

            Nome = nome;
            Cor = cor;
            UsuarioId = usuarioId;
            Saldo = saldoInicial;
            Ativo = true;
        }

        public string Nome { get; private set; }
        public string Cor { get; private set; }
        public decimal Saldo { get; private set; }
        public bool Ativo { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public IReadOnlyCollection<TransacaoRecorrente> TransacoesRecorrentes => _recorrencias;
        private List<TransacaoRecorrente> _recorrencias = new();

        public void Atualizar(string nome, string cor)
        {
            Validar(nome, cor);

            Nome = nome;
            Cor = cor;
        }

        public void AtualizarSaldo(decimal novoSaldo) => Saldo = novoSaldo;

        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;

        private void Validar(string nome, string cor)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome da conta é obrigatório.");

            if (string.IsNullOrWhiteSpace(cor))
                throw new Exception("Cor é obrigatória.");
        }
    }
}
