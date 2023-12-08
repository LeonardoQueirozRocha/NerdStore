using MediatR;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Payments.Business.Interfaces.Services;

namespace NerdStore.Payments.Business.Events;

public class PaymentEventHandler : INotificationHandler<ConfirmedStockOrderIntegrationEvent>
{
    private readonly IPaymentService _paymentService;

    public PaymentEventHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task Handle(ConfirmedStockOrderIntegrationEvent message, CancellationToken cancellationToken)
    {
        var orderPayment = new OrderPayment
        {
            OrderId = message.OrderId,
            CustomerId = message.CustomerId,
            Total = message.Total,
            CreditCardName = message.CreditCardName,
            CreditCardNumber = message.CreditCardNumber,
            CreditCardExpirationDate = message.CreditCardExpirationDate,
            CreditCardCvv = message.CreditCardCvv
        };

        await _paymentService.AccomplishOrderPaymentAsync(orderPayment);
    }
}