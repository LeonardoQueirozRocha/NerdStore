using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communication.Mediator;

public interface IMediatorHandler
{
    Task PublishEventAsync<T>(T message) where T : Event;
    Task<bool> SendCommandAsync<T>(T command) where T : Command;
    Task PublishNotificationAsync<T>(T notification) where T : DomainNotification;
}