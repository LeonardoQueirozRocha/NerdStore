using NerdStore.Core.DomainObjects;
using NerdStore.Payments.Business.Enums;

namespace NerdStore.Payments.Business.Models;

public class Transaction : Entity
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Total { get; set; }
    public TransactionStatus TransactionStatus { get; set; }

    // EF. Rel.
    public Payment Payment { get; set; }

    public bool IsTransactionPaid() => 
        TransactionStatus is TransactionStatus.Paid;
}