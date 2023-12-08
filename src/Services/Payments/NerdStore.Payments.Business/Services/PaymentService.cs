using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Payments.Business.Interfaces.Facades;
using NerdStore.Payments.Business.Interfaces.Repositories;
using NerdStore.Payments.Business.Interfaces.Services;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.Business.Services;

public class PaymentService : IPaymentService
{
    private readonly ICreditCardPaymentFacade _creditCardPaymentFacade;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public PaymentService(
        ICreditCardPaymentFacade creditCardPaymentFacade,
        IPaymentRepository paymentRepository,
        IMediatorHandler mediatorHandler)
    {
        _creditCardPaymentFacade = creditCardPaymentFacade;
        _paymentRepository = paymentRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<Transaction> AccomplishOrderPaymentAsync(OrderPayment orderPayment)
    {
        var (order, payment) = CreateOrderPayment(orderPayment);
        var transaction = _creditCardPaymentFacade.AccomplishPayment(order, payment);

        if (transaction.IsTransactionPaid())
            return await AccomplishPaidTransactionAsync(payment, orderPayment, transaction, order);

        return await AccomplishRefusedTransactionAsync(order, orderPayment, transaction);
    }

    private static (Order order, Payment payment) CreateOrderPayment(OrderPayment orderPayment)
    {
        var order = new Order
        {
            Id = orderPayment.OrderId,
            Value = orderPayment.Total
        };

        var payment = new Payment
        {
            Value = orderPayment.Total,
            CreditCardName = orderPayment.CreditCardName,
            CreditCardNumber = orderPayment.CreditCardNumber,
            CreditCardExpirationDate = orderPayment.CreditCardExpirationDate,
            CreditCardCvv = orderPayment.CreditCardCvv,
            OrderId = orderPayment.OrderId
        };

        return (order, payment);
    }

    private async Task<Transaction> AccomplishPaidTransactionAsync(
        Payment payment,
        OrderPayment orderPayment,
        Transaction transaction,
        Order order)
    {
        payment.AddEvent(new AccomplishedPaymentIntegrationEvent(
            order.Id,
            orderPayment.CustomerId,
            transaction.PaymentId,
            transaction.Id,
            order.Value));

        _paymentRepository.AddPayment(payment);
        _paymentRepository.AddTransaction(transaction);

        await _paymentRepository.UnitOfWork.Commit();

        return transaction;
    }

    private async Task<Transaction> AccomplishRefusedTransactionAsync(
        Order order,
        OrderPayment orderPayment,
        Transaction transaction)
    {
        await _mediatorHandler.PublishNotificationAsync(new DomainNotification("payment", "A operadora recusou o pagamento"));
        await _mediatorHandler.PublishEventAsync(new RefusedPaymentIntegrationEvent(
            order.Id,
            orderPayment.CustomerId,
            transaction.PaymentId,
            transaction.Id,
            order.Value));

        return transaction;
    }
}