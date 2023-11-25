using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.WebApp.MVC.Controllers.Base;

namespace NerdStore.WebApp.MVC.Controllers;

public class CartController : MainController
{
    private readonly IProductAppService _productAppService;
    private readonly IMediatorHandler _mediatorHandler;

    public CartController(
        IProductAppService productAppService,
        IMediatorHandler mediatorHandler,
        INotificationHandler<DomainNotification> notificationHandler) : base(
            notificationHandler,
            mediatorHandler)
    {
        _productAppService = productAppService;
        _mediatorHandler = mediatorHandler;
    }

    [HttpPost("my-cart")]
    public async Task<IActionResult> Additem(Guid id, int quantity)
    {
        var product = await _productAppService.GetByIdAsync(id);
        if (product is null) return BadRequest();

        if (product.QuantityInStock < quantity)
        {
            TempData["Error"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProductDetail", "Showcase", new { id });
        }

        var command = new AddOrderItemCommand(
            CustomerId,
            product.Id,
            product.Name,
            quantity,
            product.Value);

        await _mediatorHandler.SendCommandAsync(command);

        if (IsOperationValid())
            return RedirectToAction("Index");

        TempData["Errors"] = GetErrorMessages();
        return RedirectToAction("ProductDetail", "Showcase", new { id });
    }
}