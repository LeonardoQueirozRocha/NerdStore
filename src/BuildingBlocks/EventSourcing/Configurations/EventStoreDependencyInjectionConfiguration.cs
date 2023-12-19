using EventSourcing.Interfaces;
using EventSourcing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcing.Configurations;

public static class EventStoreDependencyInjectionConfiguration
{
    public static void AddEventStoreDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IEventStoreService, EventStoreService>();
    }
}