using Infrastructure.Persistance.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factory
{
    public static class ArticleFactory
    {
        public static Article Create(
                                string title,
                                string summary,
                                string content,
                                Guid userId,
                                DateTime publishedDate,
                                DateTime? endDate,
                                string createdBy)
        {
            return new Article(title,summary,content,userId,publishedDate,null,createdBy);
        }

        public static Article Update(
                                Guid id,
                               string title,
                               string summary,
                               string content,
                               Guid userId,
                               DateTime? endDate,
                               string createdBy)
        {
            return new Article(id,title, summary, content, userId,  endDate, createdBy);
        }
    }
}
