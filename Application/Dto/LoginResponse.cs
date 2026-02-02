using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public sealed record LoginResponse(
     Guid UserId,
     string Email,
     string AccessToken,
     string RefreshToken);
}
