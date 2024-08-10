using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;
        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Supplier>> CreateSupplier(Supplier request)
        {
            await _context.Suppliers.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Supplier>.Success(request)
                : Response<Supplier>.Fail("Failed to create supplier");
        }

        public async Task<Response<Unit>> DeleteSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return Response<Unit>.Fail("Supplier not found");

            _context.Suppliers.Remove(supplier);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value)
                : Response<Unit>.Fail("Failed to delete supplier");
        }

        public async Task<Response<Supplier>> GetSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
                return Response<Supplier>.Fail("Supplier not found");

            return Response<Supplier>.Success(supplier);
        }

        public async Task<Response<PagedList<Supplier>>> GetSuppliers(int page, int pageSize)
        {
            var suppliers = _context.Suppliers.AsQueryable();
            return Response<PagedList<Supplier>>.Success(
                await PagedList<Supplier>.ToPagedList(suppliers, page, pageSize));
        }

        public Task<Response<Supplier>> UpdateSupplier(Guid Id, Supplier request)
        {
           var supplier = _context.Suppliers.Find(Id);
            if (supplier == null)
                return Task.FromResult(Response<Supplier>.Fail("Supplier not found"));

            supplier.Update(request);
            var result = _context.SaveChanges() > 0;

            return Task.FromResult(result ? Response<Supplier>.Success(supplier)
                : Response<Supplier>.Fail("Failed to update supplier"));
        }

        public async Task<Response<PagedList<Product>>> GetSupplierProducts(Guid id, int page, int pageSize)
        {
            var products = _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Where(p => p.SupplierId == id)
                .AsQueryable();

            return Response<PagedList<Product>>.Success(
                await PagedList<Product>.ToPagedList(products, page, pageSize));
        }
    }
}
