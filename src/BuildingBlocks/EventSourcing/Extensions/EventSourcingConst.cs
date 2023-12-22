namespace EventSourcing.Extensions;

public static class EventSourcingConst
{
    public const int PositionToStartReadingFrom = 0;
    public const int CountToReadFromThePosition = 500;
    public const bool ResolveLinkToEventsAutomatically = false;
}