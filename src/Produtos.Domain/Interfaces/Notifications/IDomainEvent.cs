namespace Produtos.Domain.Interfaces.Notifications
{
    public interface IDomainEvent
    {
        int Version { get; }

        DateTime OccurrenceDate { get; }
    }
}
