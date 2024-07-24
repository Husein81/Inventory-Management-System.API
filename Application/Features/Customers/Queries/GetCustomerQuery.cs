using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Customers.Queries
{
    public record GetCustomerQuery(Guid Id) : IRequest<Response<Customer>>;
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Response<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Response<Customer>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            => await _customerRepository.GetCustomer(request.Id);
    }
}
