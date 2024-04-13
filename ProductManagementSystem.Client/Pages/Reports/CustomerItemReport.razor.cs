using Microsoft.AspNetCore.Components;
using ProductManagementSystem.Client.Interfaces.CustomerItems;
using ProductManagementSystem.Shared.Dtos.CustomerItem;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Client.Pages.Reports
{
    public partial class CustomerItemReport
    {
        [Inject]
        private ICustomerItemService CustomerItemService { get; set; }

        private CustomerItemListReport CustomerItemListReport { get; set; } = new();

        private async Task LoadReport()
        {
            CustomerItemListReport.Customers = (List<Domain.CustomerItems.CustomerItem>)await CustomerItemService
                .GetAllByItemRangeAsync(CustomerItemListReport.ItemNumberFrom, CustomerItemListReport.ItemNumberTo);
        }
    }
}
