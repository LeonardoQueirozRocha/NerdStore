using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;

namespace NerdStore.Sales.Application.Handlers;

public class OrderEventHandler :
    INotificationHandler<OrderDraftStartedEvent>,
    INotificationHandler<UpdatedOrderEvent>,
    INotificationHandler<OrderItemAddedEvent>,
    INotificationHandler<RejectedStockOrderIntegrationEvent>,
    INotificationHandler<AccomplishedPaymentIntegrationEvent>,
    INotificationHandler<RefusedPaymentIntegrationEvent>
{
    private readonly IMediatorHandler _mediatorHandler;

    public OrderEventHandler(IMediatorHandler mediatorHandler)
    {
        _mediatorHandler = mediatorHandler;
    }

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

    public async Task Handle(RejectedStockOrderIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommandAsync(new CancelOrderProcessingCommand(
            message.OrderId,
            message.CustomerId
        ));
    }

    public async Task Handle(AccomplishedPaymentIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommandAsync(new FinalizeOrderCommand(
            message.OrderId, 
            message.CustomerId));
    }

    public async Task Handle(RefusedPaymentIntegrationEvent message, CancellationToken cancellationToken)
    {
        await _mediatorHandler.SendCommandAsync(new CancelOrderProcessingRestockStockCommand(
            message.OrderId, 
            message.CustomerId));
    }
}