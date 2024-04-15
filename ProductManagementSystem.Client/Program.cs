using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductManagementSystem.Client.Interfaces.Customers;
using ProductManagementSystem.Client;
using ProductManagementSystem.Client.Interfaces.Categories;
using ProductManagementSystem.Client.Interfaces.Items;
using ProductManagementSystem.Client.Services.Categories;
using ProductManagementSystem.Client.Services.Customers;
using ProductManagementSystem.Client.Services.Items;
using ProductManagementSystem.Client.Interfaces.Users;
using ProductManagementSystem.Client.Services.Users;
using Blazored.SessionStorage;
using ProductManagementSystem.Client.Handlers;
using MudBlazor.Services;
using ProductManagementSystem.Client.Services.CustomerItems;
using ProductManagementSystem.Client.Interfaces.CustomerItems;
using ProductManagementSystem.Client.Services.ComponentState;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddHttpClient("ServerApi")
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiURL:Url"] ?? ""))
                .AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerItemService, CustomerItemService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<NavMenuState>();
builder.Services.AddBlazoredSessionStorageAsSingleton();
builder.Services.AddMudServices();


await builder.Build().RunAsync();
