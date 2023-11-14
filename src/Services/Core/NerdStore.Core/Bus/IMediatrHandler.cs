using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public interface IMediatrHandler
{
    Task PublishEventAsync<T>(T message) where T : Event;
}