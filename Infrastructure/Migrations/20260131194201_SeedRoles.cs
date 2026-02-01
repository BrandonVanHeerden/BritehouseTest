using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "IsActive", "LastModifiedUser" },
                values: new object[,]
                {
            {
                RoleConfiguration.AdminRoleId,
                "Admin",
                true,
                "system"
            },
            {
                RoleConfiguration.AuthorRoleId,
                "Author",
                true,
                "system"
            },
            {
                RoleConfiguration.ReaderRoleId,
                "Reader",
                true,
                "system"
            }
                });
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: RoleConfiguration.AdminRoleId);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: RoleConfiguration.AuthorRoleId);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: RoleConfiguration.ReaderRoleId);
        }

    }
}
