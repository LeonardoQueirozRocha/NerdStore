using FluentValidation;
using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Validators;

public class ApplicableVoucherValidator : AbstractValidator<Voucher>
{
    private const int MinimumQuantity = 0;

    public ApplicableVoucherValidator()
    {
        RuleFor(c => c.ExpirationDate)
            .Must(ExpirationDateHigherThanCurrentDate)
                .WithMessage("Este voucher está expirado.");

        RuleFor(c => c.Active)
            .Equal(true)
                .WithMessage("Este voucher não é mais válido.");

        RuleFor(c => c.Used)
            .Equal(false)
                .WithMessage("Este voucher já foi utilizado.");

        RuleFor(c => c.Quantity)
            .GreaterThan(MinimumQuantity)
                .WithMessage("Este voucher não está mais disponível");

    }

    private static bool ExpirationDateHigherThanCurrentDate(DateTime expirationDate) =>
        expirationDate >= DateTime.Now;
}