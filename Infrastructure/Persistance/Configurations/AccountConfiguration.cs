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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Password)
                .IsRequired();

            builder.Property(a => a.RefreshToken)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(a => a.OTP)
                .HasMaxLength(50)
                .IsRequired(false);

            // BaseModel
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.LastModifiedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.LastModifiedUser)
                .HasMaxLength(150)
                .IsRequired();

            // Relationships
            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Account>(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
