using GestaoGastos.Domain.Core;
using GestaoGastos.Domain.Enums;

namespace GestaoGastos.Domain.Entities
{
    public class Transacao : Entity
    {
        protected Transacao() { }

        public Transacao(
            decimal valor,
            DateTime data,
            ETipoTransacao tipo,
            Guid usuarioId,
            Guid contaId,
            Guid? categoriaId = null,
            string? descricao = null)
        {
            if (valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            Valor = valor;
            Data = data;
            TipoTransacao = tipo;
            UsuarioId = usuarioId;
            ContaId = contaId;
            CategoriaId = categoriaId;
            Descricao = descricao;

            Pago = true;
            Parcelas = 1;
            ParcelaAtual = 1;
        }

        public decimal Valor { get; private set; }
        public DateTime Data { get; private set; }
        public string? Descricao { get; private set; }
        public ETipoTransacao TipoTransacao { get; private set; }

        public bool Pago { get; private set; }
        public int Parcelas { get; private set; }
        public int ParcelaAtual { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Guid ContaId { get; private set; }
        public Guid? ContaDestinoId { get; private set; }
        public Guid? CategoriaId { get; private set; }

        public Usuario? Usuario { get; private set; }
        public Conta? Conta { get; private set; }
        public Categoria? Categoria { get; private set; }

        public void MarcarComoPago() => Pago = true;
        public void MarcarComoNaoPago() => Pago = false;

        public void EditarValor(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("Valor deve ser maior que zero.");

            Valor = valor;
        }

        public void EditarDescricao(string? descricao) => Descricao = descricao;

        public void EditarCategoria(Guid categoriaId) => CategoriaId = categoriaId;

        public void EditarData(DateTime novaData) => Data = novaData;

        public void DefinirParcelamento(int totalParcelas, int parcelaAtual)
        {
            if (totalParcelas < 1)
                throw new Exception("Parcelas inválidas.");

            Parcelas = totalParcelas;
            ParcelaAtual = parcelaAtual;
        }

        public static (Transacao saida, Transacao entrada) CriarTransferencia(
            decimal valor,
            Guid usuarioId,
            Guid origemId,
            Guid destinoId,
            DateTime data,
            string? descricao = null)
        {
            if (origemId == destinoId)
                throw new Exception("Contas devem ser diferentes.");

            var saida = new Transacao(valor, data, ETipoTransacao.Transferencia, usuarioId, origemId, null, descricao);
            saida.ContaDestinoId = destinoId;

            var entrada = new Transacao(valor, data, ETipoTransacao.Transferencia, usuarioId, destinoId, null, descricao);
            entrada.ContaDestinoId = origemId;

            return (saida, entrada);
        }
    }
}
