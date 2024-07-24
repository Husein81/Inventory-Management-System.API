using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;


namespace Application.Features.Customers.Commands
{
    public record UpdateCustomerCommand(Guid Id, CustomerRequest CustomerRequest) : IRequest<Response<Customer>>;
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<Customer>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
           
            var customer = _mapper.Map<Customer>(request.CustomerRequest);  

            return await _customerRepository.UpdateCustomer(request.Id, customer);
        }
    }
}
