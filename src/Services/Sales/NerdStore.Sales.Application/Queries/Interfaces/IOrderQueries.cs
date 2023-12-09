using NerdStore.Sales.Application.Queries.ViewModels;

namespace NerdStore.Sales.Application.Queries.Interfaces;

public interface IOrderQueries
{
    Task<CartViewModel> GetCustomerCartAsync(Guid customerId);
    Task<IEnumerable<OrderViewModel>> GetCustomerOrdersAsync(Guid customerId);
}