using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Handlers;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalog.Application.Configuration;

public static class CatalogDependencyInjectionConfiguration
{
    public static void AddCatalogDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<CatalogContext>();

        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        services.AddScoped<INotificationHandler<StartedOrderIntegrationEvent>, ProductEventHandler>();
        services.AddScoped<INotificationHandler<CancelOrderProcessingIntegrationEvent>, ProductEventHandler>();
    }
}