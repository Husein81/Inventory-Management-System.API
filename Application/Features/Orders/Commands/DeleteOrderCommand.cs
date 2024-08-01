
using Application.Repository;
using MediatR;
using Shared.Response;

namespace Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : IRequest<Response<Unit>>;
    
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Response<Unit>>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteOrder(request.Id);
        }

    }
}
