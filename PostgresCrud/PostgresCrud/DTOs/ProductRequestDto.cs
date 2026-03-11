namespace PostgresCrud.DTOs;

public class ProductRequestDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Price { get; set; }
}