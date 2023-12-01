using NerdStore.Core.Messages;
using NerdStore.Sales.Application.Commands.Validators;

namespace NerdStore.Sales.Application.Commands;

public class UpdateOrderItemCommand : Command
{
    public Guid CustomerId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UpdateOrderItemCommand(
        Guid customerId,
        Guid productId,
        int quantity)
    {
        CustomerId = customerId;
        ProductId = productId;
        Quantity = quantity;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateOrderItemValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}
