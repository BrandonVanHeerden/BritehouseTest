using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserRole
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
    }

}
