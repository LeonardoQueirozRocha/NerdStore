using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Configurations;

public static class CoreDependencyInjectionConfiguration
{
    public static void AddCoreDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
    }
}