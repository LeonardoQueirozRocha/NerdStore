using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class OrderItemAddedEvent : Event
{
    public Guid CustomerId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal UnitValue { get; private set; }
    public int Quantity { get; private set; }

    public OrderItemAddedEvent(
        Guid customerId,
        Guid orderId,
        string productName,
        Guid productId,
        decimal unitValue,
        int quantity)
    {
        AggregateId = orderId;
        CustomerId = customerId;
        OrderId = orderId;
        ProductId = productId;
        UnitValue = unitValue;
        Quantity = quantity;
        ProductName = productName;
    }
}
