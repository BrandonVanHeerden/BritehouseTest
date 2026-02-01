using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Admin
{
    [ApiVersion(1, Deprecated = false)]
    public class AdminController(IMediator mediator) : ApiBaseController(mediator)
    {
        
    }
}
