using Domain.Entities;
using MediatR;
using Shared.Response;

namespace Application.Repository
{
    public interface IInvoiceRepository
    {
        Task<Response<List<Invoice>>> GetInvoices();
        Task<Response<Invoice>> GetInvoice(Guid id);
        Task<Response<Invoice>> CreateInvoice(Invoice request);
        Task<Response<Invoice>> UpdateInvoice(Guid Id, Invoice request);
        Task<Response<Unit>> DeleteInvoice(Guid id);
    }
}
