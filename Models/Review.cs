using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = new User();

        public int ProductId { get; set; }
        public Product Product { get; set; } =new Product();

        [Required]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}
