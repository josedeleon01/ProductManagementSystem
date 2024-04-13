using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.Categories
{
    public class CategoryUpdateRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
