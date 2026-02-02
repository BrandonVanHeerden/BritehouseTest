using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Application
{
    public interface IPasswordHasherService
    {
        byte[] Hash(string password);

        bool Verify(string password, byte[] passwordHash);
    }
}
