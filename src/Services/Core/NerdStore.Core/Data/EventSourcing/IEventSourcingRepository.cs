using NerdStore.Core.Messages;

namespace NerdStore.Core.Data.EventSourcing;

public interface IEventSourcingRepository
{
    Task SaveEventAsync<TEvent>(TEvent evt) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEventsAsync(Guid aggregateId);
}