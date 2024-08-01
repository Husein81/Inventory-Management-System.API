
using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Orders.Queries
{
    public record GetOrderQuery(Guid Id) : IRequest<Response<Order>>;
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<Order>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.Id);
            return order;
        }
    }
}
