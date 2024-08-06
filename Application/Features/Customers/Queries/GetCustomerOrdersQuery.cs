using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;


namespace Application.Features.Customers.Queries
{
    public record GetCustomerOrdersQuery(Guid Id) : IRequest<Response<PagedList<OrderDto>>>;
    public class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, Response<PagedList<OrderDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerOrdersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
           _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<Response<PagedList<OrderDto>>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomerOrders(request.Id);
        }
    }
    
}
