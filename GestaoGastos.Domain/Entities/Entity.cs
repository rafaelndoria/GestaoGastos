namespace GestaoGastos.Domain.Core
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CriadoEm { get; private set; } = DateTime.UtcNow;
    }
}
