using PostgresCrud.DTOs;

namespace PostgresCrud.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
    Task<ProductResponse> GetProductByIdAsync(Guid id);
    Task<ProductResponse> AddProductAsync(ProductRequest productDto);
    Task UpdateProductAsync(Guid id, ProductRequest productDto);
    Task DeleteProductAsync(Guid id);
}