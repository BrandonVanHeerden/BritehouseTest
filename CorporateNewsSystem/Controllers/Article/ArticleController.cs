using Api.Extensions;
using Application.Commands.Article;
using Application.Common;
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

        [Authorize(Roles = Roles.Admin + "," + Roles.Author)]
        [MapToApiVersion(1)]
        [HttpPost("admin/create")]
        public async Task<IActionResult> CreateArticle([FromBody] AdminCreateArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult(this);

        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Author)]
        [MapToApiVersion(1)]
        [HttpDelete("admin/delete")]
        public async Task<IActionResult> DeleteArticle([FromBody] AdminDeleteArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result.ToActionResult(this);

        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Author)]
        [MapToApiVersion(1)]
        [HttpPut("admin/update/{Id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute]Guid Id,[FromBody] AdminUpdateArticleCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AdminUpdateArticleCommand(Id, command.Title, command.Summary,command.Content,command.EndDate), cancellationToken);
            return result.ToActionResult(this);

        }

    }
}
