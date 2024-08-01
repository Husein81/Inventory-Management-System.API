using Application.Features.OrderItems.Commands;
using Application.Features.OrderItems.Queries;
using Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class OrderItemController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderItem>>> GetOrderItems()
            => HandleResult(await Mediator.Send(new GetOrderItemsQuery()));
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderItem(Guid id)
            => HandleResult(await Mediator.Send(new GetOrderItemQuery(id)));
        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemRequest orderItem)
            => HandleResult(await Mediator.Send(new CreateOrderItemCommand(orderItem)));
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(Guid id, [FromBody] OrderItemRequest orderItem)
            => HandleResult(await Mediator.Send(new UpdateOrderItemCommand(id, orderItem)));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id)
            => HandleResult(await Mediator.Send(new DeleteOrderItemCommand(id)));
    }
}
