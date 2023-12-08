using NerdStore.Core.DomainObjects;
using NerdStore.Core.DomainObjects.Interfaces;

namespace NerdStore.Payments.Business.Models;

public class Payment : Entity, IAggregateRoot
{
    public Guid OrderId { get; set; }
    public string Status { get; set; }
    public decimal Value { get; set; }

    public string CreditCardName { get; set; }
    public string CreditCardNumber { get; set; }
    public string CreditCardExpirationDate { get; set; }
    public string CreditCardCvv { get; set; }

    // EF. Rel.
    public Transaction Transaction { get; set; }
}