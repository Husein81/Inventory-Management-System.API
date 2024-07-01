using Application.Features.Products.Commands;
using AutoMapper;
using Domain;
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
        }
    }
}
