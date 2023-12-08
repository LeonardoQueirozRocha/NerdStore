namespace NerdStore.Core.DomainObjects.DTOs;

public class OrderPayment
{
    public Guid OrderId { get; set; }   
    public Guid CustomerId { get; set; }
    public decimal Total { get; set; }
    public string CreditCardName { get; set; }
    public string CreditCardNumber { get; set; }
    public string CreditCardExpirationDate { get; set; }
    public string CreditCardCvv { get; set; }
}