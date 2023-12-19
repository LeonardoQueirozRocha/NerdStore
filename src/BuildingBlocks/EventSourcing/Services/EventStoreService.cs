using EventSourcing.Extensions;
using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;

namespace EventSourcing.Services;

public class EventStoreService : IEventStoreService
{
    private readonly IEventStoreConnection _connection;

    public EventStoreService(IConfiguration configuration)
    {
        _connection = EventStoreConnection.Create(configuration.GetEventStoreConnection());
        _connection.ConnectAsync();
    }

    public IEventStoreConnection GetConnection()
    {
        return _connection;
    }
}