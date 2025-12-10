using GestaoGastos.Domain.Core;
using GestaoGastos.Domain.Enums;

namespace GestaoGastos.Domain.Entities
{
    public class MetaFinanceira : Entity
    {
        protected MetaFinanceira() { }

        public MetaFinanceira(string nome, string icone, decimal valorFinal, DateTime inicio, DateTime fim, Guid usuarioId)
        {
            Validar(nome, icone, valorFinal, inicio, fim);

            Nome = nome;
            Icone = icone;
            ValorFinal = valorFinal;
            ValorAtual = 0;

            DataInicio = inicio;
            DataFim = fim;

            UsuarioId = usuarioId;

            DefinirStatus();
        }

        public string Nome { get; private set; }
        public string Icone { get; private set; }
        public decimal ValorAtual { get; private set; }
        public decimal ValorFinal { get; private set; }

        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }

        public EStatusMeta Status { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        public void Atualizar(string nome, string icone, decimal valorFinal, DateTime inicio, DateTime fim)
        {
            VerificarFinalizada();
            Validar(nome, icone, valorFinal, inicio, fim);

            Nome = nome;
            Icone = icone;
            ValorFinal = valorFinal;
            DataInicio = inicio;
            DataFim = fim;

            DefinirStatus();
        }

        public void AdicionarValor(decimal valor)
        {
            VerificarFinalizada();

            if (valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            ValorAtual = Math.Min(ValorAtual + valor, ValorFinal);

            DefinirStatus();
        }

        public void RemoverValor(decimal valor)
        {
            VerificarFinalizada();

            if (valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            ValorAtual = Math.Max(ValorAtual - valor, 0);

            DefinirStatus();
        }

        private void VerificarFinalizada()
        {
            if (Status == EStatusMeta.Finalizada)
                throw new Exception("Meta já finalizada.");
        }

        private void DefinirStatus()
        {
            if (ValorAtual >= ValorFinal)
                Status = EStatusMeta.Finalizada;
            else if (DateTime.UtcNow > DataFim)
                Status = EStatusMeta.Expirada;
            else
                Status = EStatusMeta.EmAndamento;
        }

        private void Validar(string nome, string icone, decimal valorFinal, DateTime inicio, DateTime fim)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome é obrigatório.");

            if (valorFinal <= 0)
                throw new Exception("Valor final deve ser maior que zero.");

            if (inicio > fim)
                throw new Exception("Data início não pode ser maior que data final.");

            if (!string.IsNullOrWhiteSpace(icone) && icone.Length > 50)
                throw new Exception("Ícone muito longo.");
        }
    }
}
