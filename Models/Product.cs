using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
        [NotMapped] 
        public string CategoryName => Category.Name;
        public Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; } = [];
    }
}
