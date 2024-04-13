using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.Items
{
    public class ItemUpdateRequest
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "ItemNumber must be greater than 0.")]
        public int ItemNumber { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
