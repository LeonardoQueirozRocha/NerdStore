using AutoMapper;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain.Interfaces.Repositories;
using NerdStore.Catalog.Domain.Interfaces.Services;
using NerdStore.Catalog.Domain.Models;
using NerdStore.Core.DomainObjects.Exceptions;

namespace NerdStore.Catalog.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly IStockService _stockService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductAppService(
        IStockService stockService,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _stockService = stockService;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllAsync() =>
        _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAllAsync());

    public async Task<ProductViewModel> GetByIdAsync(Guid id) => 
        _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));

    public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync() => 
        _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetCategoriesAsync());

    public async Task<IEnumerable<ProductViewModel>> GetByCategoryAsync(int code) => 
        _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategoryAsync(code));

    public async Task AddProductAsync(ProductViewModel productDTO)
    {
        _productRepository.Add(_mapper.Map<Product>(productDTO));
        await _productRepository.UnitOfWork.Commit();
    }

    public async Task UpdateProductAsync(ProductViewModel productDTO)
    {
        _productRepository.Update(_mapper.Map<Product>(productDTO));
        await _productRepository.UnitOfWork.Commit();
    }

    public async Task<ProductViewModel> DebitStockAsync(Guid id, int quantity)
    {
        if (!await _stockService.DebitStockAsync(id, quantity))
            throw new DomainException("Falha ao debitar estoque");

        return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
    }

    public async Task<ProductViewModel> ReplenishStockAsync(Guid id, int quantity)
    {
        if (!await _stockService.ReplenishStockAsync(id, quantity))
            throw new DomainException("Falha ao repor estoque");

        return _mapper.Map<ProductViewModel>(await _productRepository.GetByIdAsync(id));
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
        _stockService?.Dispose();
    }
}