using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Services.ComponentState;

namespace ProductManagementSystem.Client.Layout
{
    public partial class MainLayout
    {
        [Inject]
        NavMenuState NavMenuState { get; set; }

        private void ToggleNavMenu()
        {
            NavMenuState.ToggleNavMenu();

        }
    }
}
