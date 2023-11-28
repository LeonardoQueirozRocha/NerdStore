using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Queries.ViewModels;

public class CartItemViewModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitValue { get; set; }
    public decimal TotalValue { get; set; }

    public CartItemViewModel(OrderItem item)
    {
        ProductId = item.ProductId;
        ProductName = item.ProductName;
        Quantity = item.Quantity;
        UnitValue = item.UnitValue;
        TotalValue = item.UnitValue * item.Quantity;
    }
}
