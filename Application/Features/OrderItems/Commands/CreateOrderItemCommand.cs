using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.OrderItems.Commands
{
    public record CreateOrderItemCommand(OrderItemRequest request) : IRequest<Response<OrderItem>>;
    
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, Response<OrderItem>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;   

        public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Response<OrderItem>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = _mapper.Map<OrderItem>(request.request);
            return await _orderItemRepository.CreateOrderItem(orderItem);
        }
    }
}
