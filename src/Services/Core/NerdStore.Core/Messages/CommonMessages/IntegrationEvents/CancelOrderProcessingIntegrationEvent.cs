using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents.Base;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class CancelOrderProcessingIntegrationEvent : IntegrationEvent
{

    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public OrderProductsList OrderProducts { get; private set; }

    public CancelOrderProcessingIntegrationEvent(
        Guid orderId,
        Guid customerId,
        OrderProductsList orderProducts)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
        OrderProducts = orderProducts;
    }
}