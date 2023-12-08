using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Sales.Application.Events;

namespace NerdStore.Sales.Application.Handlers;

public class OrderEventHandler :
    INotificationHandler<OrderDraftStartedEvent>,
    INotificationHandler<UpdatedOrderEvent>,
    INotificationHandler<OrderItemAddedEvent>,
    INotificationHandler<RejectedStockOrderIntegrationEvent>,
    INotificationHandler<RefusedPaymentIntegrationEvent>
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

    public Task Handle(RejectedStockOrderIntegrationEvent notification, CancellationToken cancellationToken)
    {
        // Cancel order process - return error to the customer
        return Task.CompletedTask;
    }

    public Task Handle(RefusedPaymentIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}