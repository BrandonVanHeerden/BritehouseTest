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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public static readonly Guid AdminRoleId =
         Guid.Parse("55676D85-48D3-4995-B827-790FC0634600");

        public static readonly Guid AuthorRoleId =
            Guid.Parse("CBC3B84E-BEAC-40B7-B31D-0C2FBC527148");

        public static readonly Guid ReaderRoleId =
            Guid.Parse("5F78F802-1FA2-4AF6-BC81-269AA1B5181A");
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);
            builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();
            builder.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(r => r.IsActive).IsRequired();
            builder.Property(r => r.LastModifiedDate)
                .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(r => r.LastModifiedUser)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
