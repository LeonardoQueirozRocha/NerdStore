using EventStore.ClientAPI;

namespace EventSourcing.Interfaces;

public interface IEventStoreService
{
    IEventStoreConnection GetConnection();    
}