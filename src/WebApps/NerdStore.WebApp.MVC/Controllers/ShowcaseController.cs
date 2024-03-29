using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services.Interfaces;

namespace NerdStore.WebApp.MVC.Controllers;

public class ShowcaseController : Controller
{
    private readonly IProductAppService _productAppService;

    public ShowcaseController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("")]
    [Route("showcase")]
    public async Task<IActionResult> Index()
    {
        return View(await _productAppService.GetAllAsync());
    }

    [HttpGet]
    [Route("product-detail/{id}")]
    public async Task<IActionResult> ProductDetail(Guid id)
    {
        return View(await _productAppService.GetByIdAsync(id));
    }
}