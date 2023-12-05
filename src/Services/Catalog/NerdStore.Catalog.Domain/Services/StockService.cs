using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Catalog.Domain.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;
    private readonly IMediatorHandler _mediatorHandler;

    public StockService(
        IProductRepository productRepository,
        IMediatorHandler mediatorHandler)
    {
        _productRepository = productRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> DebitStockAsync(Guid productId, int quantity)
    {
        if (!await DebitStockAsync(productId, quantity)) return false;

        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> DebitStockAsync(OrderProductsList orderProductsList)
    {
        foreach (var item in orderProductsList.Items)
            if (!await DebitStockItemAsync(item.Id, item.Quantity))
                return false;

        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReplenishStockAsync(Guid productId, int quantity)
    {
        if (!await ReplenishStockItemAsync(productId, quantity)) return false;

        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReplenishStockAsync(OrderProductsList orderProductsList)
    {
        foreach (var item in orderProductsList.Items)
            await ReplenishStockItemAsync(item.Id, item.Quantity);

        return await _productRepository.UnitOfWork.Commit();
    }

    private async Task<bool> DebitStockItemAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null) return false;

        if (!product.HasStock(quantity))
        {
            await _mediatorHandler.PublishNotificationAsync(new DomainNotification("Stock", $"Produto - {product.Name} sem estoque"));
            return false;
        }

        product.DebitStock(quantity);

        if (product.QuantityInStock < 10)
            await _mediatorHandler.PublishEventAsync(new ProductBelowStockEvent(product.Id, product.QuantityInStock));

        _productRepository.Update(product);
        return true;
    }

    private async Task<bool> ReplenishStockItemAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null) return false;

        product.ReplenishStock(quantity);

        _productRepository.Update(product);

        return true;
    }

    public void Dispose() => _productRepository.Dispose();
}
