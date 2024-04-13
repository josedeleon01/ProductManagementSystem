using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagementSystem.Client.Components.Common;
using ProductManagementSystem.Client.Interfaces.CustomerItems;
using ProductManagementSystem.Client.Interfaces.Customers;
using ProductManagementSystem.Client.Interfaces.Items;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Shared.Dtos.CustomerItem;

namespace ProductManagementSystem.Client.Pages.CustomerItem
{
    public partial class CustomerItem : ComponentBase
    {
        [Inject]
        private ICustomerItemService CustomerItemService { get; set; }

        [Inject]
        private ICustomerService CustomerService { get; set; }

        [Inject]
        private IItemService ItemService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private List<Domain.CustomerItems.CustomerItem> _customerItems = [];

        private List<Customer> _customers = [];

        private List<Domain.Item.Item> _items = [];

        private Domain.CustomerItems.CustomerItem customerItem = new();

        private Customer customer = new();

        private bool _loading;

        private readonly ModelFluentValidator CustomerItemValidator = new();

        MudForm form;

        protected override async Task OnInitializedAsync()
        {
            _customers = (List<Customer>)await CustomerService.GetAllAsync();
            _items = (List<Domain.Item.Item>)await ItemService.GetAllAsync();
        }
        private async Task Save()
        {
            await form.Validate();

            if (!form.IsValid)
            {
                return;
            }
            //First check if combination of ItemId cuatomerid exist then add or update
            var AssociationExist = await CustomerItemService.GetByIdAsync(customer.Id, customerItem.ItemId);

            if (AssociationExist == null)
                await CustomerItemService.AddAsync(new CustomerItemCreateRequest
                {
                    CustomerId = customer.Id, 
                    ItemId = customerItem.ItemId, 
                    Quantity = customerItem.Quantity, 
                    Active = customerItem.Active, 
                    Price = customerItem.Price,
                });
            else
                await CustomerItemService.UpdateAsync(new CustomerItemUpdateRequest
                {
                    CustomerId = customer.Id,
                    ItemId = customerItem.ItemId,
                    Quantity = customerItem.Quantity,
                    Active = customerItem.Active,
                    Price = customerItem.Price
                });

            
            Snackbar.Add("CustomerItem Save Successfully", Severity.Success);
            var customerItems = (List<Domain.CustomerItems.CustomerItem>)await CustomerItemService.GetAllAsync();
            _customerItems = customerItems.Where(item => item.CustomerId == customer.Id).ToList();

            customerItem = new Domain.CustomerItems.CustomerItem();
        }

        private async Task CustomerValueChanged(string selectedItem)
        {
            int customerId = Convert.ToInt32(selectedItem);

            ////Get the CustomerItems by the selected Customer id to fill the table
            var customerItems = await CustomerItemService.GetAllAsync();
            _customerItems = customerItems.Where(item => item.CustomerId == customerId).ToList();

            customer = _customers.FirstOrDefault(customer => customer.Id == customerId);
        }

        private async Task Edit(int customerId, int itemId)
        {
            customerItem = await CustomerItemService.GetByIdAsync(customerId, itemId);

        }

        private async Task Delete(int cusomerId, int itemId)
        {
            var parameters = new DialogParameters<ConfirmDelete>
            {
                { x => x.ContentText, "Do you really want to delete this record? This process cannot be undone." },
                { x => x.ButtonText, "Delete" },
                { x => x.Color, Color.Error }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            var dialog = DialogService.Show<ConfirmDelete>("Delete", parameters, options);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await CustomerItemService.RemoveAsync(cusomerId, itemId);
                Snackbar.Add("Customer Deleted Successfully", Severity.Success);
                var customerItems = await CustomerItemService.GetAllAsync();
                _customerItems = customerItems.Where(item => item.CustomerId == cusomerId).ToList();
            }
        }

        private void Clear() { customerItem = new(); }
    }
}
