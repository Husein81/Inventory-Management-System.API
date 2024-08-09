using Application.Repository;
using Application.Requests;
using MediatR;
using Shared;
using Shared.Response;


namespace Application.Features.Orders.Queries
{
    public record GetCompletedOrdersQuery(int Page, int PageSize, string searchTerm) : IRequest<Response<PagedList<OrderDto>>>;
    public class GetCompletedOrdersQueryHandler : IRequestHandler<GetCompletedOrdersQuery, Response<PagedList<OrderDto>>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetCompletedOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<PagedList<OrderDto>>> Handle(GetCompletedOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetCompletedOrders(request.Page, request.PageSize, request.searchTerm);
            return orders;
        }
    }
}
