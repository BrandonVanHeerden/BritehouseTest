using Api.Extensions;
using Application.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authentication
{
    [ApiVersion(1, Deprecated = false)]
    public class AuthenticationController(IMediator mediator) : ApiBaseController(mediator)
    {
        [MapToApiVersion(1)]
        [HttpGet("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromQuery]SignInQuery query,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult(this);
        }
    }
}
