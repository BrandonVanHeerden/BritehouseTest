using Application.Common;
using Domain.Abstraction.Application;
using Domain.Abstraction.Infrastructure;
using Domain.Factory;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Commands
{
    public sealed record SignUpCommand(string Name, string Surname, string Email, string Cell, string Id, string Password, IEnumerable<Guid> Roles) : IRequest<NewsResult<bool>>;

    public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, NewsResult<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountsRepository _accountsRepository;
        private readonly IUnitOfWorkRepository _unitOfWork;
        private readonly IPasswordHasherService _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public SignUpCommandHandler(IUserRepository userRepository, IAccountsRepository accountsRepository, IUnitOfWorkRepository unitOfWork, IPasswordHasherService passwordHasher, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _accountsRepository = accountsRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }
        public async Task<NewsResult<bool>> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email))
            {
                return NewsResult<bool>.Failure(
                    new BaseNewsError("User Already Exists", StatusCodes.Status409Conflict));
            }

            var user = UserFactory.Create(
                request.Name,
                request.Surname,
                request.Email,
                request.Cell,
                request.Id,
                "sign up");

            var passwordHash =
            _passwordHasher.Hash(request.Password);


            var account =
            AccountFactory.Create(user.Id, passwordHash, null, "sign up");

            await _roleRepository.AddRolesToUserAsync(
                                            user.Id,
                                            request.Roles,
                                            cancellationToken);


            // await _userRepository.AddUserRole(user.Id, role.Id);
            await _userRepository.Add(user);
            await _accountsRepository.Add(account);
            var inserted = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return NewsResult<bool>.Success(inserted > 0);
        }
    }
}
