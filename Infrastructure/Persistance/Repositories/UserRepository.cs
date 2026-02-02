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
    public class UserRepository:IUserRepository
    {
        private readonly AppDBContext _context;

        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task Add(User user)
        {
           await _context.Users.AddAsync(user);
        }
        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users
                .SingleOrDefaultAsync(u => u.Email == email);
        }
        public Task<User?> GetByEmailWithRolesAsync(string email)
        {
            return _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
        }
        public Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public Task<User?> GetByIdAsync(Guid id)
        {
            return _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Id == id);
        }
    }
}
