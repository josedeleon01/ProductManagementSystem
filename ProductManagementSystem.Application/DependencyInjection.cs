using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementSystem.Application.Mappings;

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
