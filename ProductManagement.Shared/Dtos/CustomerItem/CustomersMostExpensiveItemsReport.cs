using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.CustomerItem
{
    public class CustomersMostExpensiveItemsReport
    {
        public List<List<Domain.CustomerItems.CustomerItem>> CustomerItemGroup { get; set; } = [];

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Top must be greater than 0.")]
        public int Top { get; set; }
    }
}
