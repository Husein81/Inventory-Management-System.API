using Application.Features.OrderItems.Commands;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Requests;
using Application.Features.Categories.Commands;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductRequest>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();

            CreateMap<CategoryRequest, Category>();
            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();
            CreateMap<SupplierRequest, Supplier>();
            CreateMap<CustomerRequest, Customer>();

        }
    }
}
