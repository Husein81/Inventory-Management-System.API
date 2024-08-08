using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Customers.Queries
{
    public record GetCustomersQuery(int Page, int PageSize) : IRequest<Response<PagedList<CustomerDto>>>;
    public class GetCustomerQueriesHandler : IRequestHandler<GetCustomersQuery, Response<PagedList<CustomerDto>>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueriesHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<PagedList<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return Response<PagedList<CustomerDto>>.Success(_mapper.Map<PagedList<CustomerDto>>((await _customerRepository.GetCustomers(request.Page,request.PageSize)).Value));
        }
    }
}
