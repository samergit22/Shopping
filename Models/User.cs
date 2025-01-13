using Microsoft.AspNetCore.Identity;

namespace Shopping.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalID { get; set; } = String.Empty;
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; } = "customer";
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public List<RefreshTokens> RefreshTokens { get; set; } = [];
        public Cart Cart { get; set; } = new Cart();
        public ICollection<Order> Orders { get; set; } = [];
        public ICollection<Wishlist> Wishlists { get; set; } = [];
    }
}
