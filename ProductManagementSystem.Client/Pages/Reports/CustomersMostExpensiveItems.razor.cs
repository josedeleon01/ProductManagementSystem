using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Interfaces.CustomerItems;
namespace ProductManagementSystem.Client.Pages.Reports
{
    public partial class CustomersMostExpensiveItems
    {
        [Inject]
        private ICustomerItemService CustomerItemService { get; set; }

        private List<List<Domain.CustomerItems.CustomerItem>> CustomerItemGroup { get; set; }

        private int top { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CustomerItemGroup = await CustomerItemService
                .GetTopExpensiveItems(3);
        }
    }
}
