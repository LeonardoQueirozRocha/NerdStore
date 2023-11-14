using NerdStore.Catalog.Domain.Models;
using NerdStore.Core.Data;

namespace NerdStore.Catalog.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetByCategoryAsync(int code);
    Task<IEnumerable<Category>> GetCategoriesAsync();

    void Add(Product product);
    void Update(Product product);

    void Add(Category category);
    void Update(Category category);
}