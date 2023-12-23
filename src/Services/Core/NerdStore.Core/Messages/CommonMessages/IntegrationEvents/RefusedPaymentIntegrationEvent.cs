using NerdStore.Core.Messages.CommonMessages.IntegrationEvents.Base;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

public class RefusedPaymentIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid PaymentId { get; private set; }
    public Guid TransactionId { get; private set; }
    public decimal Total { get; private set; }

    public RefusedPaymentIntegrationEvent(
        Guid orderId,
        Guid customerId,
        Guid paymentId,
        Guid transactionId,
        decimal total)
    {
        AggregateId = orderId;
        OrderId = orderId;
        CustomerId = customerId;
        PaymentId = paymentId;
        TransactionId = transactionId;
        Total = total;
    }
}