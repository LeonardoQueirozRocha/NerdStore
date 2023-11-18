using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Events.Handlers;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Bus;

namespace NerdStore.WebApp.MVC.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMediatrHandler, MediatrHandler>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<CatalogContext>();
        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
    }
}