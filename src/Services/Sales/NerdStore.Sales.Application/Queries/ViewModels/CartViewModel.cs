using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Queries.ViewModels;

public class CartViewModel
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal TotalValue { get; set; }
    public decimal DiscountValue { get; set; }
    public string VoucherCode { get; set; }
    public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    public PaymentCartViewModel Payment { get; set; }

    public CartViewModel(Order order)
    {
        CustomerId = order.CustomerId;
        TotalValue = order.TotalValue;
        OrderId = order.Id;
        DiscountValue = order.Discount;
        SubTotal = order.Discount + order.TotalValue;
    }
}