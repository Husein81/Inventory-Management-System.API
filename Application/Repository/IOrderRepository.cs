using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Repository
{
    public interface IOrderRepository
    {
        Task<Response<List<Order>>> GetOrders();
        Task<Response<Order>> GetOrder(Guid id);
        Task<Response<Order>> CreateOrder(Order request);
        Task<Response<Order>> UpdateOrder(Guid Id,Order request);
        Task<Response<Unit>> DeleteOrder(Guid id);
    }
}
