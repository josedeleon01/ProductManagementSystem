namespace ProductManagementSystem.Client.Services.ComponentState
{
    public class NavMenuStateService
    {
        public event EventHandler<NavMenuEventArgs> OnToggleNavMenu;

        public bool CollapseNavMenu { get; private set; } = false;

        public void ToggleNavMenu()
        {
            CollapseNavMenu = !CollapseNavMenu;
            OnToggleNavMenu?.Invoke(this, new NavMenuEventArgs(CollapseNavMenu));

        }
    }

    public class NavMenuEventArgs : EventArgs
    {
        public bool IsCollapsed { get; }

        public NavMenuEventArgs(bool isCollapsed)
        {
            IsCollapsed = isCollapsed;
        }
    }
}
