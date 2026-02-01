using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public bool IsActive { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public string LastModifiedUser { get; set; } = string.Empty;

        public BaseModel()
        {
            Id = Guid.NewGuid();
            LastModifiedDate = DateTime.Now;
        }
    }
}
