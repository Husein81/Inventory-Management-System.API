using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Response;

namespace API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();  
        protected IActionResult HandleResult<T>(Response<T> response)
            => response.IsSuccess ? Ok(response.Value) : BadRequest(response.Message);
    }
}
