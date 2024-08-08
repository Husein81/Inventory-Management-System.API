using Application.Features.Suppliers.Commands;
using Application.Features.Suppliers.Queries;
using Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    public class SupplierController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<Supplier>>> GetSuppliers(int page, int pageSize,CancellationToken cancellationToken)
            => HandlePagedResult(await Mediator.Send(new GetSuppliersQuery(page, pageSize), cancellationToken));
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSupplier(Guid id)
            => HandleResult(await Mediator.Send(new GetSupplierQuery(id)));

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(SupplierRequest request)
            => HandleResult(await Mediator.Send(new CreateSupplierCommand(request)));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(Guid id, SupplierRequest request)
            => HandleResult(await Mediator.Send(new UpdateSupplierCommand(id, request)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
            => HandleResult(await Mediator.Send(new DeleteSupplierCommand(id)));

    }
}
