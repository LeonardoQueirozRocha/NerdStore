using MediatR;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Core.Communication.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public MediatorHandler(
        IMediator mediator,
        IEventSourcingRepository eventSourcingRepository)
    {
        _mediator = mediator;
        _eventSourcingRepository = eventSourcingRepository;
    }

    public async Task PublishEventAsync<T>(T message) where T : Event
    {
        await _mediator.Publish(message);
        await _eventSourcingRepository.SaveEventAsync(message);
    }

    public async Task PublishDomainEventAsync<T>(T message) where T : DomainEvent
    {
        await _mediator.Publish(message);
    }

    public async Task<bool> SendCommandAsync<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task PublishNotificationAsync<T>(T notification) where T : DomainNotification
    {
        await _mediator.Publish(notification);
    }
}
