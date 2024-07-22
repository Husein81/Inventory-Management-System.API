using Application.Repository;
using MediatR;
using Shared.Response;

namespace Application.Features.OrderItems.Commands
{
    public record DeleteOrderItemCommand(Guid Id) : IRequest<Response<Unit>>;
    
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, Response<Unit>>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = await _orderItemRepository.DeleteOrderItem(request.Id);

            return orderItem;
        }
    }
}
