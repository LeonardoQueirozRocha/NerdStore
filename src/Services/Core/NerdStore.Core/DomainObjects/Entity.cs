using NerdStore.Core.Messages;

namespace NerdStore.Core.DomainObjects;

public abstract class Entity
{
    private List<Event> _notifications;

    public Guid Id { get; set; }
    public IReadOnlyCollection<Event> Notifications => _notifications?.AsReadOnly();

    protected Entity() =>
        Id = Guid.NewGuid();

    public void AddEvent(Event eventItem)
    {
        _notifications ??= new List<Event>();
        _notifications.Add(eventItem);
    }

    public void RemoveEvent(Event eventItem) =>
        _notifications?.Remove(eventItem);
    
    public void ClearEvents() =>
        _notifications?.Clear();

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) =>
        !(a == b);

    public override int GetHashCode() =>
        GetType().GetHashCode() * 907 + Id.GetHashCode();

    public override string ToString() =>
        $"{GetType().Name} [Id ={Id}]";

    public virtual bool IsValid()
    {
        throw new NotImplementedException();
    }
}