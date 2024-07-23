using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Repository{
    public interface ISupplierRepository
    {
        Task<Response<List<Supplier>>> GetSuppliers();
        Task<Response<Supplier>> GetSupplier(Guid id);
        Task<Response<Supplier>> CreateSupplier(Supplier request);
        Task<Response<Supplier>> UpdateSupplier(Guid Id,Supplier request);
        Task<Response<Unit>> DeleteSupplier(Guid id);
    }
}
