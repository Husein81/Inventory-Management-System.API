using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Suppliers.Queries
{
    public record GetSupplierQuery(Guid Id) : IRequest<Response<Supplier>>;
    public class GetSupplierQueryHandler : IRequestHandler<GetSupplierQuery, Response<Supplier>>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetSupplierQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Response<Supplier>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSupplier(request.Id);
        }
    }
}
