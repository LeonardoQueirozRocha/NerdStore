using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Payments.Data.Extensions;

public static class MediatorExtension
{
    public static async Task PublishEventsAsync(this IMediatorHandler mediator, PaymentContext context)
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.Notifications)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
                await mediator.PublishEventAsync(domainEvent));

        await Task.WhenAll(tasks);
    }
}