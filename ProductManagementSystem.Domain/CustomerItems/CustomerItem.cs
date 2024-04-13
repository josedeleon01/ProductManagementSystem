using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Domain.CustomerItems
{
    public class CustomerItem
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Customer is required.")]
        public int CustomerId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Item is required.")]
        public int ItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        public Item.Item Item { get; set; } = new Item.Item();

        public Customers.Customer Customer { get; set; } = new Customers.Customer();

    }
}
