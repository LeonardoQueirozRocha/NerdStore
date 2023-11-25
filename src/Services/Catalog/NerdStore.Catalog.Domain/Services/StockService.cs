using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Core.Bus;

namespace NerdStore.Catalog.Domain.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;
    private readonly IMediatorHandler _bus;

    public StockService(
        IProductRepository productRepository,
        IMediatorHandler bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task<bool> DebitStockAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null || !product.HasStock(quantity)) return false;

        product.DebitStock(quantity);

        if (product.QuantityInStock < 10)
            await _bus.PublishEventAsync(new ProductBelowStockEvent(product.Id, product.QuantityInStock));

        _productRepository.Update(product);

        return await _productRepository.UnitOfWork.Commit();
    }

    public async Task<bool> ReplenishStockAsync(Guid productId, int quantity)
    {
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null) return false;

        product.ReplenishStock(quantity);

        _productRepository.Update(product);

        return await _productRepository.UnitOfWork.Commit();
    }

    public void Dispose() => _productRepository.Dispose();
}
