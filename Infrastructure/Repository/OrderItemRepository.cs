using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<OrderItem>> CreateOrderItem(OrderItem request)
        {   
            await _context.OrderItems.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<OrderItem>.Success(request)
                : Response<OrderItem>.Fail("Failed to create order item");

        }

        public async Task<Response<Unit>> DeleteOrderItem(Guid id)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
            if (orderItem is null)
            {
                return Response<Unit>.Fail($"Order item with id:{id} not found");
            }
            
            _context.OrderItems.Remove(orderItem);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Unit>.Success(Unit.Value)
                : Response<Unit>.Fail("Failed to delete order item");
        }

        public async Task<Response<OrderItem>> GetOrderItem(Guid id)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
            if (orderItem is null)
            {
                return Response<OrderItem>.Fail($"Order item with id:{id} not found");
            }

            return Response<OrderItem>.Success(orderItem);
        }

        public async Task<Response<List<OrderItem>>> GetOrderItems()
            => Response<List<OrderItem>>.Success(await _context.OrderItems.ToListAsync());

        public async Task<Response<OrderItem>> UpdateOrderItem(Guid Id, OrderItem request)
        {
            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(x => x.Id == Id);
            if (orderItem is null)
            {
                return Response<OrderItem>.Fail($"Order item with id:{Id} not found");
            }
            
            orderItem.Update(request); 
            orderItem.CalculateTotalPrice();

            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<OrderItem>.Success(orderItem)
                : Response<OrderItem>.Fail("Failed to update order item");
            
        }
    }
}
