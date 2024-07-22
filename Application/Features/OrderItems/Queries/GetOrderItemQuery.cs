using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;


namespace Application.Features.OrderItems.Queries
{
    public record GetOrderItemQuery(Guid Id) : IRequest<Response<OrderItem>>; 
    
    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, Response<OrderItem>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public GetOrderItemQueryHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Response<OrderItem>> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemRepository.GetOrderItem(request.Id);

            return orderItem;
        }

    }
}
