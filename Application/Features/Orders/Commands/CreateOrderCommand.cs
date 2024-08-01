
using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Response;

namespace Application.Features.Orders.Commands
{
    public record CreateOrderCommand(OrderRequest request) : IRequest<Response<Order>>;

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser>  _userManager;
       
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, UserManager<AppUser> userManager)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userManager = userManager;
           
        }

        public async Task<Response<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            
            var order = _mapper.Map<Order>(request.request);
           
            return await _orderRepository.CreateOrder(order);
        }

    }
}
