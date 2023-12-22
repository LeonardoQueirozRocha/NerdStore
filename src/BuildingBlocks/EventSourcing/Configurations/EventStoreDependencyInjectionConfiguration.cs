using EventSourcing.Interfaces;
using EventSourcing.Repositories;
using EventSourcing.Services;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Core.Data.EventSourcing;

namespace EventSourcing.Configurations;

public static class EventStoreDependencyInjectionConfiguration
{
    public static void AddEventStoreDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IEventStoreService, EventStoreService>();
        services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
    }
}