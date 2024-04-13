using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.CustomerItem
{
    public class CustomerItemUpdateRequest
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
