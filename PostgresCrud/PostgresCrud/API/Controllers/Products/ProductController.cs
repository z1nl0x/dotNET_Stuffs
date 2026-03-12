using Microsoft.AspNetCore.Mvc;
using PostgresCrud.DTOs;
using PostgresCrud.Services;

namespace PostgresCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(ProductInputModel productDto)
    {
        var createdProduct = await _productService.AddProductAsync(productDto); 
        
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct); 
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ProductInputModel productDto)
    {
        try
        {
            await _productService.UpdateProductAsync(id, productDto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    // Handles HTTP DELETE request to delete a product by ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
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