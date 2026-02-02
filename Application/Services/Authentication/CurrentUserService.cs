using Domain.Abstraction.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Application.Services.Authentication
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _http;

        public CurrentUserService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public Guid GetCurrentUserId()
        {
            var claimValue = _http.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type == "UserId")?.Value;

            if (Guid.TryParse(claimValue, out var userId))
                return userId;

            return Guid.Empty;
        }
    }
}
