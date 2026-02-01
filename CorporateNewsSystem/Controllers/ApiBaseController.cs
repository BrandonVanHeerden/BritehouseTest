using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/v{v:apiVersion}/[Controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
