
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Application.Dtos
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
		[EmailAddress]
		public string Password { get; set; }
    }
}
