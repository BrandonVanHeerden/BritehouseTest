using Application.Common;
using Application.Dto;
using Domain.Abstraction.Infrastructure;
using Domain.Models;
using Infrastructure.Persistance.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Article
{
    public sealed record ListArticleQuery(
                    int PageNumber = 1,
                    int PageSize = 10,
                    string? SearchTerm = null,
                    bool OnlyActive = true
        ) : IRequest<NewsResult<PagedList<ArticleModel>>>
    {
    }

    public sealed class ListArticleQueryHandler : IRequestHandler<ListArticleQuery, NewsResult<PagedList<ArticleModel>>>
    {
        private readonly IArticleRepository _articleRepository;

        public ListArticleQueryHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<NewsResult<PagedList<ArticleModel>>> Handle(ListArticleQuery request, CancellationToken cancellationToken)
        {
            var pagedArticles = await _articleRepository.GetPagedArticlesAsync(
           request.PageNumber,
           request.PageSize,
           request.SearchTerm,
           request.OnlyActive,
           cancellationToken);
            //TODO: Implement Automapper
            return NewsResult<PagedList<ArticleModel>>.Success(pagedArticles);
        }
    }
}
