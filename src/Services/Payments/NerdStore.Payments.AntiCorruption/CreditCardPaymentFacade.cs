using NerdStore.Payments.AntiCorruption.Interfaces;
using NerdStore.Payments.Business.Enums;
using NerdStore.Payments.Business.Interfaces.Facades;
using NerdStore.Payments.Business.Models;

namespace NerdStore.Payments.AntiCorruption;

public class CreditCardPaymentFacade : ICreditCardPaymentFacade
{
    private readonly IPayPalGateway _payPalGateway;
    private readonly IConfigurationManagement _configurationManagement;

    public CreditCardPaymentFacade(
        IPayPalGateway payPalGateway,
        IConfigurationManagement configurationManagement)
    {
        _payPalGateway = payPalGateway;
        _configurationManagement = configurationManagement;
    }

    public Transaction AccomplishPayment(Order order, Payment payment)
    {
        var apiKey = _configurationManagement.GetValue("apiKey");
        var encryptionKey = _configurationManagement.GetValue("encryptionKey");
        var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encryptionKey);
        var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CreditCardNumber);
        var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

        var transaction = new Transaction
        {
            OrderId = order.Id,
            Total = order.Value,
            PaymentId = payment.Id
        };

        if (paymentResult)
        {
            transaction.TransactionStatus = TransactionStatus.Paid;
            return transaction;
        }

        transaction.TransactionStatus = TransactionStatus.Refused;
        return transaction;
    }
}