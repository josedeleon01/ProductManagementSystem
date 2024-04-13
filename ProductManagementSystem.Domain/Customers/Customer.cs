using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Domain.Customers
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Customer Number must be greater than 0.")]
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
