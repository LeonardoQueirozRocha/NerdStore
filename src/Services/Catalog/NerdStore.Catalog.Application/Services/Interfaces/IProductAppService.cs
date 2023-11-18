using NerdStore.Catalog.Application.ViewModels;

namespace NerdStore.Catalog.Application.Services;

public interface IProductAppService : IDisposable
{
    Task<IEnumerable<ProductViewModel>> GetAllAsync();
    Task<ProductViewModel> GetByIdAsync(Guid id);
    Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
    Task<IEnumerable<ProductViewModel>> GetByCategoryAsync(int code);

    Task AddProductAsync(ProductViewModel productDTO);
    Task UpdateProductAsync(ProductViewModel productDTO);

    Task<ProductViewModel> DebitStockAsync(Guid id, int quantity);
    Task<ProductViewModel> ReplenishStockAsync(Guid id, int quantity);
}