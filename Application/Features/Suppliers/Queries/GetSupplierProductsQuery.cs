using Application.Repository;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Suppliers.Queries
{
    public record GetSupplierProductsQuery(Guid Id, int Page, int PageSize) : IRequest<Response<PagedList<Product>>>;
    public class GetSupplierProductsQueryHandler : IRequestHandler<GetSupplierProductsQuery, Response<PagedList<Product>>>
    {
        private readonly ISupplierRepository _supplierRepository;
        public GetSupplierProductsQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Response<PagedList<Product>>> Handle(GetSupplierProductsQuery request, CancellationToken cancellationToken)
        {
            return await _supplierRepository.GetSupplierProducts(request.Id, request.Page, request.PageSize);
        }
    }
}
