using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Customers.Queries
{
    public record GetCustomerQuery(Guid Id) : IRequest<Response<CustomerDto>>;
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public GetCustomerQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<Response<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return Response<CustomerDto>.Success(_mapper.Map<CustomerDto>((await _customerRepository.GetCustomer(request.Id)).Value));
        }
    }
}
