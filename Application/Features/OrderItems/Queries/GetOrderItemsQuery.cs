using MediatR;
using Domain.Entities;
using Shared.Response;
using Application.Repository;

namespace Application.Features.OrderItems.Queries
{
    public record GetOrderItemsQuery : IRequest<Response<List<OrderItem>>>;
    
    public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, Response<List<OrderItem>>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public GetOrderItemsQueryHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Response<List<OrderItem>>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemRepository.GetOrderItems();

            return orderItems;
        }
    }
}
