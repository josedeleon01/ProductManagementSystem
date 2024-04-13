using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos.Customers
{
    public class CustomerCreateRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CustomerNumber must be greater than 0.")]
        public int CustomerNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
