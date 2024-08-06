
using Application.Repository;
using Application.Requests;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Features.Orders.Queries
{
    public record GetOrdersQuery(int Page, int PageSize, string SearchTerm) : IRequest<Response<PagedList<OrderDto>>>;
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, Response<PagedList<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<PagedList<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrders(request.Page, request.PageSize, request.SearchTerm);
            return orders;
        }
    }
}
