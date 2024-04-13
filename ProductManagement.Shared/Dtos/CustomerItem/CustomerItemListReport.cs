using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.CustomerItem
{
    public class CustomerItemListReport
    {
        public List<Domain.CustomerItems.CustomerItem> Customers { get; set; } = new();

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Item Number must be greater than 0.")]
        public int ItemNumberFrom { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Item Number must be greater than 0.")]
        public int ItemNumberTo { get; set; }
    }
}
