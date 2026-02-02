using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factory
{
    public static class AccountFactory
    {
        public static Account Create(
                    Guid userId, byte[] password, string refreshToken, string lastModifiedUser)
        {
            return new Account(userId , password, refreshToken, lastModifiedUser);
        }
    }
}
