using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders()
            => HandleResult(await Mediator.Send(new GetOrdersQuery()));

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

    }
}
