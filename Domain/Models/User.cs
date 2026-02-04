using Infrastructure.Persistance.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User: BaseModel
    {
        public string Name { get; private set; } = null!;
        public string Surname { get; private set; } = string.Empty;      
        public string Email { get; private set; } = string.Empty;      
        public string CellNo { get; private set; } = string.Empty;      
        public string? IdNumber { get; private set; } = string.Empty;      
        public ICollection<Article> Articles { get; private set; } = new List<Article>();
        public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public User()
        {
            
        }
        public User(string name, string surname, string email, string cell, string idNumber, string lastModifiedUser)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            CellNo = cell;
            IdNumber = idNumber;
            LastModifiedUser = lastModifiedUser;
            IsActive = true;
        }     

    }
   
}
