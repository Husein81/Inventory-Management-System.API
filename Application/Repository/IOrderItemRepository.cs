using Domain.Entities;
using MediatR;
using Shared.Response;
namespace Application.Repository
{
    public interface IOrderItemRepository
    {
        Task<Response<List<OrderItem>>> GetOrderItems();
        Task<Response<OrderItem>> GetOrderItem(Guid id);
        Task<Response<OrderItem>> CreateOrderItem(OrderItem request);
        Task<Response<OrderItem>> UpdateOrderItem(Guid Id,OrderItem request);
        Task<Response<Unit>> DeleteOrderItem(Guid id);
    }
}
