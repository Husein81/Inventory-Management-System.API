using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Order>> CreateOrder(Order request)
        {
            await _context.Orders.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Order>.Success(request) 
                : Response<Order>.Fail("Failed to create order");
        }

        public async Task<Response<Unit>> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
            {
                return Response<Unit>.Fail($"Order with id:{id} not found");
            }

            _context.Remove(order);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value) 
                : Response<Unit>.Fail("Failed to delete order");
        }

        public async Task<Response<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if(order is null)
            {
                return Response<Order>.Fail($"Order with id:{id} not found");
            }

            return Response<Order>.Success(order);
        }

        public async Task<Response<List<Order>>> GetOrders()
            => Response<List<Order>>.Success(await _context.Orders.ToListAsync());

        public async Task<Response<Order>> UpdateOrder(Guid Id, Order request)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            if (order is null)
            {
                return Response<Order>.Fail($"Order with id:{Id} not found");
            }

            order.Update(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Order>.Success(order) 
                : Response<Order>.Fail("Failed to update order");
        }
    }
}
