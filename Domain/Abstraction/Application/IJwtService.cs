using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstraction.Application
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(string userId, string account, bool accountActive, IEnumerable<string> roles = null);
        Task<string> GenerateAsync(IEnumerable<Claim> claims);
        (string refreshToken, DateTime expireDateTime) GenerateRefreshToken();
    }
}
