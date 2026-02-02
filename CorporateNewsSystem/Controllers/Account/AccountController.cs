using Api.Extensions;
using Application.Commands;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Account
{
    [ApiVersion(1, Deprecated = false)]
    public class AccountController(IMediator mediator) : ApiBaseController(mediator)
    {
        [HttpPost]
        [MapToApiVersion(1)]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody]SignUpCommand command,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult(this);
        }

    }
}
