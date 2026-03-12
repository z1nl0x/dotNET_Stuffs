using AutoMapper;
using PostgresCrud.DTOs;
using PostgresCrud.Entities;
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

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) throw new KeyNotFoundException("Product not found");

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<ProductViewModel> AddProductAsync(ProductInputModel productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        
        await _productRepository.AddAsync(product);
        
        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task UpdateProductAsync(Guid id, ProductInputModel productDto)
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