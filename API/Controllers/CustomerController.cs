using Application.Features.Customers.Commands;
using Application.Features.Customers.Queries;
using Application.Requests;
using Domain.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<Customer>>> GetCustomers(int page, int pageSize, CancellationToken cancellationToken)
            => HandlePagedResult(await Mediator.Send(new GetCustomersQuery(page, pageSize), cancellationToken));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
            => HandleResult(await Mediator.Send(new GetCustomerQuery(id)));

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRequest request)
            => HandleResult(await Mediator.Send(new CreateCustomerCommand(request)));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, CustomerRequest request)
            => HandleResult(await Mediator.Send(new UpdateCustomerCommand(id, request)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
            => HandleResult(await Mediator.Send(new DeleteCustomerCommand(id)));
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetCustomerOrders(Guid id)
            => HandlePagedResult(await Mediator.Send(new GetCustomerOrdersQuery(id)));
    }
}
