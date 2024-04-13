using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagementSystem.Client.Components.Common;
using ProductManagementSystem.Client.Interfaces.Customers;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Shared.Dtos.Customers;

namespace ProductManagementSystem.Client.Pages.Customers
{
    public partial class Customers : ComponentBase
    {
        [Inject]
        private ICustomerService CustomerService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private List<Customer> _customers = [];

        private Customer customer = new();

        private bool _loading;

        protected override async Task OnInitializedAsync()
        {
            _customers = (List<Customer>)await CustomerService.GetAllAsync();
        }
        private async Task Save()
        {
            if (customer.Id == 0)
                await CustomerService.AddAsync(new CustomerCreateRequest
                {
                    CustomerNumber = customer.CustomerNumber,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Active = customer.Active
                });
            else
                await CustomerService.UpdateAsync(new CustomerUpdateRequest
                {
                    Id = customer.Id,
                    CustomerNumber = customer.CustomerNumber,
                    Name = customer.Name,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Active = customer.Active
                });

            customer = new Customer();
            Snackbar.Add("Customer Save Successfully", Severity.Success);
            _customers = (List<Domain.Customers.Customer>)await CustomerService.GetAllAsync();
        }
        private async Task Edit(int id)
        {
            customer = await CustomerService.GetByIdAsync(id);
        }

        private async Task Delete(int id)
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
                await CustomerService.RemoveAsync(id);
                Snackbar.Add("Customer Deleted Successfully", Severity.Success);
                _customers = (List<Domain.Customers.Customer>)await CustomerService.GetAllAsync();
            }
        }
        private void Clear() { customer = new(); }
    }
}
