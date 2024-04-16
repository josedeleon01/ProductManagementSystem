using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Services.ComponentState;

namespace ProductManagementSystem.Client.Layout
{
    public partial class MainLayout
    {
        private bool collapseNavMenu { get; set; } = false;
        private string NavMenuCssClass { get; set; } 
        private void ToggleNavMenu()
        {
            NavMenuCssClass = !collapseNavMenu ? "hide" : string.Empty;
            collapseNavMenu =!collapseNavMenu;

            // Notify Blazor to re-render the component
            StateHasChanged();

        }
    }
}
