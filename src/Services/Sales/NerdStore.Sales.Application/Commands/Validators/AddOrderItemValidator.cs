using FluentValidation;

namespace NerdStore.Sales.Application.Commands.Validators;

public class AddOrderItemValidator : AbstractValidator<AddOrderItemCommand>
{
    private const int MinimumQuantity = 0;
    private const int MaximumQuantity = 15;
    private const int MinimumValue = 0;

    public AddOrderItemValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

        RuleFor(c => c.Quantity)
            .GreaterThan(MinimumQuantity)
                .WithMessage("A quantidade mínima de um item é 1");

        RuleFor(c => c.Quantity)
            .LessThan(MaximumQuantity)
                .WithMessage("A quantidade máxima de um item é 15");

        RuleFor(c => c.UnitValue)
            .GreaterThan(MinimumValue)
                .WithMessage("O valor do item precisa ser maior que 0");
    }
}