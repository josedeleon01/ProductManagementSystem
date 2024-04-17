using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Interfaces.Users;

namespace ProductManagementSystem.Client.Layout
{
    public partial class MainLayout
    {
        [Inject] 
        IUserService userService { get; set; }

        private bool collapseNavMenu { get; set; } = false;

        private string NavMenuCssClass { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }
        private void ToggleNavMenu()
        {
            NavMenuCssClass = !collapseNavMenu ? "hide" : string.Empty;
            collapseNavMenu =!collapseNavMenu;

            // Notify Blazor to re-render the component
            StateHasChanged();

        }

        public async Task LogOut()
        {
             await userService.LogoutAsync();
            NavigationManager.NavigateTo("/login");


        }
    }
}
