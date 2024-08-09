using Application.Repository;
using Application.Requests;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public OrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<Order>> CreateOrder(Order request)
        {
    
            await _context.Orders.AddAsync(request);

            var result = await _context.SaveChangesAsync() > 0;
            
            return result ? Response<Order>.Success(request) 
                : Response<Order>.Fail("Failed to create order");
        }

        public async Task<Response<Unit>> DeleteOrder(Guid Id)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == Id);
            if (order is null)
            {
                return Response<Unit>.Fail($"Order with id:{Id} not found");
            }

            _context.Remove(order);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value) 
                : Response<Unit>.Fail("Failed to delete order");
        }

        public async Task<Response<OrderDto>> GetOrder(Guid Id)
        {
            var order = await _context.Orders
                           .Include(x => x.Customer)
                           .Include(x => x.OrderItems)
                           .ThenInclude(x => x.Product)
                           .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                           .FirstOrDefaultAsync(x => x.Id == Id);
            if (order is null)
            {
                return Response<OrderDto>.Fail($"Order with id:{Id} not found");
            }

            return Response<OrderDto>.Success(order);
        }

        public async Task<Response<PagedList<OrderDto>>> GetOrders(int page, int pageSize, string searchTerm)
        {
            var orders = _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ThenInclude(o => o.Product)
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {   
                searchTerm = searchTerm.ToLower();
                orders = orders.Where(x => x.Customer.Name.ToLower().Contains(searchTerm));
            }
            return Response<PagedList<OrderDto>>.Success(
                await PagedList<OrderDto>.ToPagedList(orders, page, pageSize));
        }

        public async Task<Response<Order>> UpdateOrder(Guid Id, Order request)
        {
             
            var order = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ThenInclude(o => o.Product)
                .FirstOrDefaultAsync(x => x.Id == Id);

            if (order is null)
            {
                return Response<Order>.Fail($"Order with id:{Id} not found");
            }

            order.Update(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Order>.Success(order) 
                : Response<Order>.Fail("Failed to update order");
        }

        public async Task<Response<Order>> UpdateOrderStatus(Guid id, Order status)
        {
            var order = await _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
            {
                return Response<Order>.Fail($"Order with id:{id} not found");
            }

            order.OrderStatus = status.OrderStatus;
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Order>.Success(order) 
                : Response<Order>.Fail("Failed to update order status");
        }

        public async Task<Response<Order>> UpdateOrderPayment(Guid id, Order payment)
        {
            var order = await _context.Orders
              .Include(x => x.Customer)
              .Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == id);
            if (order is null)
            {
                return Response<Order>.Fail($"Order with id:{id} not found");
            }

            order.Payment = payment.Payment;
            order.UpdatedAt = DateTime.Now;

            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Order>.Success(order)
                : Response<Order>.Fail("Failed to update order status");
        }
        public async Task<Response<PagedList<OrderDto>>> GetCompletedOrders(int page, int pageSize, string searchTerm)
        {
            var orders = _context.Orders
                .Where(x => x.OrderStatus.Contains("completed"))
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ThenInclude(o => o.Product)
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .AsQueryable();


            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                orders = orders.Where(x => x.Customer.Name.ToLower().Contains(searchTerm));
            }
            return Response<PagedList<OrderDto>>.Success(
                await PagedList<OrderDto>.ToPagedList(orders, page, pageSize));
        }
    }
}
