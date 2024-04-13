using AutoMapper;
using ProductManagementSystem.Shared.Dtos.Categories;
using ProductManagementSystem.Shared.Dtos.Customers;
using ProductManagementSystem.Shared.Dtos.Items;
using ProductManagementSystem.Domain.Categories;
using ProductManagementSystem.Domain.Customers;
using ProductManagementSystem.Domain.Item;
using ProductManagementSystem.Shared.Dtos.CustomerItem;

namespace ProductManagementSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerCreateRequest, Customer>();
            CreateMap<CustomerUpdateRequest, Customer>();
            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();
            CreateMap<ItemCreateRequest, Item>();
            CreateMap<ItemUpdateRequest, Item>();
            CreateMap<CustomerItemCreateRequest, Domain.CustomerItems.CustomerItem>();
            CreateMap<CustomerItemUpdateRequest, Domain.CustomerItems.CustomerItem>();

        }
    }
}
