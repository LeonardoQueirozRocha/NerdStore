using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class UpdatedOrderEvent : Event
{
    public Guid CustomerId { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal TotalValue { get; private set; }

    public UpdatedOrderEvent(
        Guid customerId,
        Guid orderId,
        decimal totalValue)
    {
        AggregateId = orderId;
        CustomerId = customerId;
        OrderId = orderId;
        TotalValue = totalValue;
    }
}