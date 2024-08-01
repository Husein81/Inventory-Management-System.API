
using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Orders.Queries
{
    public record GetOrdersQuery : IRequest<Response<List<Order>>>;
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Response<List<Order>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<List<Order>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrders();
            return orders;
        }
    }
}
