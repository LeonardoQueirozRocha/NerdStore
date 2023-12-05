using MediatR;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;

namespace NerdStore.Catalog.Domain.Handlers;

public class ProductEventHandler :
    INotificationHandler<ProductBelowStockEvent>,
    INotificationHandler<StartedOrderIntegrationEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly IStockService _stockService;
    private readonly IMediatorHandler _mediatorHandler;

    public ProductEventHandler(
        IProductRepository productRepository,
        IStockService stockService,
        IMediatorHandler mediatorHandler)
    {
        _productRepository = productRepository;
        _stockService = stockService;
        _mediatorHandler = mediatorHandler;
    }

    public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(message.AggregateId);

        // Send an email for acquisition for more products
    }

    public async Task Handle(StartedOrderIntegrationEvent message, CancellationToken cancellationToken)
    {
        var result = await _stockService.DebitStockAsync(message.OrderProducts);

        if (result)
        {
            await _mediatorHandler.PublishEventAsync(new ConfirmedStockOrderIntegrationEvent(
                message.OrderId,
                message.CustomerId,
                message.Total,
                message.OrderProducts,
                message.CreditCardName,
                message.CreditCardNumber,
                message.CreditCardExpirationDate,
                message.CreditCardCvv));
        }
        else
        {
            await _mediatorHandler.PublishEventAsync(new RejectedStockOrderIntegrationEvent(
                message.OrderId, 
                message.CustomerId));
        }
    }
}