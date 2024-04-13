using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Interfaces.CustomerItems;

namespace ProductManagementSystem.Client.Pages.Reports
{
    public partial class CustomerItemReport
    {
        [Inject]
        private ICustomerItemService CustomerItemService { get; set; }

        private List<Domain.CustomerItems.CustomerItem> _customers = [];
        private int ItemNumberFrom { get; set; }

        private int ItemNumberTo { get; set; }

        private async Task LoadReport()
        {
            _customers = (List<Domain.CustomerItems.CustomerItem>)await CustomerItemService
                .GetAllByItemRangeAsync(ItemNumberFrom, ItemNumberTo);
        }
    }
}
