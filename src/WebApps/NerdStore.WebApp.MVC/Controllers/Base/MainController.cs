using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers.Base;

public abstract class MainController : Controller
{
    protected Guid CustomerId = Guid.Parse("ba241d6c-d26f-4e85-94a8-4b5a887aa49f");
}