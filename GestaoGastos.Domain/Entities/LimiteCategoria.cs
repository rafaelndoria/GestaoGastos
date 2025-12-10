using GestaoGastos.Domain.Core;

namespace GestaoGastos.Domain.Entities
{
    public class LimiteCategoria : Entity
    {
        protected LimiteCategoria() { }

        public LimiteCategoria(Guid planejamentoId, Guid categoriaId, decimal limite)
        {
            Validar(limite);

            PlanejamentoMensalId = planejamentoId;
            CategoriaId = categoriaId;
            Limite = limite;
        }

        public Guid PlanejamentoMensalId { get; private set; }
        public Guid CategoriaId { get; private set; }
        public decimal Limite { get; private set; }

        public PlanejamentoMensal? PlanejamentoMensal { get; private set; }
        public Categoria? Categoria { get; private set; }

        public void Atualizar(decimal novoLimite)
        {
            Validar(novoLimite);

            Limite = novoLimite;
        }

        private void Validar(decimal limite)
        {
            if (limite <= 0)
                throw new Exception("Limite deve ser maior que zero.");
        }
    }
}
