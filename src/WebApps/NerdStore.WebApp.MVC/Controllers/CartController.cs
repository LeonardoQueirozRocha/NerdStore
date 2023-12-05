using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Application.Queries.ViewModels;
using NerdStore.WebApp.MVC.Controllers.Base;

namespace NerdStore.WebApp.MVC.Controllers;

public class CartController : MainController
{
    private readonly IProductAppService _productAppService;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IOrderQueries _orderQueries;

    public CartController(
        IProductAppService productAppService,
        IMediatorHandler mediatorHandler,
        INotificationHandler<DomainNotification> notificationHandler,
        IOrderQueries orderQueries) : base(
            notificationHandler,
            mediatorHandler)
    {
        _productAppService = productAppService;
        _mediatorHandler = mediatorHandler;
        _orderQueries = orderQueries;
    }

    [Route("my-cart")]
    public async Task<IActionResult> Index()
    {
        return View(await _orderQueries.GetCustomerCartAsync(CustomerId));
    }

    [HttpPost("my-cart")]
    public async Task<IActionResult> AddItem(Guid id, int quantity)
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

        if (IsValid())
            return RedirectToAction("Index");

        TempData["Errors"] = GetErrorMessages();
        return RedirectToAction("ProductDetail", "Showcase", new { id });
    }

    [HttpPost("remove-item")]
    public async Task<IActionResult> RemoveItem(Guid id)
    {
        var product = await _productAppService.GetByIdAsync(id);

        if (product is null) return BadRequest();

        var command = new RemoveOrderItemCommand(CustomerId, id);

        await _mediatorHandler.SendCommandAsync(command);

        if (IsValid())
            return RedirectToAction("Index");

        return View("Index", await _orderQueries.GetCustomerCartAsync(CustomerId));
    }

    [HttpPost("update-item")]
    public async Task<IActionResult> UpdateItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetByIdAsync(id);

        if (product is null) return BadRequest();

        var command = new UpdateOrderItemCommand(
            CustomerId,
            product.Id,
            quantity);

        await _mediatorHandler.SendCommandAsync(command);

        if (IsValid())
            return RedirectToAction("Index");

        return View("Index", await _orderQueries.GetCustomerCartAsync(CustomerId));
    }

    [HttpPost("apply-voucher")]
    public async Task<IActionResult> ApplyVoucher(string voucherCode)
    {
        var command = new ApplyVoucherOrderCommand(CustomerId, voucherCode);

        await _mediatorHandler.SendCommandAsync(command);

        if (IsValid())
            return RedirectToAction("Index");

        return View("Index", await _orderQueries.GetCustomerCartAsync(CustomerId));
    }

    [HttpGet("purchase-summary")]
    public async Task<IActionResult> PurchaseSummary()
    {
        return View(await _orderQueries.GetCustomerCartAsync(CustomerId));
    }

    [HttpPost("start-order")]
    public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
    {
        var cart = await _orderQueries.GetCustomerCartAsync(CustomerId);
        var command = new StartOrderCommand(
            cart.OrderId,
            CustomerId,
            cart.TotalValue,
            cartViewModel.Payment.CreditCardName,
            cartViewModel.Payment.CreditCardNumber,
            cartViewModel.Payment.CreditCardExpirationDate,
            cartViewModel.Payment.CreditCardCvv);

        await _mediatorHandler.SendCommandAsync(command);

        if (IsValid()) return RedirectToAction("Index", "Order");

        return View("OrderSummary", await _orderQueries.GetCustomerCartAsync(CustomerId));
    }
}