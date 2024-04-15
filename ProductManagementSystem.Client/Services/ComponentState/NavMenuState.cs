namespace ProductManagementSystem.Client.Services.ComponentState
{
    public class NavMenuState
    {
        public event Action OnToggleNavMenu;
        
        public bool CollapseNavMenu { get; private set; } = true;

        public void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
            OnToggleNavMenu?.Invoke();
        
        }
    }
}
