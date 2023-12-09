using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class CancelOrderProcessingRestockStockCommand : Command
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public CancelOrderProcessingRestockStockCommand(Guid orderId, Guid customerId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
    }
}