using FluentValidation;

namespace NerdStore.Sales.Application.Commands.Validators;

public class UpdateOrderItemValidator : AbstractValidator<UpdateOrderItemCommand>
{
    private const int MinimumQuantity = 0;
    private const int MaximumQuantity = 15;

    public UpdateOrderItemValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

        RuleFor(c => c.Quantity)
            .GreaterThan(MinimumQuantity)
                .WithMessage("A quantidade miníma de um item é 1");

        RuleFor(c => c.Quantity)
            .LessThan(MaximumQuantity)
                .WithMessage($"A quantidade máxima de um item é {MaximumQuantity}");
    }
}