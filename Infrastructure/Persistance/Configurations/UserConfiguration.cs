using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(u => u.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Surname)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(u => u.CellNo)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(u => u.IdNumber)
                .HasMaxLength(20)
                .IsRequired(false);

            // BaseModel properties
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.LastModifiedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(u => u.LastModifiedUser)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
