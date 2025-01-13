using Shopping.Contracts.Category;

namespace Shopping.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse?> GetByIdAsync(int id);
        Task<CategoryResponse?> GetByNameAsync(string name);
        Task<CategoryResponse> CreateAsync(CategoryRequest categoryRequest);
        Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest categoryRequest);
        Task<bool> DeleteAsync(int id);
    }
}
