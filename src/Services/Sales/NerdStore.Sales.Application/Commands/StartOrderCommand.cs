using NerdStore.Core.Messages;
using NerdStore.Sales.Application.Commands.Validators;

namespace NerdStore.Sales.Application.Commands;

public class StartOrderCommand : Command
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }
    public decimal Total { get; private set; }
    public string CreditCardName { get; private set; }
    public string CreditCardNumber { get; private set; }
    public string CreditCardExpirationDate { get; private set; }
    public string CreditCardCvv { get; private set; }

    public StartOrderCommand(
        Guid orderId, 
        Guid customerId, 
        decimal total, 
        string creditCardName, 
        string creditCardNumber, 
        string creditCardExpirationDate, 
        string creditCardCvv)
    {
        OrderId = orderId;
        CustomerId = customerId;
        Total = total;
        CreditCardName = creditCardName;
        CreditCardNumber = creditCardNumber;
        CreditCardExpirationDate = creditCardExpirationDate;
        CreditCardCvv = creditCardCvv;
    }

    public override bool IsValid()
    {
        ValidationResult = new StartOrderValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
