using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Application.Dtos;
using ProductManagementSystem.Client.Interfaces.Users;

namespace ProductManagementSystem.Client.Pages.Login
{
    public partial class Login
    {
        [Inject]
        public IUserService AuthService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private AuthenticateRequest model = new();
        private string errorMessage;

        private async Task SubmitAsync()
        {
            try
            {
                await AuthService.Authenticate(model);
                NavigationManager.NavigateTo("/reports/customeritems");
                errorMessage = null;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
