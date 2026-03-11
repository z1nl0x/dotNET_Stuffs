using Microsoft.AspNetCore.Mvc;
using PostgresCrud.DTOs;
using PostgresCrud.Services;

namespace PostgresCrud.Controllers;

[ApiController] // Specifies that this is an API controller
[Route("api/[controller]")] // Defines the route as 'api/product'
public class ProductController : ControllerBase
{
    private readonly IProductService _productService; // Service instance for business logic
    
    public ProductController(IProductService productService)
    {
        _productService = productService; // Injecting the service via constructor
    }
    
    // Handles HTTP GET request to fetch all products
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync(); // Calls service to get all products
        return Ok(products); // Returns 200 OK response with product data
    }
    
    // Handles HTTP GET request to fetch a single product by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id); // Calls service to fetch product by ID
            return Ok(product); // Returns 200 OK response if found
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); // Returns 404 Not Found if product does not exist
        }
    }
    
    // Handles HTTP POST request to add a new product
    [HttpPost]
    public async Task<IActionResult> Add(ProductRequestDto productDto)
    {
        await _productService.AddProductAsync(productDto); // Calls service to add a new product
        return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto); 
        // Returns 201 Created response with location header pointing to the new product
    }
    
    // Handles HTTP PUT request to update an existing product
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductRequestDto productDto)
    {
        try
        {
            await _productService.UpdateProductAsync(id, productDto); // Calls service to update product
            return NoContent(); // Returns 204 No Content response on success
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); // Returns 404 Not Found if product does not exist
        }
    }
    
    // Handles HTTP DELETE request to delete a product by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id); // Calls service to delete product
            return NoContent(); // Returns 204 No Content response on success
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); // Returns 404 Not Found if product does not exist
        }
    }
}