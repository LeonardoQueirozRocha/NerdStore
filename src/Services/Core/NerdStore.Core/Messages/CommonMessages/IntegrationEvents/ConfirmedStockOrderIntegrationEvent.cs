using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents.Base;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class ConfirmedStockOrderIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal Total { get; private set; }
    public OrderProductsList OrderProducts { get; private set; }
    public string CreditCardName { get; private set; }
    public string CreditCardNumber { get; private set; }
    public string CreditCardExpirationDate { get; private set; }
    public string CreditCardCvv { get; private set; }

    public ConfirmedStockOrderIntegrationEvent(
        Guid orderId,
        Guid customerId,
        decimal total,
        OrderProductsList orderProducts,
        string creditCardName,
        string creditCardNumber,
        string creditCardExpirationDate,
        string creditCardCvv)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
        Total = total;
        OrderProducts = orderProducts;
        CreditCardName = creditCardName;
        CreditCardNumber = creditCardNumber;
        CreditCardExpirationDate = creditCardExpirationDate;
        CreditCardCvv = creditCardCvv;
    }
}
