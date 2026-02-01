using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Role:BaseModel
    {
        public string Name { get; private set; }
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    }
}
