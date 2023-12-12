using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Sales.Domain.Models.Enums;
using NerdStore.Sales.Domain.Validators;

namespace NerdStore.Sales.Domain.Models;

public class Voucher : Entity
{
    public string Code { get; private set; }
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType VoucherDiscountType { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public DateTime? DateOfUse { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool Active { get; private set; }
    public bool Used { get; private set; }

    // EF Relation
    public ICollection<Order> Orders { get; set; }

    internal ValidationResult ValidateIfApplicable() => 
        new ApplicableVoucherValidator().Validate(this);
}
