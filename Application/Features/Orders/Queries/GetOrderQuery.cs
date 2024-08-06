
using Application.Repository;
using Application.Requests;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Orders.Queries
{
    public record GetOrderQuery(Guid Id) : IRequest<Response<OrderDto>>;
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Response<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrder(request.Id);
            return order;
        }
    }
}
