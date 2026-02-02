using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Infrastructure
{
    public interface IAccountsRepository
    {
        Task Add(Account account);

        Task<Account?> GetByUserIdAsync(Guid userId);

        Task<bool> ExistsForUserAsync(Guid userId);
    }
}
