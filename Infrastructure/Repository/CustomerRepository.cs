using Application.Repository;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<Customer>>> GetCustomers()
            => Response<List<Customer>>.Success(await _context.Customers.ToListAsync());

        public async Task<Response<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
                return Response<Customer>.Fail("Customer not found");

            return Response<Customer>.Success(customer);
        }

        public async Task<Response<Customer>> CreateCustomer(Customer request)
        {
            await _context.Customers.AddAsync(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Customer>.Success(request)
                : Response<Customer>.Fail("Failed to create customer");
        }

        public async Task<Response<Customer>> UpdateCustomer(Guid Id, Customer request)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == Id);
            if (customer == null)
                return Response<Customer>.Fail("Customer not found");

            customer.Update(request);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Customer>.Success(customer) 
                : Response<Customer>.Fail("Failed to update customer");
        }

        public async Task<Response<Unit>> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null)
                return Response<Unit>.Fail("Customer not found");

            _context.Customers.Remove(customer);
            var result = await _context.SaveChangesAsync() > 0;

            return result ? Response<Unit>.Success(Unit.Value)
                : Response<Unit>.Fail("Failed to delete customer");
        }
    }
}
