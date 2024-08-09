using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<Order>>> GetOrders(int page, int pageSize, string searchTerm, CancellationToken cancellationToken)
            => HandlePagedResult(await Mediator.Send(new GetOrdersQuery(page, pageSize, searchTerm),cancellationToken));
        [HttpGet("CompletedOrders")]
        public async Task<ActionResult<PagedList<Order>>> GetCompletedOrders(int page, int pageSize, string searchTerm, CancellationToken cancellationToken)
            => HandlePagedResult(await Mediator.Send(new GetCompletedOrdersQuery(page, pageSize, searchTerm),cancellationToken));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
            => HandleResult(await Mediator.Send(new GetOrderQuery(id)));
        
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderRequest order)
            => HandleResult(await Mediator.Send(new CreateOrderCommand(order)));
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] OrderRequest order)
            => HandleResult(await Mediator.Send(new UpdateOrderCommand(id, order)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
            => HandleResult(await Mediator.Send(new DeleteOrderCommand(id)));
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto status)
            => HandleResult(await Mediator.Send(new UpdateOrderStatusCommand(id, status)));
        [HttpPut("{id}/payment")]
        public async Task<IActionResult> UpdateOrderPayment(Guid id, [FromBody] UpdateOrderPaymentDto payment)
            => HandleResult(await Mediator.Send(new UpdateOrderPaymentCommand(id, payment)));
    }
}
