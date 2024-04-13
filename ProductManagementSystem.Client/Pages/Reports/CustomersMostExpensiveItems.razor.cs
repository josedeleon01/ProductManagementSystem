using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Interfaces.CustomerItems;
using ProductManagementSystem.Shared.Dtos.CustomerItem;
namespace ProductManagementSystem.Client.Pages.Reports
{
    public partial class CustomersMostExpensiveItems
    {
        [Inject]
        private ICustomerItemService CustomerItemService { get; set; }

        private CustomersMostExpensiveItemsReport CustomerItemGroup { get; set; } = new();

        private async Task LoadReport()
        {
            CustomerItemGroup.CustomerItemGroup = await CustomerItemService
                .GetTopExpensiveItems(CustomerItemGroup.Top);

        }
    }
}
