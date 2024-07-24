using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Customers.Commands
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<Response<Unit>>;
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<Unit>>  
    {
        private readonly ICustomerRepository _customerRepository;
        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteCustomer(request.Id);
        }
    }
}
