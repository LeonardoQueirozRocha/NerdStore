using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain.Interfaces.Repositories;
using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Commands.Handlers;

public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public OrderCommandHandler(
        IOrderRepository orderRepository,
        IMediatorHandler mediatorHandler)
    {
        _orderRepository = orderRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await _orderRepository.GetOrderDraftByCustomerIdAsync(message.CustomerId);
        var orderItem = new OrderItem(
            message.ProductId,
            message.Name,
            message.Quantity,
            message.UnitValue);

        if (order is null)
        {
            CreateOrderDraft(message.CustomerId, message.ProductId, orderItem);
        }
        else
        {
            UpdateOrder(order, orderItem);
        }

        order.AddEvent(new OrderItemAddedEvent(
            order.CustomerId,
            order.Id,
            message.Name,
            message.ProductId,
            message.UnitValue,
            message.Quantity));

        return await _orderRepository.UnitOfWork.Commit();
    }

    private void CreateOrderDraft(Guid customerId, Guid productId, OrderItem item)
    {
        var order = Order.OrderFactory.NewOrderDraft(customerId);
        order.AddItem(item);
        _orderRepository.Add(order);
        order.AddEvent(new OrderDraftStartedEvent(customerId, productId));
    }

    private void UpdateOrder(Order order, OrderItem item)
    {
        var hasOrderItem = order.ExistOrderItem(item);
        order.AddItem(item);

        if (hasOrderItem)
            _orderRepository.UpdateItem(order.GetItemByProductId(item.ProductId));
        else
            _orderRepository.AddItem(item);

        order.AddEvent(new UpdatedOrderEvent(order.CustomerId, order.Id, order.TotalValue));
    }

    private bool ValidateCommand(AddOrderItemCommand message)
    {
        if (message.IsValid()) return true;

        message.ValidationResult.Errors.ForEach(async error =>
            await _mediatorHandler.PublishNotificationAsync(
                new DomainNotification(message.MessageType, error.ErrorMessage)));

        return false;
    }
}