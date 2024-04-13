using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.Categories
{
    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
