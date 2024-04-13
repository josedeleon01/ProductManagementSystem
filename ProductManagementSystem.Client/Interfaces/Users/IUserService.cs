using ProductManagementSystem.Application.Dtos;
using ProductManagementSystem.Domain.Users;

namespace ProductManagementSystem.Client.Interfaces.Users
{
    public interface IUserService
    {
        Task<DateTime> Authenticate(AuthenticateRequest authenticateRequest);
        Task<string> GetJwtAsync();
        Task LogoutAsync();
        Task<bool> RefreshAsync();
        event Action<string?>? LoginChange;
        string GetUsername(string token);
        Task<bool> IsAuthenticated();
    }
}
