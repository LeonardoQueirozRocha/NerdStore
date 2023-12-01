using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class OrderVoucherApplied : Event
{
    public Guid CustomerId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid VoucherId { get; private set; }

    public OrderVoucherApplied(
        Guid customerId,
        Guid orderId,
        Guid voucherId)
    {
        AggregateId = orderId;
        CustomerId = customerId;
        OrderId = orderId;
        VoucherId = voucherId;
    }
}