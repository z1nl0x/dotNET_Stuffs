using PostgresCrud.DTOs;
using PostgresCrud.Entities;
using PostgresCrud.Repositories;

namespace PostgresCrud.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository; // Repository instance for database operations
    
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository; // Injecting the repository via constructor
    }
    
    // Retrieves all products, converts them to DTOs, and returns the list
    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync(); // Fetch all products from repository

        // Convert each product entity into a ProductResponseDto and return the list
        return products.Select(p => new ProductResponseDto 
        { 
            Id = p.Id, 
            Name = p.Name, 
            Price = p.Price 
        });
    }
    
    // Retrieves a product by ID and converts it to a DTO
    public async Task<ProductResponseDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id); // Fetch product by ID

        // If the product is not found, throw an exception
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        // Convert entity to DTO and return it
        return new ProductResponseDto 
        { 
            Id = product.Id, 
            Name = product.Name, 
            Price = product.Price 
        };
    }
    
    // Adds a new product using a request DTO
    public async Task AddProductAsync(ProductRequestDto productDto)
    {
        // Convert DTO to entity
        var product = new Product()
        {
            Name = productDto.Name,
            Price = productDto.Price
        };

        // Add the new product to the database
        await _productRepository.AddAsync(product);
    }
    
    // Updates an existing product with new data
    public async Task UpdateProductAsync(int id, ProductRequestDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id); // Fetch the product by ID

        // If the product does not exist, throw an exception
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        // Update product fields with new values from DTO
        product.Name = productDto.Name;
        product.Price = productDto.Price;

        // Save the updated product in the database
        await _productRepository.UpdateAsync(product);
    }
    
    // Deletes a product by ID
    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id); // Fetch the product by ID

        // If the product does not exist, throw an exception
        if (product == null)
            throw new KeyNotFoundException("Product not found");

        // Delete the product from the database
        await _productRepository.DeleteAsync(id);
    }   
}