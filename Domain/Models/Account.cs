using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Account:BaseModel
    {
        public Guid UserId { get; set; }
        public byte[] Password { get; set; }
        public string RefreshToken { get; set; }
        public string OTP { get; set; }
        public User User { get; set; }
    }
}
