using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagementSystem.Client.Components.Common;
using ProductManagementSystem.Client.Interfaces.Categories;
using ProductManagementSystem.Client.Interfaces.Items;
using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Shared.Dtos.Items;

namespace ProductManagementSystem.Client.Pages.Items
{
	public partial class Items : ComponentBase
    {
        [Inject]
        private IItemService ItemService { get; set; }

        [Inject]
        private ICategoryService Categoryervice { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private List<Item> _items = [];

        private List<Category> _categories = [];

        private Item item = new();

        private bool _loading;

        protected override async Task OnInitializedAsync()
        {
            _items = (List<Item>) await ItemService.GetAllAsync();
            _categories = (List<Category>) await Categoryervice.GetAllAsync();
        }
        private async Task Save()
        {
            if (item.Id == 0)
                await ItemService.AddAsync(new ItemCreateRequest
                {
                    ItemNumber = item.ItemNumber, 
                    Description = item.Description, 
                    CategoryId = item.CategoryId,
                    IsActive = item.IsActive,
                    Price = item.Price
                });
            else
                await ItemService.UpdateAsync(new ItemUpdateRequest
                {
                    Id = item.Id,
                    ItemNumber = item.ItemNumber,
                    Description = item.Description,
                    CategoryId = item.CategoryId,
                    IsActive = item.IsActive,
                    Price = item.Price
                });

            item = new Item();
            Snackbar.Add("Item Save Successfully", Severity.Success);
            _items = (List<Item>)await ItemService.GetAllAsync();
        }
        private async Task Edit(int id)
        {
            item = await ItemService.GetByIdAsync(id);
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
                await ItemService.RemoveAsync(id);
                Snackbar.Add("Item Deleted Successfully", Severity.Success);
                _items = (List<Item>)await ItemService.GetAllAsync();
            }
        }

        private void Clear() { item = new(); }
    }
}
