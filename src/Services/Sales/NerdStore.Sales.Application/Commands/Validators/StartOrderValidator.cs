using FluentValidation;

namespace NerdStore.Sales.Application.Commands.Validators;

public class StartOrderValidator : AbstractValidator<StartOrderCommand>
{
    public StartOrderValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

        RuleFor(c => c.OrderId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");

        RuleFor(c => c.CreditCardName)
            .NotEmpty()
                .WithMessage("O nome do cartão não foi informado");

        RuleFor(c => c.CreditCardNumber)
            .CreditCard()
                .WithMessage("Número do cartão de crédito inválido");

        RuleFor(c => c.CreditCardExpirationDate)
            .NotEmpty()
                .WithMessage("Data de expiração não informada");

        RuleFor(c => c.CreditCardCvv)
            .Length(3, 4)
                .WithMessage("O CVV não foi preenchido corretamente");
    }
}