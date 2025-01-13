using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Contracts.Category;
using Shopping.IServices;
using Shopping.Services;

namespace Shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Get all categories
        [HttpGet("AllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // Get category by ID
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CategoryResponse>> GetByIdAsync(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // Search category by name
        [HttpGet("Search")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {

            var category = await _categoryService.GetByNameAsync(name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // Create a new category
        [HttpPost("Add")]
        public async Task<ActionResult<CategoryResponse>> CreateAsync(CategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryResponse = await _categoryService.CreateAsync(categoryRequest);

            // Ensure the route to GetByIdAsync is correct in CreatedAtAction
            return CreatedAtAction("GetById", new { id = categoryResponse.Id }, categoryResponse);
        }

        // Update a category
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<CategoryResponse>> UpdateAsync(int id, CategoryRequest categoryRequest)
        {
            var categoryResponse = await _categoryService.UpdateAsync(id, categoryRequest);
            if (categoryResponse == null)
            {
                return NotFound();
            }

            return Ok(categoryResponse);
        }

        // Delete a category
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var success = await _categoryService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
