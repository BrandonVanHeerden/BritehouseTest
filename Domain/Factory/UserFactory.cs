using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factory
{
    public static class UserFactory
    {
        public static User Create(
                     string name,
                     string surname,
                     string email,
                     string cellNo,
                     string idNumber,
                     string lastModifiedUser)
        {
            return new User(name, surname, email, cellNo, idNumber, lastModifiedUser);
        }

    }
}
