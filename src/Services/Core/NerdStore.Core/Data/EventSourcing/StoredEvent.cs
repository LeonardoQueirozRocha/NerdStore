namespace NerdStore.Core.Data.EventSourcing;

public class StoredEvent
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public DateTime OccurrenceDate { get; set; }
    public string Data { get; private set; }

    public StoredEvent(
        Guid id,
        string type,
        DateTime occurrenceDate,
        string data)
    {
        Id = id;
        Type = type;
        OccurrenceDate = occurrenceDate;
        Data = data;
    }
}