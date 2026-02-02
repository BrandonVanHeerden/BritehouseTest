using Domain.Abstraction.Infrastructure;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class AccountRepository:IAccountsRepository
    {
        private readonly AppDBContext _context;

        public AccountRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task Add(Account account)
            => await _context.Accounts.AddAsync(account);

        public Task<Account?> GetByUserIdAsync(Guid userId)
            => _context.Accounts
                .SingleOrDefaultAsync(a => a.UserId == userId);

        public Task<bool> ExistsForUserAsync(Guid userId)
            => _context.Accounts
                .AnyAsync(a => a.UserId == userId);
    }
}
