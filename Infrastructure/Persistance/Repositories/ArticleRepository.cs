using Application.Dto;
using Domain.Abstraction.Infrastructure;
using Domain.Models;
using Infrastructure.Persistance.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly AppDBContext _context;

        public ArticleRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<PagedList<ArticleModel>> GetPagedArticlesAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm,
            bool onlyActive,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Articles
                .AsNoTracking()
                .Include(a => a.User)
                .Where(a => !onlyActive || a.IsActive);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                    a.Title.Contains(searchTerm) ||
                    a.Summary.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(a => a.PublishedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new ArticleModel(
                    a.Id,
                    a.Title,
                    a.Summary,
                    a.PublishedDate,
                    $"{a.User.Name} {a.User.Surname}"))
                .ToListAsync(cancellationToken);

            return new PagedList<ArticleModel>(items, totalCount, pageNumber, pageSize);
        }
        public async Task Add(Article article)
       => await _context.Articles.AddAsync(article);
    }
}
