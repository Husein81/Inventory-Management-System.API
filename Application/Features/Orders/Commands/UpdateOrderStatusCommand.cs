
using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderStatusCommand(Guid Id, UpdateOrderStatusDto Status) : IRequest<Response<Order>>;
    public class UpdateOrderStatusCommandHandler :IRequestHandler<UpdateOrderStatusCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }   
        public async Task<Response<Order>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var OrderStatus = _mapper.Map<Order>(request.Status);
            return await _orderRepository.UpdateOrderStatus(request.Id, OrderStatus);
        }
    }
}
