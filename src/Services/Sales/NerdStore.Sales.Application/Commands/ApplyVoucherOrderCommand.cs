using NerdStore.Core.Messages;
using NerdStore.Sales.Application.Commands.Validators;

namespace NerdStore.Sales.Application.Commands;

public class ApplyVoucherOrderCommand : Command
{
    public Guid CustomerId { get; private set; }
    public string VoucherCode { get; private set; }

    public ApplyVoucherOrderCommand(
        Guid customerId,
        string voucherCode)
    {
        CustomerId = customerId;
        VoucherCode = voucherCode;
    }

    public override bool IsValid()
    {
        ValidationResult = new ApplyVoucherOrderValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}