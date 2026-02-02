using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Infrastructure
{
    public interface IUnitOfWorkRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
