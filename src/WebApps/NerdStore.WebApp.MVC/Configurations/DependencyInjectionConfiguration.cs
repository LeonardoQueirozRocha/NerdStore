using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Events.Handlers;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Commands.Handlers;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Application.Events.Handlers;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Data;
using NerdStore.Sales.Data.Repositories;
using NerdStore.Sales.Domain.Interfaces.Repositories;

namespace NerdStore.WebApp.MVC.Configurations;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencies(this IServiceCollection services)
    {
        // Mediator
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        // Notifications
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

        // Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        services.AddScoped<CatalogContext>();

        // Sales
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<SalesContext>();

        services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();

        services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<UpdatedOrderEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
    }
}