using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Application.Categories;
using ProductManagementSystem.Application.Customers;
using ProductManagementSystem.Application.CustomerItem;
using ProductManagementSystem.Application.Items;
using ProductManagementSystem.Domain.Users;
using ProductManagementSystem.Infrastructure.Helpers.Tokens;
using ProductManagementSystem.Infrastructure.Persistance;

namespace ProductManagementSystem.API
{
    /// <summary>
    /// Provides extension methods to configure dependency injection for API services.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers API-related services in the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services are registered.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining method calls.</returns>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddTransient<AppDbContext>();
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<JwtTokenGenerator>();
            services.AddIdentityCore<AppUser>();;
            services.AddScoped<CategoryHandler>();
            services.AddScoped<CustomerHandler>();
            services.AddScoped<ItemHandler>();
            services.AddScoped<CustomerItemHandler>();
            services.AddIdentityCore<AppUser>
                (options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
