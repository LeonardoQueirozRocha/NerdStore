using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Handlers;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Payments.AntiCorruption;
using NerdStore.Payments.AntiCorruption.ConfigurationManagements;
using NerdStore.Payments.AntiCorruption.ExternalServices;
using NerdStore.Payments.AntiCorruption.Interfaces;
using NerdStore.Payments.Business.Events;
using NerdStore.Payments.Business.Interfaces.Facades;
using NerdStore.Payments.Business.Interfaces.Repositories;
using NerdStore.Payments.Business.Interfaces.Services;
using NerdStore.Payments.Business.Services;
using NerdStore.Payments.Data;
using NerdStore.Payments.Data.Repositories;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Application.Handlers;
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
        services.AddScoped<CatalogContext>();

        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        services.AddScoped<INotificationHandler<StartedOrderIntegrationEvent>, ProductEventHandler>();

        // Sales
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderQueries, OrderQueries>();
        services.AddScoped<SalesContext>();

        services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();

        services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<UpdatedOrderEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<RefusedPaymentIntegrationEvent>, OrderEventHandler>();

        // Payment
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<IConfigurationManagement, ConfigurationManagement>();
        services.AddScoped<PaymentContext>();

        services.AddScoped<INotificationHandler<ConfirmedStockOrderIntegrationEvent>, PaymentEventHandler>();
    }
}