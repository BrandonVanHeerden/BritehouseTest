using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Infrastructure
{
    public interface IArticleRepository
    {
        Task<PagedList<ArticleModel>> GetPagedArticlesAsync(int pageNumber, int pageSize, string? searchTerm, bool onlyActive, CancellationToken cancellationToken = default);
    }
}
