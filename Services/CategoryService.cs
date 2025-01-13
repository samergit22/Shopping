using Mapster;
using Microsoft.EntityFrameworkCore;
using Shopping.Contracts.Category;
using Shopping.Data;
using Shopping.IServices;
using Shopping.Models;

namespace Shopping.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ShoppingData _data;
        public CategoryService(ShoppingData data)
        {
            _data = data;
        }
        public async Task<CategoryResponse> CreateAsync(CategoryRequest categoryRequest)
        {
            var category = categoryRequest.Adapt<Category>();
            _data.Categories.Add(category);
            await _data.SaveChangesAsync();

            return category.Adapt<CategoryResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _data.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _data.Categories.Remove(category);
            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories = await _data.Categories.ToListAsync();
            return categories.Adapt<IEnumerable<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var category = await _data.Categories.FindAsync(id);
            return category?.Adapt<CategoryResponse>();
        }

        public async Task<CategoryResponse?> GetByNameAsync(string name)
        {
            var category = await _data.Categories.FirstOrDefaultAsync(c => c.Name == name);
            return category?.Adapt<CategoryResponse>();
        }

        public async Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest categoryRequest)
        {
            var existingCategory = await _data.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return null;
            }

            categoryRequest.Adapt(existingCategory);
            await _data.SaveChangesAsync();

            return existingCategory.Adapt<CategoryResponse>();
        }
    }
}
