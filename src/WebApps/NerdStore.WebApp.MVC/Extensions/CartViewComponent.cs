using Microsoft.AspNetCore.Mvc;
using NerdStore.Sales.Application.Queries.Interfaces;

namespace NerdStore.WebApp.MVC.Extensions;

public class CartViewComponent : ViewComponent
{
    private readonly Guid CustomerId = Guid.Parse("ba241d6c-d26f-4e85-94a8-4b5a887aa49f");
    private readonly IOrderQueries _orderQueries;

    public CartViewComponent(IOrderQueries orderQueries) => 
        _orderQueries = orderQueries;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cart = await _orderQueries.GetCustomerCartAsync(CustomerId);
        var items = cart?.Items.Count ?? 0;
        return View(items);
    }
}