using Application.Repository;
using Application.Requests;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Suppliers.Commands
{
    public record CreateSupplierCommand(SupplierRequest Request) : IRequest<Response<Supplier>>;
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, Response<Supplier>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _mapper = mapper;
            _supplierRepository = supplierRepository;
        }
        public async Task<Response<Supplier>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier =  _mapper.Map<Supplier>(request.Request);

            return await _supplierRepository.CreateSupplier(supplier);
        }
    }
}
