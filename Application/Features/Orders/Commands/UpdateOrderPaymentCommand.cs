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
    public record UpdateOrderPaymentCommand(Guid Id, UpdateOrderPaymentDto Payment) : IRequest<Response<Order>>;
    public class UpdateOrderPaymentCommandHandler : IRequestHandler<UpdateOrderPaymentCommand, Response<Order>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public UpdateOrderPaymentCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        public async Task<Response<Order>> Handle(UpdateOrderPaymentCommand request, CancellationToken cancellationToken)
        {
            var OrderPayment = _mapper.Map<Order>(request.Payment);
            return await _orderRepository.UpdateOrderPayment(request.Id, OrderPayment);
        }
    }
}
