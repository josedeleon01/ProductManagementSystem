using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystem.Application.Categories;
using ProductManagementSystem.Application.Customers;
using ProductManagementSystem.Application.Interfaces;
using ProductManagementSystem.Application.Items;
using ProductManagementSystem.Application.Mappings;
using ProductManagementSystem.Domain.Users;
using System;

namespace ProductManagementSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(x => {
                x.AddProfile(new MappingProfile());

            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
        
    }
}
