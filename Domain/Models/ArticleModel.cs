using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public record ArticleModel(
    Guid Id,
    string Title,
    string Summary,
    DateTime PublishedDate,
    string AuthorName);

}
