using Application.Features.Categories.Commands;
using Application.Features.Categories.Queries;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Requests;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class CategoryController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
            => HandleResult(await Mediator.Send(new GetCategoriesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(Guid id)
            => HandleResult(await Mediator.Send(new GetCategoryQuery(id)));

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest category)
            => HandleResult(await Mediator.Send(new CreateCategoryCommand(category)));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryRequest category)
            => HandleResult(await Mediator.Send(new UpdateCategoryCommand(id, category)));
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
            => HandleResult(await Mediator.Send(new DeleteCategoryCommand(id)));

    }
}
