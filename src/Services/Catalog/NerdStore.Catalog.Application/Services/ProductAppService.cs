using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
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

    public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        => _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetAllAsync());

    public async Task<ProductDTO> GetByIdAsync(Guid id)
        => _mapper.Map<ProductDTO>(await _productRepository.GetByIdAsync(id));

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        => _mapper.Map<IEnumerable<CategoryDTO>>(await _productRepository.GetCategoriesAsync());

    public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int code)
        => _mapper.Map<IEnumerable<ProductDTO>>(await _productRepository.GetByCategoryAsync(code));

    public async Task AddProductAsync(ProductDTO productDTO)
    {
        _productRepository.Add(_mapper.Map<Product>(productDTO));
        await _productRepository.UnitOfWork.Commit();
    }

    public async Task UpdateProductAsync(ProductDTO productDTO)
    {
        _productRepository.Update(_mapper.Map<Product>(productDTO));
        await _productRepository.UnitOfWork.Commit();
    }

    public async Task<ProductDTO> DebitStockAsync(Guid id, int quantity)
    {
        if (!await _stockService.DebitStockAsync(id, quantity))
            throw new DomainException("Falha ao debitar estoque");

        return _mapper.Map<ProductDTO>(await _productRepository.GetByIdAsync(id));
    }

    public async Task<ProductDTO> ReplenishStockAsync(Guid id, int quantity)
    {
        if (!await _stockService.ReplenishStockAsync(id, quantity))
            throw new DomainException("Falha ao repor estoque");

        return _mapper.Map<ProductDTO>(await _productRepository.GetByIdAsync(id));
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
        _stockService?.Dispose();
    }
}