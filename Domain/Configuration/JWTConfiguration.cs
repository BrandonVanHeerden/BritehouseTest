using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configuration
{
    public class JWTConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int RefreshTokenExpirationTimeInDays { get; set; }
        public Guid APP_UUID { get; set; }
    }
}
