using Application.Requests;
using Domain.Entities;
using MediatR;
using Shared;
using Shared.Response;

namespace Application.Repository
{
    public interface ICustomerRepository
    {
        Task<Response<List<Customer>>> GetCustomers();
        Task<Response<Customer>> GetCustomer(Guid id);
        Task<Response<Customer>> CreateCustomer(Customer request);
        Task<Response<Customer>> UpdateCustomer(Guid Id,Customer request);
        Task<Response<Unit>> DeleteCustomer(Guid id);
        Task<Response<PagedList<OrderDto>>> GetCustomerOrders(Guid id);

    }
}
