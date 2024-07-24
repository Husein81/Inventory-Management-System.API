using Application.Repository;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Suppliers.Commands
{
    public record DeleteSupplierCommand(Guid Id) : IRequest<Response<Unit>>;
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, Response<Unit>>
    {
        private readonly ISupplierRepository _supplierRepository;   
        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.DeleteSupplier(request.Id);
        }
    }
}
