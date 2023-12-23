using NerdStore.Sales.Domain.Models;

namespace NerdStore.Sales.Application.Queries.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime RegistrationDate { get; set; }
    public int OrderStatus { get; set; }

    public OrderViewModel() { }

    public OrderViewModel(Order order)
    {
        Id = order.Id;
        TotalValue = order.TotalValue;
        OrderStatus = (int)order.OrderStatus;
        Code = order.Code;
        RegistrationDate = order.RegistrationDate;
    }

    public bool IsOrderPaid => OrderStatus == 4;

    public bool IsOrderCanceled => OrderStatus == 6;

}
