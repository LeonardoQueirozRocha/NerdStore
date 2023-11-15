using NerdStore.Catalog.Application.DTOs;

namespace NerdStore.Catalog.Application.Services;

public interface IProductAppService : IDisposable
{
    Task<IEnumerable<ProductDTO>> GetAllAsync();
    Task<ProductDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
    Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int code);

    Task AddProductAsync(ProductDTO productDTO);
    Task UpdateProductAsync(ProductDTO productDTO);

    Task<ProductDTO> DebitStockAsync(Guid id, int quantity);
    Task<ProductDTO> ReplenishStockAsync(Guid id, int quantity);
}