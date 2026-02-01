using Domain.Abstraction.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class UnitOfWorkRepository:IUnitOfWorkRepository
    {
        private readonly AppDBContext _context;

        public UnitOfWorkRepository(AppDBContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
