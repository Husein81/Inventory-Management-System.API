using Application.Products.Commands;
using Application.Products.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts()
            => HandleResult(await Mediator.Send(new GetProductsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
            => HandleResult(await Mediator.Send(new GetProductQuery(id)));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
            => HandleResult(await Mediator.Send(command));

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
            => HandleResult(await Mediator.Send(command));

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
            => HandleResult(await Mediator.Send(new DeleteProductCommand(id)));
    }
}
