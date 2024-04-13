using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystem.Infrastructure.Persistance.Categories;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Infrastructure.Persistance.Customers;
using ProductManagementSystem.Infrastructure.Persistance.Items;
using ProductManagementSystem.Infrastructure.Persistance.CustomerItem;

namespace ProductManagementSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICustomerItemRepository, CustomerItemRepository>();


            return services;
        }
    }

}
