using Application.Features.Products.Commands;
using Application.Features.Products.Queries;
using Application.Products.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Requests;
using Shared;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<Product>>> GetProducts(int page  , int pageSize, string searchTerm , CancellationToken cancellationToken)
        {
            var products = await Mediator.Send(new GetProductsQuery(page, pageSize, searchTerm), cancellationToken);
            return HandlePagedResult(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
            => HandleResult(await Mediator.Send(new GetProductQuery(id)));

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest product)
            => HandleResult(await Mediator.Send(new CreateProductCommand(product)));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest product)
            => HandleResult(await Mediator.Send(new UpdateProductCommand(id, product)));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
            => HandleResult(await Mediator.Send(new DeleteProductCommand(id)));

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<PagedList<Product>>> 
            GetProductsByCategory(Guid categoryId, int page, int pageSize)
            => HandlePagedResult(await Mediator.Send(new GetProductsByCategoryQuery(categoryId, page, pageSize)));
    }
}
