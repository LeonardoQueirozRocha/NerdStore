using NerdStore.Core.Messages.CommonMessages.IntegrationEvents.Base;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class RejectedStockOrderIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public RejectedStockOrderIntegrationEvent(Guid orderId, Guid customerId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
    }
}