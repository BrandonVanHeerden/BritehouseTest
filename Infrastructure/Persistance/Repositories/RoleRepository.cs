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

        public async Task<IEnumerable<Role>> GetAllActiveRolesAsync()
        {
            return await _context.Roles
                .Where(ur => ur.IsActive == true)               
                .ToListAsync();
        }
        public async Task AddRolesToUserAsync(
        Guid userId,
        IEnumerable<Guid> roleIds,
        CancellationToken cancellationToken)
        {
            var distinctRoleIds = roleIds.Distinct().ToList();

            var existingRoleIds = await _context.UserRoles
                .Where(ur => ur.UserId == userId &&
                             distinctRoleIds.Contains(ur.RoleId))
                .Select(ur => ur.RoleId)
                .ToListAsync(cancellationToken);

            var newRoleIds = distinctRoleIds
                .Except(existingRoleIds)
                .ToList();

            if (!newRoleIds.Any())
                return;

            var userRoles = newRoleIds.Select(roleId =>
                new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });

            await _context.UserRoles.AddRangeAsync(
                userRoles,
                cancellationToken);
        }
    }
}
