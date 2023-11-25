using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator) => 
        _mediator = mediator;

    public async Task PublishEventAsync<T>(T message) where T : Event => 
        await _mediator.Publish(message);

    public async Task<bool> SendCommandAsync<T>(T command) where T : Command => 
        await _mediator.Send(command);
}
