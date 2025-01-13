namespace Shopping.Contracts.Products
{
    public record ProductResponse(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int Stock,
        string? ImageUrl,
        int CategoryId,
        string? CategoryName
        );
 
}
