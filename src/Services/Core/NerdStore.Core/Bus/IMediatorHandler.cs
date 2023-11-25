using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public interface IMediatorHandler
{
    Task PublishEventAsync<T>(T message) where T : Event;
    Task<bool> SendCommandAsync<T>(T command) where T : Command;
}