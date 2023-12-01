using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class ProductOrderRemovedEvent : Event
{
    public Guid CustomerId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }

    public ProductOrderRemovedEvent(
        Guid customerId, 
        Guid orderId, 
        Guid productId)
    {
        AggregateId = orderId;
        CustomerId = customerId;
        OrderId = orderId;
        ProductId = productId;
    }
}