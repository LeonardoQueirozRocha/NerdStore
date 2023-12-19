using Microsoft.Extensions.Configuration;

namespace EventSourcing.Extensions;

public static class EventStoreExtension
{
    public static string GetEventStoreConnection(this IConfiguration configuration) => 
        configuration.GetConnectionString("EventStoreConnection");
}