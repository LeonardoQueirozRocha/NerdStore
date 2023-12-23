using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.WebApp.MVC.Controllers.Base;

namespace NerdStore.WebApp.MVC.Controllers;

public class EventsController : MainController
{
    private readonly IEventSourcingRepository _eventSourcingRepository;

    public EventsController(
        INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler,
        IEventSourcingRepository eventSourcingRepository) : base(notificationHandler, mediatorHandler)
    {
        _eventSourcingRepository = eventSourcingRepository;
    }

    public async Task<IActionResult> Index(Guid id)
    {
        var events = await _eventSourcingRepository.GetEventsAsync(id);
        return View(events);
    }
}