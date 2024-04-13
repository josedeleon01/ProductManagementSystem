using System.ComponentModel.DataAnnotations;
using ProductManagementSystem.Domain.Categories;

namespace ProductManagementSystem.Domain.Item
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Item Number must be greater than 0.")]
        public int ItemNumber { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = new Category();

    }
}
