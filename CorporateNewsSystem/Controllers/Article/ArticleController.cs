using Api.Extensions;
using Application.Commands.Article;
using Application.Queries.Article;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Article
{
    [ApiVersion(1, Deprecated = false)]
    public class ArticleController(IMediator mediator) : ApiBaseController(mediator)
    {
        [AllowAnonymous]
        [MapToApiVersion(1)]
        [HttpGet("list")]
        public async Task<IActionResult> GetArticleList([FromQuery]ListArticleQuery query,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.ToActionResult(this);

        }

        [Authorize(Roles ="Admin,Author")]
        [MapToApiVersion(1)]
        [HttpPost("admin")]
        public async Task<IActionResult> CreateArticle([FromBody] AdminCreateArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult(this);

        }

    }
}
