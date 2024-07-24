using Application.Features.Orders.Commands;
using Application.Requests;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateCreateOrder([FromBody] OrderRequest order)
            => HandleResult(await Mediator.Send(new CreateOrderCommand(order)));
    }
}
