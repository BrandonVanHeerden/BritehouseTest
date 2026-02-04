using Application.Common;
using Domain.Abstraction.Application;
using Domain.Abstraction.Infrastructure;
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
    public sealed record AdminDeleteArticleCommand : IRequest<NewsResult<bool>>
    {
        public Guid ArticleId { get; set; }
        public AdminDeleteArticleCommand(Guid id)
        {
            ArticleId = id;
        }
    }

    public sealed class AdminDeleteArticleCommandHandler : IRequestHandler<AdminDeleteArticleCommand, NewsResult<bool>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IArticleRepository _articleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IRoleRepository _roleRepository;

        public AdminDeleteArticleCommandHandler(ICurrentUserService currentUserService, IArticleRepository articleRepository,
        IUserRepository userRepository,
        IUnitOfWorkRepository unitOfWorkRepository,IRoleRepository roleRepository)
        {
            _currentUserService = currentUserService;
            _articleRepository = articleRepository;
            _userRepository = userRepository;
            _unitOfWorkRepository = unitOfWorkRepository;
            _roleRepository = roleRepository;
        }
        public async Task<NewsResult<bool>> Handle(AdminDeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetByIdAsync(
          request.ArticleId,
          cancellationToken);

            if (article is null)
                return NewsResult<bool>.Failure(new BaseNewsError("Article does not exist",404));

            var userId = _currentUserService.GetCurrentUserId();

            var userRoles = await _roleRepository.GetRolesForUserAsync(userId);
            var isAdmin = userRoles.Any(r => r.Name == Roles.Admin);
            var isAuthor = userRoles.Any(r => r.Name == Roles.Author);

            if (!isAdmin && article.UserId != userId)
                return NewsResult<bool>.Failure(new BaseNewsError("Invalid Permissions",403));

            if (isAdmin)
            {
                _articleRepository.Remove(article);   
            }
            else if (isAuthor)
            {
                //article.SoftDelete(_currentUser.Email);
            }

            await _unitOfWorkRepository.SaveChangesAsync(cancellationToken);

            return NewsResult<bool>.Success(true);
        }
    }
}
