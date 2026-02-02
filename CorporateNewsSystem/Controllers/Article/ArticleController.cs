using Api.Extensions;
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
    }
}
