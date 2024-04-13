
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Application.Dtos
{
    public class AuthenticateResponse
    {
        [Required]
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

		[Required]
		public  string RefreshToken { get; set; }
    }
}
