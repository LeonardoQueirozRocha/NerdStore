using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.WebApp.MVC.Controllers.Base;

public abstract class MainController : Controller
{
    protected Guid CustomerId = Guid.Parse("ba241d6c-d26f-4e85-94a8-4b5a887aa49f");
    private readonly DomainNotificationHandler _notificationHandler;
    private readonly IMediatorHandler _mediatorHandler;

    protected MainController(
        INotificationHandler<DomainNotification> notificationHandler,
        IMediatorHandler mediatorHandler)
    {
        _notificationHandler = (DomainNotificationHandler)notificationHandler;
        _mediatorHandler = mediatorHandler;
    }

    protected bool IsOperationValid() =>
        !_notificationHandler.HasNotifications();

    protected IEnumerable<string> GetErrorMessages() => 
        _notificationHandler
            .GetNotifications()
            .Select(c => c.Value)
            .ToList();

    protected void NotifyError(string code, string message) =>
        _mediatorHandler.PublishNotificationAsync(new DomainNotification(code, message));
}