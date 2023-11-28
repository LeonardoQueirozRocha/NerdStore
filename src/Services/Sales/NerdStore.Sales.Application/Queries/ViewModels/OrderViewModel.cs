using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Queries.ViewModels;

public class OrderViewModel
{
    public int Code { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int OrderStatus { get; set; }

    public OrderViewModel(Order order)
    {
        TotalValue = order.TotalValue;
        OrderStatus = (int)order.OrderStatus;
        Code = order.Code;
        RegistrationDate = order.RegistrationDate;
    }
}
