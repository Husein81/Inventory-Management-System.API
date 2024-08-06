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

namespace Application.Features.Customers.Queries
{
    public record GetCustomersQuery : IRequest<Response<List<CustomerDto>>>;
    public class GetCustomerQueriesHandler : IRequestHandler<GetCustomersQuery, Response<List<CustomerDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueriesHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return Response<List<CustomerDto>>.Success(_mapper.Map<List<CustomerDto>>((await _customerRepository.GetCustomers()).Value));
        }
    }
}
