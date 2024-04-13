using FluentValidation;

namespace ProductManagementSystem.Client.Components.Common
{
    public class ModelFluentValidator : AbstractValidator<Domain.CustomerItems.CustomerItem>
    {
        public ModelFluentValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.ItemId)
                .NotNull()
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .NotNull()
                .GreaterThan(0)
                .NotEmpty();

            RuleFor(x => x.ItemId)
                .NotNull()
                .GreaterThan(0);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Domain.CustomerItems.CustomerItem>.CreateWithOptions((Domain.CustomerItems.CustomerItem)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };

    }
}
