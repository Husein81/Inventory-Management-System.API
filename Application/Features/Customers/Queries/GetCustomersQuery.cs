using Application.Repository;
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
    public record GetCustomersQuery : IRequest<Response<List<Customer>>>;
    public class GetCustomerQueriesHandler : IRequestHandler<GetCustomersQuery, Response<List<Customer>>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerQueriesHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Response<List<Customer>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _customerRepository.GetCustomers();
        }
    }
}
