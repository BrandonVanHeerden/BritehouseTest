using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Account:BaseModel
    {
        public Guid UserId { get; private set; }
        public byte[] Password { get; private set; } 
        public string RefreshToken { get; private set; }
        public string OTP { get; private set; }
        public User User { get; private set; }

        public Account()
        {
            
        }
        public Account(Guid userId, byte[] password, string refreshToken, string lastModifiedUser)
        {
            UserId = userId;
            Password = password;
            RefreshToken = refreshToken;
            LastModifiedUser = lastModifiedUser;
        }
    }
}
