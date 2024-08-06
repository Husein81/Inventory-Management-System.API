using MediatR;
using Microsoft.AspNetCore.Http;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Response;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();  
        protected ActionResult HandleResult<T>(Response<T> result)
        {
            if (result is null) return NotFound();

            if (result.IsSuccess && result.Value is not null)
            {
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value is null)
            {
                return NotFound();
            }
            return BadRequest(result.Message);
        }
        protected ActionResult HandlePagedResult<T>(Response<PagedList<T>> result)
        {
            if (result is null) return NotFound();
            if (result.IsSuccess && result.Value is not null)
            {
                Response.AddPaginationHeader(
                    result.Value.CurrentPage, result.Value.PageSize, result.Value.TotalCount, result.Value.TotalPages);
                return Ok(result.Value);
            }
            if (result.IsSuccess && result.Value is null)
            {
                return NotFound();
            }
            return BadRequest(result.Message);
        }
    }
}
