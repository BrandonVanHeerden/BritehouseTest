using Domain.Models;
using Infrastructure.Persistance.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Infrastructure
{
    public interface IArticleRepository
    {
        Task Add(Article article);
        Task<PagedList<ArticleModel>> GetPagedArticlesAsync(int pageNumber, int pageSize, string? searchTerm, bool onlyActive, CancellationToken cancellationToken = default);
    }
}
