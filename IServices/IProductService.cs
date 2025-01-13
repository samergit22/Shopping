using Shopping.Contracts.Products;

namespace Shopping.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse?> GetByNameAsync(string name);
        Task<ProductResponse> CreateAsync(ProductRequest productRequest , IFormFile? imageFile);
        Task<ProductResponse?> UpdateAsync(int id, ProductRequest productRequest);
        Task<bool> DeleteAsync(int id);
    }
}
