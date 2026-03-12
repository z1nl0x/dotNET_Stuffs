using AutoMapper;
using PostgresCrud.Domain.Products;
using PostgresCrud.DTOs;
using PostgresCrud.Repositories;

namespace PostgresCrud.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper; 

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) throw new KeyNotFoundException("Product not found");

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> AddProductAsync(ProductRequest productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        
        await _productRepository.AddAsync(product);
        
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task UpdateProductAsync(Guid id, ProductRequest productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) throw new KeyNotFoundException("Product not found");
        
        _mapper.Map(productDto, product);

        await _productRepository.UpdateAsync(product);
    }
    
    public async Task DeleteProductAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
    }
}