using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus;

public class MediatrHandler : IMediatrHandler
{
    private readonly IMediator _mediator;

    public MediatrHandler(IMediator mediator)
        => _mediator = mediator;

    public async Task PublishEventAsync<T>(T message) where T : Event
        => await _mediator.Publish(message);
}
