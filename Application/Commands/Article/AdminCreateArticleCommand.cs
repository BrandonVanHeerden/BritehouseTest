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
    public record AdminCreateArticleCommand(
    string Title,
    string Summary,
    string Content,
    DateTime? ArticleEndDate)
    : IRequest<NewsResult<Guid>>;

    public class AdminCreateArticleCommandHandler : IRequestHandler<AdminCreateArticleCommand, NewsResult<Guid>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public AdminCreateArticleCommandHandler(IArticleRepository articleRepository, ICurrentUserService currentUserService, IUserRepository userRepository,IUnitOfWorkRepository unitOfWorkRepository)
        {
            _articleRepository = articleRepository;
            _currentUserService = currentUserService;
            _userRepository = userRepository;
            _unitOfWorkRepository = unitOfWorkRepository;
        }
        public async Task<NewsResult<Guid>> Handle(AdminCreateArticleCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.GetCurrentUserId();

            var user = await _userRepository.GetByIdAsync(userId);

            if (user is null)
                return NewsResult<Guid>.Failure(
                    new BaseNewsError("Account not found",404));

            var article = ArticleFactory.Create(
                request.Title,
                request.Summary,
                request.Content,
                userId,
                DateTime.Now,
                request.ArticleEndDate,
                createdBy: user.Email);

            await _articleRepository.Add(article);

            await _unitOfWorkRepository.SaveChangesAsync(cancellationToken);

            return NewsResult<Guid>.Success(article.Id);
        }
    }
}
