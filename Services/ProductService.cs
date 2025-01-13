using Mapster;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.IServices;
using Shopping.Contracts.Products;
using Shopping.Data;
using Shopping.IServices;
using Shopping.Models;

namespace Shopping.Services
{
    public class ProductService : IProductService
    {
        private readonly ShoppingData _data;
        private readonly IFileService _fileService;
        public ProductService(ShoppingData data, IFileService fileService)
        {
            _data = data;
            _fileService = fileService;
        }
        public async Task<ProductResponse> CreateAsync(ProductRequest productRequest , IFormFile? imageFile)
        {
            string? imageUrl = null;
            if (imageFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                imageUrl = await _fileService.SaveFileAsync(imageFile, fileName);
            }


            var product = productRequest.Adapt<Product>();
            product.ImageUrl = imageUrl;
            _data.Products.Add(product);
            await _data.SaveChangesAsync();
            var savedProduct = await _data.Products
                                   .Include(p => p.Category)  // Include the Category to get CategoryName
                                   .FirstOrDefaultAsync(p => p.Id == product.Id);

            return product.Adapt<ProductResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _data.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return false;
            }

            _data.Products.Remove(product);
            await _data.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _data.Products.Include(p => p.Category).ToListAsync();
            return products.Adapt<IEnumerable<ProductResponse>>();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _data.Products
                .Include(p => p.Category) 
                .FirstOrDefaultAsync(p => p.Id == id);

            return product?.Adapt<ProductResponse>();
        }

        public async Task<ProductResponse?> GetByNameAsync(string name)
        {
            var product = await _data.Products.
                Include(p=>p.Category)
                .FirstOrDefaultAsync(p => p.Name == name);
            return product?.Adapt<ProductResponse>();
        }

        public async Task<ProductResponse?> UpdateAsync(int id, ProductRequest productRequest)
        {
            var existingProduct = await _data.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            productRequest.Adapt(existingProduct);
            await _data.SaveChangesAsync();

            return existingProduct.Adapt<ProductResponse>();
        }
    }
}
