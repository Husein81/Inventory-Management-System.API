using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Features.Suppliers.Queries
{
    public record GetSuppliersQuery : IRequest<Response<List<Supplier>>>;

    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, Response<List<Supplier>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Response<List<Supplier>>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSuppliers();
        }
    }
}

