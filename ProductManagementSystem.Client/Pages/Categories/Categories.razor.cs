using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagementSystem.Client.Components.Common;
using ProductManagementSystem.Client.Interfaces.Categories;
using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Shared.Dtos.Categories;

namespace ProductManagementSystem.Client.Pages.Categories
{
    public partial class Categories : ComponentBase
    {
        [Inject]
        private ICategoryService CategoryService { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private List<Category> _categories = [];

        private Category category = new();

        private bool _loading;

        protected override async Task OnInitializedAsync()
        {
            _categories = (List<Category>)await CategoryService.GetAllAsync();
        }
        private async Task Save()
        {


            if (category.Id == 0)
                await CategoryService.AddAsync(new CategoryCreateRequest { Name = category.Name, Description = category.Description });
            else
                await CategoryService.UpdateAsync(new CategoryUpdateRequest
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });

            category = new Category();
            Snackbar.Add("Category Save Successfully", Severity.Success);
            _categories = (List<Category>)await CategoryService.GetAllAsync();
        }
        private async Task Edit(int id)
        {
            category = await CategoryService.GetByIdAsync(id);
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
                await CategoryService.RemoveAsync(id);
                Snackbar.Add("Category Deleted Successfully", Severity.Success);
                _categories = (List<Category>)await CategoryService.GetAllAsync();
            }
        }

        private void Clear() { category = new(); }
    }
}
