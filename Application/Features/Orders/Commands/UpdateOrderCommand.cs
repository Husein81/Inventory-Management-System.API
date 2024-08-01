using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands
{
    public record UpdateOrderCommand(Guid Id, OrderRequest Request) : IRequest<Response<Order>>;
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
           
        }

        public async Task<Response<Order>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var order = _mapper.Map<Order>(request.Request);

            return await _orderRepository.UpdateOrder(request.Id,order);
        }
    }
}
