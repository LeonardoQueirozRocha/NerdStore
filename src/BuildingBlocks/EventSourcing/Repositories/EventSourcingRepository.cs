using System.Text;
using EventSourcing.Extensions;
using EventSourcing.Interfaces;
using EventStore.ClientAPI;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing.Repositories;

public class EventSourcingRepository : IEventSourcingRepository
{
    private readonly IEventStoreService _eventStoreService;

    public EventSourcingRepository(IEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task SaveEventAsync<TEvent>(TEvent evt) where TEvent : Event
    {
        await _eventStoreService
            .GetConnection()
            .AppendToStreamAsync(
                stream: evt.AggregateId.ToString(),
                expectedVersion: ExpectedVersion.Any,
                events: FormatEvent(evt));
    }

    public async Task<IEnumerable<StoredEvent>> GetEventsAsync(Guid aggregateId)
    {
        var storedEvents = await _eventStoreService
            .GetConnection()
            .ReadStreamEventsForwardAsync(
                stream: aggregateId.ToString(),
                start: EventSourcingConst.PositionToStartReadingFrom,
                count: EventSourcingConst.CountToReadFromThePosition,
                resolveLinkTos: EventSourcingConst.ResolveLinkToEventsAutomatically);

        return GetStoredEvents(storedEvents);
    }

    private static IEnumerable<StoredEvent> GetStoredEvents(StreamEventsSlice storedEvents)
    {
        var events = new List<StoredEvent>();

        foreach (var resolvedEvent in storedEvents.Events)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);
            var storedEvent = new StoredEvent(
                id: resolvedEvent.Event.EventId,
                type: resolvedEvent.Event.EventType,
                occurrenceDate: jsonData.Timestamp,
                data: dataEncoded
            );

            events.Add(storedEvent);
        }

        return events.OrderBy(e => e.OccurrenceDate);
    }

    private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent evt) where TEvent : Event
    {
        yield return new EventData(
            eventId: Guid.NewGuid(),
            type: evt.MessageType,
            isJson: true,
            data: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(evt)),
            metadata: null
        );
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}