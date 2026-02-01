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
    public class RoleRepository: IRoleRepository
    {
        private readonly AppDBContext _context;

        public RoleRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _context.Roles
                .SingleOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<IEnumerable<Role>> GetRolesForUserAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role)
                .ToListAsync();
        }
    }
}
