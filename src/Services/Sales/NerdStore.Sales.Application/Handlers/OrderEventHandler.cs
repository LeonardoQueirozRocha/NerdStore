using MediatR;
using NerdStore.Sales.Application.Events;

namespace NerdStore.Sales.Application.Handlers;

public class OrderEventHandler :
    INotificationHandler<OrderDraftStartedEvent>,
    INotificationHandler<UpdatedOrderEvent>,
    INotificationHandler<OrderItemAddedEvent>
{
    public Task Handle(OrderDraftStartedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(UpdatedOrderEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}