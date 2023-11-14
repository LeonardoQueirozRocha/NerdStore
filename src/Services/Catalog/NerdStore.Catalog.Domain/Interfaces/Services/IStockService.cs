namespace NerdStore.Catalog.Domain.Interfaces.Services;

public interface IStockService : IDisposable
{
    Task<bool> DebitStockAsync(Guid productId, int quantity);
    Task<bool> ReplenishStockAsync(Guid productId, int quantity);
}