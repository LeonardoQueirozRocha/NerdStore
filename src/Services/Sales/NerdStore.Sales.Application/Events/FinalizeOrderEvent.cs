using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class FinalizeOrderEvent : Event
{
    public Guid OrderId { get; private set; }

    public FinalizeOrderEvent(Guid orderId)
    {
        AggregateId = orderId;
        OrderId = orderId;
    }
}