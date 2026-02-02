using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Application
{
    public interface ICurrentUserService
    {
        string GetCurrentUserAccount();
        Guid GetCurrentUserId();
    }
}
