using Application.Common;
using Domain.Abstraction.Application;
using Domain.Abstraction.Infrastructure;
using Domain.Factory;
using Domain.Models;
using Infrastructure.Persistance.DataModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Commands.Article
{
    public sealed record AdminUpdateArticleContract(string Title, string Summary, string Content, DateTime? EndDate);
    public sealed record AdminUpdateArticleCommand : IRequest<NewsResult<bool>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public DateTime? EndDate { get; set; }

        public AdminUpdateArticleCommand(Guid id, string title, string summary, string content, DateTime? endDate)
        {
            Id = id;
            Title = title;
            Summary = summary;
            Content = content;
            EndDate = endDate;

        }
    }
    public sealed class AdminUpdateArticleCommandHandler : IRequestHandler<AdminUpdateArticleCommand, NewsResult<bool>>
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRoleRepository _roleRepository;

        public AdminUpdateArticleCommandHandler(IUnitOfWorkRepository unitOfWorkRepository, IArticleRepository articleRepository, IUserRepository userRepository, ICurrentUserService currentUserService, IRoleRepository roleRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _roleRepository = roleRepository;
        }
        public async Task<NewsResult<bool>> Handle(AdminUpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(
           request.Id,
           cancellationToken);

            if (article is null || !article.IsActive)
                return NewsResult<bool>.Failure(new BaseNewsError("Article does not exist or is not active",404));

            var userId = _currentUserService.GetCurrentUserId();

            var roles = await _roleRepository.GetRolesForUserAsync(userId);

            var isAdmin = roles.Any(r => r.Name == Roles.Admin);

            if (!isAdmin && article.UserId != userId)
                return NewsResult<bool>.Failure(new BaseNewsError("Insufficient Permissions",403));

            ArticleFactory.Update(
                request.Title,
                request.Summary,
                request.Content,
                userId,
                request.EndDate,
                _currentUserService.GetCurrentUserAccount());

            await _unitOfWorkRepository.SaveChangesAsync(cancellationToken);

            return NewsResult<bool>.Success(true);
        }
    }
}
