using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DataModels
{
    public class Article: BaseModel
    {

        public string Title { get; private set; } = null!;
        public string Summary { get; private set; } = null!;
        public string Content { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public DateTime PublishedDate { get; private set; }
        public DateTime? EndDate { get; private set; }


    }
}

