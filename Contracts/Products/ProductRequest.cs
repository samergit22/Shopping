using System.ComponentModel.DataAnnotations;

namespace Shopping.Contracts.Products
{
    public record ProductRequest(

        string Name,
        string? Description,
        decimal Price,
        int Stock,
        IFormFile? ImageUrl,
       int CategoryId
    );
    
}
