using PostgresCrud.DTOs;

namespace PostgresCrud.Services;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    Task<ProductViewModel> GetProductByIdAsync(Guid id);
    Task<ProductViewModel> AddProductAsync(ProductInputModel productDto);
    Task UpdateProductAsync(Guid id, ProductInputModel productDto);
    Task DeleteProductAsync(Guid id);
}