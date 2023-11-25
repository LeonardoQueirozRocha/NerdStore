using NerdStore.Core.Messages.CommonMessages.DomainEvents;

namespace NerdStore.Catalog.Domain.Events;

public class ProductBelowStockEvent : DomainEvent
{
    public int QuantityRemaining { get; private set; }

    public ProductBelowStockEvent(Guid aggregateId, int quantityRemaining) : base(aggregateId)
    {
        QuantityRemaining = quantityRemaining;
    }
}