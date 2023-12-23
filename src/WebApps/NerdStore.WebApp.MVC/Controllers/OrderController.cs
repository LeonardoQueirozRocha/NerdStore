using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Queries.Interfaces;
using NerdStore.WebApp.MVC.Controllers.Base;

namespace NerdStore.WebApp.MVC.Controllers;

public class OrderController : MainController
{
    private readonly IOrderQueries _orderQueries;

    public OrderController(
        IOrderQueries orderQueries,
        INotificationHandler<DomainNotification> notifications,
        IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
    {
        _orderQueries = orderQueries;
    }

    [Route("my-orders")]
    public async Task<IActionResult> Index()
    {
        return View(await _orderQueries.GetCustomerOrdersAsync(CustomerId));
    }
}