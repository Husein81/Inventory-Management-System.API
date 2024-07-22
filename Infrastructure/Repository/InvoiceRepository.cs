using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<Invoice>> CreateInvoice(Invoice request)
        {
            await _context.Invoices.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Invoice>.Success(request)
                : Response<Invoice>.Fail("Failed to create Invoice");
        }

        public async Task<Response<Unit>> DeleteInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (invoice is null)
            {
                return Response<Unit>.Fail($"Invoice with id:{id} not found");
            }

            _context.Invoices.Remove(invoice);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Unit>.Success(Unit.Value)
                : Response<Unit>.Fail("Failed to delete invoice");
        }

        public async Task<Response<List<Invoice>>> GetInvoices()
            => Response<List<Invoice>>.Success(await _context.Invoices.ToListAsync());

        public async Task<Response<Invoice>> GetInvoice(Guid id)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            if (invoice is null)
            {
                return Response<Invoice>.Fail($"Invoice with id:{id} not found");
            }
            return Response<Invoice>.Success(invoice);
        }

        public async Task<Response<Invoice>> UpdateInvoice(Guid Id, Invoice request)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.Id == Id);
            if (invoice is null)
            {
                return Response<Invoice>.Fail($"Inventory with id:{Id} not found");
            }

            invoice.Update(request);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? Response<Invoice>.Success(invoice)
                : Response<Invoice>.Fail("Failed to update invoice");
        }
    }
}
