using Application.Features.OrderItems.Commands;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;
using Shared.Requests;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductRequest>().ReverseMap();

            CreateMap<CreateProductCommand, Product>();

            CreateMap<CategoryRequest, Category>();

            CreateMap<OrderItem, CreateOrderItemCommand>().ReverseMap();
        }
    }
}
