using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Shared.Dtos
{
    public class AuthenticateRefresh
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
