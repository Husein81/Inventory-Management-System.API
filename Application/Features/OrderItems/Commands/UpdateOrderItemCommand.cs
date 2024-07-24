using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.OrderItems.Commands
{
    public record UpdateOrderItemCommand(Guid Id, OrderItemRequest request) : IRequest<Response<OrderItem>>;

    public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, Response<OrderItem>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public UpdateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<Response<OrderItem>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = _mapper.Map<OrderItem>(request.request);
            return await _orderItemRepository.UpdateOrderItem(request.Id, orderItem);
        }
    }
}
