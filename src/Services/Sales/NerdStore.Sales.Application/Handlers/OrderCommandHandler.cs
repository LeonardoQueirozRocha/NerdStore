using FluentValidation.Results;
using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Extensions;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain.Interfaces.Repositories;
using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Handlers;

public class OrderCommandHandler :
    IRequestHandler<AddOrderItemCommand, bool>,
    IRequestHandler<UpdateOrderItemCommand, bool>,
    IRequestHandler<RemoveOrderItemCommand, bool>,
    IRequestHandler<ApplyVoucherOrderCommand, bool>,
    IRequestHandler<StartOrderCommand, bool>,
    IRequestHandler<FinalizeOrderCommand, bool>,
    IRequestHandler<CancelOrderProcessingRestockStockCommand, bool>,
    IRequestHandler<CancelOrderProcessingCommand, bool>
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
            order = CreateOrderDraft(
                message.CustomerId,
                message.ProductId,
                orderItem);
        }
        else
        {
            UpdateOrder(
                order,
                orderItem);
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

    public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await GetOrderDraftAsync(message.CustomerId);

        if (order is null) return false;

        var orderItem = await GetOrderItemAsync(order, message.ProductId);

        if (orderItem is null) return false;

        order.UpdateUnits(orderItem, message.Quantity);

        order.AddEvent(new ProductOrderUpdatedEvent(
            order.CustomerId,
            order.Id,
            message.ProductId,
            message.Quantity));

        _orderRepository.UpdateItem(orderItem);
        _orderRepository.Update(order);

        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await GetOrderDraftAsync(message.CustomerId);

        if (order is null) return false;

        var orderItem = await GetOrderItemAsync(order, message.ProductId);

        if (orderItem is null) return false;

        order.RemoveItem(orderItem);

        order.AddEvent(new ProductOrderRemovedEvent(
            message.CustomerId,
            order.Id,
            message.ProductId));

        _orderRepository.RemoveItem(orderItem);
        _orderRepository.Update(order);

        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await GetOrderDraftAsync(message.CustomerId);

        if (order is null) return false;

        var voucher = await GetVoucherAsync(message.VoucherCode);

        if (voucher is null) return false;

        var applicableVoucherValidator = order.ApplyVoucher(voucher);

        if (!applicableVoucherValidator.IsValid)
        {
            PublishErrorsNotificationsAsync(applicableVoucherValidator.Errors);
            return false;
        }

        order.AddEvent(new OrderVoucherApplied(
            message.CustomerId,
            order.Id,
            voucher.Id));

        _orderRepository.Update(order);

        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message)) return false;

        var order = await _orderRepository.GetOrderDraftByCustomerIdAsync(message.CustomerId);

        order.StartOrder();

        var orderProductsList = InsertOrderProductList(order);

        order.AddEvent(new StartedOrderIntegrationEvent(
            order.Id,
            order.CustomerId,
            order.TotalValue,
            orderProductsList,
            message.CreditCardName,
            message.CreditCardNumber,
            message.CreditCardExpirationDate,
            message.CreditCardCvv));

        _orderRepository.Update(order);
        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(FinalizeOrderCommand message, CancellationToken cancellationToken)
    {
        var order = await GetOrderAsync(message.OrderId);

        if (order is null) return false;

        order.FinalizeOrder();
        order.AddEvent(new FinalizeOrderEvent(message.OrderId));
        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(CancelOrderProcessingRestockStockCommand message, CancellationToken cancellationToken)
    {
        var order = await GetOrderAsync(message.OrderId);

        if (order is null) return false;

        var orderProductsList = InsertOrderProductList(order);

        order.AddEvent(new CancelOrderProcessingIntegrationEvent(
            order.Id,
            order.CustomerId,
            orderProductsList));

        order.MakeDraft();
        return await _orderRepository.UnitOfWork.Commit();
    }

    public async Task<bool> Handle(CancelOrderProcessingCommand message, CancellationToken cancellationToken)
    {
        var order = await GetOrderAsync(message.OrderId);

        if (order is null) return false;

        order.MakeDraft();

        return await _orderRepository.UnitOfWork.Commit();
    }

    private Order CreateOrderDraft(
        Guid customerId,
        Guid productId,
        OrderItem item)
    {
        var order = Order.OrderFactory.NewOrderDraft(customerId);
        order.AddItem(item);
        _orderRepository.Add(order);
        order.AddEvent(new OrderDraftStartedEvent(customerId, productId));

        return order;
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

    private bool ValidateCommand(Command message)
    {
        if (message.IsValid()) return true;

        PublishErrorsNotificationsAsync(message.ValidationResult.Errors);

        return false;
    }

    private void PublishErrorsNotificationsAsync(List<ValidationFailure> errors) =>
        errors.ForEach(async error =>
            await _mediatorHandler.PublishNotificationAsync(
                new DomainNotification(error.ErrorCode, error.ErrorMessage)));

    private async Task<Order> GetOrderDraftAsync(Guid customerId)
    {
        var order = await _orderRepository.GetOrderDraftByCustomerIdAsync(customerId);

        if (order is null)
        {
            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
            return null;
        }

        return order;
    }

    private async Task<Order> GetOrderAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
        {
            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Pedido não encontrado!"));
            return null;
        }

        return order;
    }

    private async Task<OrderItem> GetOrderItemAsync(Order order, Guid productId)
    {
        var orderItem = await _orderRepository.GetItemByOrderAsync(order.Id, productId);

        if (!order.ExistOrderItem(orderItem))
        {
            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Item do pedido não encontrado"));
            return null;
        }

        return orderItem;
    }

    private async Task<Voucher> GetVoucherAsync(string voucherCode)
    {
        var voucher = await _orderRepository.GetVoucherByCodeAsync(voucherCode);

        if (voucher is null)
        {
            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("order", "Voucher não encontrado!"));
            return null;
        }

        return voucher;
    }

    private static OrderProductsList InsertOrderProductList(Order order)
    {
        var items = new List<Item>();
        order.OrderItems.ForEach(i => items.Add(new Item
        {
            Id = i.ProductId,
            Quantity = i.Quantity
        }));

        var orderProductsList = new OrderProductsList
        {
            OrderId = order.Id,
            Items = items
        };

        return orderProductsList;
    }
}