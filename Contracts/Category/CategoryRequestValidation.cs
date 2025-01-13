using System.ComponentModel.DataAnnotations;

namespace Shopping.Contracts.Category
{
    public class CategoryRequestValidation
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(250, ErrorMessage = "Description can be up to 250 characters.")]
        public string Description { get; set; }
    }
}
