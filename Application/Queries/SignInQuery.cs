using Application.Common;
using Application.Dto;
using Domain.Abstraction.Application;
using Domain.Abstraction.Infrastructure;
using Domain.Models;
using MediatR;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Queries
{
    public sealed record SignInQuery(string Email, string Password):IRequest<NewsResult<LoginResponse>>;
    public sealed class SignInQueryHandler : IRequestHandler<SignInQuery, NewsResult<LoginResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public SignInQueryHandler(IUserRepository userRepository,
        IAccountsRepository accountsRepository,
        IJwtService jwtService,
        IPasswordHasherService passwordHasher,
        IRoleRepository roleRepository
        //IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _accountsRepository = accountsRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }
        public async Task<NewsResult<LoginResponse>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            
             var user = await _userRepository.GetByEmailAsync(request.Email);

              if (user is null || !user.IsActive)
                  return NewsResult<LoginResponse>.Failure(new BaseNewsError("User Account Inactive Or Not Found", 404));

              var account =
                  await _accountsRepository.GetByUserIdAsync(user.Id);

              if (account is null)
                  return NewsResult<LoginResponse>.Failure(
                      new BaseNewsError("User Account does not exist", 404));

              if (!_passwordHasher.Verify(
                  request.Password,
                  account.Password))
              {
                  return NewsResult<LoginResponse>.Failure(
                      new BaseNewsError("Invalid Credentials Supplied", 404));
              }
              
              var userRoles = await _roleRepository.GetRolesForUserAsync(user.Id);

            var roleNames = userRoles?.Select(x => x.Name).ToList() ?? new List<string>();
            var claims = _jwtService.GetClaims(user.Id.ToString(), user.Email, user.IsActive, roleNames);
            var accessToken = await _jwtService.GenerateAsync(claims);
            //TODO: assign refresh token to user
            var refreshToken = _jwtService.GenerateRefreshToken();

            return NewsResult<LoginResponse>.Success(
                          new LoginResponse(
                              user.Id,
                              user.Email,
                              accessToken,
                              refreshToken.refreshToken));
          
        }
    }
}
