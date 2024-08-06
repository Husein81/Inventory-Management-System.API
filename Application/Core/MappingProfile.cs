using Application.Features.OrderItems.Commands;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Requests;
using Application.Features.Categories.Commands;
using Application.Features.Orders.Commands;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductRequest>().ReverseMap();
          

            CreateMap<CategoryRequest, Category>();
            CreateMap<SupplierRequest, Supplier>();
            CreateMap<CustomerRequest, Customer>();
            CreateMap<OrderItemRequest, OrderItem>();
          
            CreateMap<OrderRequest, Order>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<Order, CustomerOrderDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Customer, OrderCustomer>();
            CreateMap<Order, OrderDto>();

        }
    }
}
