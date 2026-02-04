using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } 
        public bool IsActive { get; set; }
        public DateTime LastModifiedDate { get;  set; }
        public string LastModifiedUser { get; set; } = string.Empty;

        public BaseModel()
        {
           // Id = Guid.NewGuid();
            //LastModifiedDate = DateTime.Now;
            //IsActive = true;
        }
        public BaseModel(Guid id, string lastModifiedUser) : this()
        {
            Id = id;
            LastModifiedDate = DateTime.Now;
            lastModifiedUser = lastModifiedUser;
        }
        public BaseModel(string lastModifiedUser)
        {
            Id = Guid.NewGuid();
            LastModifiedDate = DateTime.Now;
            lastModifiedUser = lastModifiedUser;
        }
    }
}
