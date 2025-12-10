using GestaoGastos.Domain.Core;

namespace GestaoGastos.Domain.Entities
{
    public class PlanejamentoMensal : Entity
    {
        protected PlanejamentoMensal() { }

        public PlanejamentoMensal(Guid usuarioId, int ano, int mes, decimal limiteGeral)
        {
            UsuarioId = usuarioId;
            Ano = ano;
            Mes = mes;

            AtualizarLimiteGeral(limiteGeral);
        }

        public Guid UsuarioId { get; private set; }
        public int Ano { get; private set; }
        public int Mes { get; private set; }
        public decimal LimiteGeral { get; private set; }

        public Usuario? Usuario { get; private set; }

        private readonly List<LimiteCategoria> _limites = new();
        public IReadOnlyCollection<LimiteCategoria> LimitesPorCategoria => _limites;

        public void AtualizarLimiteGeral(decimal novoLimite)
        {
            if (novoLimite <= 0)
                throw new Exception("Limite deve ser maior que zero.");

            LimiteGeral = novoLimite;
        }

        public void AdicionarLimiteCategoria(Guid categoriaId, decimal limite)
        {
            if (_limites.Any(x => x.CategoriaId == categoriaId))
                throw new Exception("Limite já definido para esta categoria.");

            _limites.Add(new LimiteCategoria(Id, categoriaId, limite));
        }
    }
}
