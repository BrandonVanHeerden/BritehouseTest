using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Infrastructure
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<bool> ExistsByEmailAsync(string email);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByEmailWithRolesAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
    }
}
