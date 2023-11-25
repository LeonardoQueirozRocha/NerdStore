using MediatR;
using NerdStore.Sales.Domain.Interfaces.Repositories;
using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Commands.Handlers;

public class OrderCommandHandler : IRequestHandler<AddOrderItemCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
            CreateOrderDraft(message.CustomerId, orderItem);
        }
        else
        {
            CreateOrder(order, orderItem);
        }

        return await _orderRepository.UnitOfWork.Commit();
    }

    private void CreateOrderDraft(Guid customerId, OrderItem item)
    {
        var order = Order.OrderFactory.NewOrderDraft(customerId);
        order.AddItem(item);
        _orderRepository.Add(order);
    }

    private void CreateOrder(Order order, OrderItem item)
    {
        var hasOrderItem = order.ExistOrderItem(item);
        order.AddItem(item);

        if (hasOrderItem)
        {
            _orderRepository.UpdateItem(order.GetItemByProductId(item.ProductId));
        }
        else
        {
            _orderRepository.AddItem(item);
        }
    }

    private static bool ValidateCommand(AddOrderItemCommand message)
    {
        if (message.IsValid()) return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            // throw an error event
        }

        return false;
    }
}