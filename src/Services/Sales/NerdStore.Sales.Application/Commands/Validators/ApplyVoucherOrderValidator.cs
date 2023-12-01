using FluentValidation;

namespace NerdStore.Sales.Application.Commands.Validators;

public class ApplyVoucherOrderValidator : AbstractValidator<ApplyVoucherOrderCommand>
{
    public ApplyVoucherOrderValidator()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

        RuleFor(c => c.VoucherCode)
            .NotEmpty()
                .WithMessage("O código do voucher não pode ser vazio");
    }
}