using Application.Requests;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository
{
    public interface IOrderRepository
    {
        Task<Response<PagedList<OrderDto>>> GetOrders(int page, int pageSize, string searchTerm);
        Task<Response<OrderDto>> GetOrder(Guid id);
        Task<Response<Order>> CreateOrder(Order request);
        Task<Response<Order>> UpdateOrder(Guid Id,Order request);
        Task<Response<Unit>> DeleteOrder(Guid id);
        Task<Response<Order>> UpdateOrderStatus(Guid id, Order order);
        Task<Response<Order>> UpdateOrderPayment(Guid id, Order order);
        Task<Response<PagedList<OrderDto>>> GetCompletedOrders(int page, int pageSize, string searchTerm);
    }
}
