using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Features.Suppliers.Queries
{
    public record GetSuppliersQuery(int Page, int PageSize) : IRequest<Response<PagedList<Supplier>>>;

    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, Response<PagedList<Supplier>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Response<PagedList<Supplier>>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSuppliers(request.Page, request.PageSize);
        }
    }
}

