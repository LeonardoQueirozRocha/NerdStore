using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Application.Handlers;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Application.Queries.Interfaces;
using NerdStore.Sales.Data;
using NerdStore.Sales.Data.Repositories;
using NerdStore.Sales.Domain.Interfaces.Repositories;

namespace NerdStore.Sales.Application.Configurations;

public static class SalesDependencyInjectionConfiguration
{
    public static void AddSalesDependencies(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<SalesContext>();
        services.AddScoped<IOrderQueries, OrderQueries>();

        services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<FinalizeOrderCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<CancelOrderProcessingCommand, bool>, OrderCommandHandler>();
        services.AddScoped<IRequestHandler<CancelOrderProcessingRestockStockCommand, bool>, OrderCommandHandler>();

        services.AddScoped<INotificationHandler<OrderDraftStartedEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<RejectedStockOrderIntegrationEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<AccomplishedPaymentIntegrationEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<RefusedPaymentIntegrationEvent>, OrderEventHandler>();

        services.AddScoped<INotificationHandler<UpdatedOrderEvent>, OrderEventHandler>();
        services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
    }
}