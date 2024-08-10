using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository{
    public interface ISupplierRepository
    {
        Task<Response<PagedList<Supplier>>> GetSuppliers(int page, int pageSize);
        Task<Response<Supplier>> GetSupplier(Guid id);
        Task<Response<Supplier>> CreateSupplier(Supplier request);
        Task<Response<Supplier>> UpdateSupplier(Guid Id,Supplier request);
        Task<Response<Unit>> DeleteSupplier(Guid id);
        Task<Response<PagedList<Product>>> GetSupplierProducts(Guid id, int page, int pageSize);
    }
}
