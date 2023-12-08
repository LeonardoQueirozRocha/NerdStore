namespace NerdStore.Payments.Business.Models;

public class Order
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public List<Product> Products { get; set; }
}
