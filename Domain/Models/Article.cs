using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.DataModels
{

    public class Article : BaseModel
    {

        public string Title { get; private set; } = null!;
        public string Summary { get; private set; } = null!;
        public string Content { get; private set; } = null!;
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public DateTime PublishedDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Article()
        {

        }
        public Article(string title, string summary, string content, Guid userId, DateTime publishedDate, DateTime? endDate, string createdBy)
        {
            Title = title;
            Summary = summary;
            Content = content;
            UserId = userId;
            PublishedDate = publishedDate;
            EndDate = endDate;
            IsActive = true;

        }
        public Article(Guid id, string title, string summary, string content, Guid userId, DateTime? endDate, string createdBy)
        {
            Id = id;
            LastModifiedUser = createdBy;
            Title = title;
            Summary = summary;
            Content = content;
            UserId = userId;
            EndDate = endDate;
            LastModifiedDate = DateTime.Now;
        }

        public void Update(
                string title,
                string summary,
                string content,
                DateTime? endDate,
                string modifiedBy)
        {
            Title = title;
            Summary = summary;
            Content = content;
            EndDate = endDate;

            LastModifiedDate = DateTime.UtcNow;
            LastModifiedUser = modifiedBy;
        }

    }
}

