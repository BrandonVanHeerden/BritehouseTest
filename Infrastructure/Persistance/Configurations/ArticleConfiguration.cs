using Infrastructure.Persistance.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{


    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");

            builder.HasKey(a => a.Id);
            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(a => a.Title)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(a => a.Summary)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.PublishedDate).IsRequired();
            builder.Property(a => a.EndDate).IsRequired(false);

            // BaseModel
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.LastModifiedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.LastModifiedUser)
                .HasMaxLength(150)
                .IsRequired();

            // Relationship: User -> Articles
            builder.HasOne(a => a.User)
                .WithMany(u => u.Articles)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => a.PublishedDate);
        }
    }
}
