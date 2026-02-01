using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authentication
{
    [ApiVersion(1, Deprecated = false)]
    public class AuthenticationController(IMediator mediator) : ApiBaseController(mediator)
    {
       
    }
}
