using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Commands;

public class CancelOrderProcessingCommand : Command
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public CancelOrderProcessingCommand(
        Guid orderId,
        Guid customerId)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
    }
}