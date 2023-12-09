using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Payments.AntiCorruption;
using NerdStore.Payments.AntiCorruption.ConfigurationManagements;
using NerdStore.Payments.AntiCorruption.ExternalServices;
using NerdStore.Payments.AntiCorruption.Interfaces;
using NerdStore.Payments.Business.Events;
using NerdStore.Payments.Business.Interfaces.Facades;
using NerdStore.Payments.Business.Interfaces.Repositories;
using NerdStore.Payments.Business.Interfaces.Services;
using NerdStore.Payments.Business.Services;
using NerdStore.Payments.Data.Repositories;

namespace NerdStore.Payments.Data.Configurations;

public static class PaymentDependencyConfiguration
{
    public static void AddPaymentDependencies(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
        services.AddScoped<IPayPalGateway, PayPalGateway>();
        services.AddScoped<IConfigurationManagement, ConfigurationManagement>();
        services.AddScoped<PaymentContext>();

        services.AddScoped<INotificationHandler<ConfirmedStockOrderIntegrationEvent>, PaymentEventHandler>();
    }
}