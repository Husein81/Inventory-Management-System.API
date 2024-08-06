using Application.Repository;
using Application.Requests;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Response;

namespace Infrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<List<Customer>>> GetCustomers()
        {
            return Response<List<Customer>>.Success(await _context.Customers
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderItems)
                .ThenInclude(x => x.Product).ToListAsync());
        } 
        // Order, OrderItems
        public async Task<Response<Customer>> GetCustomer(Guid id)
        {
            var customer = await _context.Customers.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id);
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
            var customer = await _context.Customers.Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == Id);
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

        public async Task<Response<PagedList<OrderDto>>> GetCustomerOrders(Guid id)
        {
            var orders =  _context.Orders
                .Include(x => x.Customer)
                .Include(x => x.OrderItems)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .Where(x => x.CustomerId == id).AsQueryable();

            return Response<PagedList<OrderDto>>.Success(await PagedList<OrderDto>.ToPagedList(orders, 1, 10));
        }
    }
}
