using Application.Features.Orders.Commands;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> CreateCreateOrder([FromBody] Order order)
            => HandleResult(await Mediator.Send(new CreateOrderCommand(order)));
    }
}
