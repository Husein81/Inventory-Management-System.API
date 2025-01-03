﻿using Application.Features.OrderItems.Commands;
using Application.Features.Products.Commands;
using AutoMapper;
using Domain.Entities;
using Application.Requests;
using Application.Features.Categories.Commands;
using Application.Features.Orders.Commands;
using Shared;

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
            CreateMap<Customer, CustomerDto>();

            CreateMap<UpdateOrderStatusDto, Order>();
            CreateMap<UpdateOrderPaymentDto, Order>();

            CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
        }
    }
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var mappedItems = context.Mapper.Map<List<TDestination>>(source);
            return new PagedList<TDestination>(mappedItems, source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
