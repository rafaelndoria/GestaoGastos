using GestaoGastos.Domain.Core;
using GestaoGastos.Domain.Enums;

namespace GestaoGastos.Domain.Entities
{
    public class TransacaoRecorrente : Entity
    {
        protected TransacaoRecorrente() { }

        public TransacaoRecorrente(
            string nome,
            decimal valor,
            DateTime dataInicio,
            EPeriodicidade periodicidade,
            ETipoTransacao tipo,
            int intervaloDia,
            Guid usuarioId,
            Guid contaId)
        {
            Nome = nome;
            Valor = valor;
            DataInicio = dataInicio;
            DataAtual = dataInicio;
            UsuarioId = usuarioId;
            ContaId = contaId;
            Periodicidade = periodicidade;
            IntervaloDia = intervaloDia;
            TipoTransacao = tipo;

            Ativo = true;

            Validar();
            GerarProximaExecucao();
        }

        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataAtual { get; private set; }
        public DateTime ProximaExecucao { get; private set; }

        public bool Ativo { get; private set; }

        public EPeriodicidade Periodicidade { get; private set; }
        public ETipoTransacao TipoTransacao { get; private set; }
        public int IntervaloDia { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Guid ContaId { get; private set; }

        public Usuario? Usuario { get; private set; }
        public Conta? Conta { get; private set; }

        public void AlterarValor(decimal valor)
        {
            if (valor <= 0) throw new Exception("Valor inválido.");
            Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome inválido.");
            Nome = nome;
        }

        public void AlterarDataInicio(DateTime novaData)
        {
            if (novaData == default)
                throw new Exception("Data inválida.");
            DataInicio = novaData;
            DataAtual = novaData;

            GerarProximaExecucao();
        }

        public void AlterarPeriodicidade(EPeriodicidade periodicidade, int intervalo)
        {
            Periodicidade = periodicidade;
            IntervaloDia = intervalo;
            Validar();
            GerarProximaExecucao();
        }

        public void Ativar() => Ativo = true;
        public void Inativar() => Ativo = false;

        public void RegistrarExecucao()
        {
            DataAtual = ProximaExecucao;
            GerarProximaExecucao();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new Exception("Nome obrigatório.");

            if (Valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            if (DataInicio == default)
                throw new Exception("Data inicial inválida.");

            if (UsuarioId == Guid.Empty)
                throw new Exception("Usuário inválido.");

            if (ContaId == Guid.Empty)
                throw new Exception("Conta inválida.");

            if (Periodicidade == EPeriodicidade.IntevalosPersonalizados && IntervaloDia <= 0)
                throw new Exception("Intervalo deve ser maior que zero.");

            if (Periodicidade != EPeriodicidade.IntevalosPersonalizados && IntervaloDia != 0)
                throw new Exception("Intervalo deve ser zero quando não é personalizado.");
        }

        private void GerarProximaExecucao()
        {
            DateTime baseDate = DataAtual;
            ProximaExecucao = Periodicidade switch
            {
                EPeriodicidade.Diaria => baseDate.AddDays(1),
                EPeriodicidade.Semanal => baseDate.AddDays(7),
                EPeriodicidade.Mensal => baseDate.AddMonths(1),
                EPeriodicidade.Anual => baseDate.AddYears(1),
                EPeriodicidade.IntevalosPersonalizados => baseDate.AddDays(IntervaloDia),
                _ => throw new Exception("Periodicidade inválida.")
            };
        }
    }
}
