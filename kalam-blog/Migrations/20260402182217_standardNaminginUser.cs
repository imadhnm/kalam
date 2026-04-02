using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kalam_blog.Migrations
{
    /// <inheritdoc />
    public partial class standardNaminginUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "squatter_flag",
                table: "AspNetUsers",
                newName: "SquatterFlag");

            migrationBuilder.RenameColumn(
                name: "member_status",
                table: "AspNetUsers",
                newName: "MemberStatus");

            migrationBuilder.RenameColumn(
                name: "default_domain",
                table: "AspNetUsers",
                newName: "DefaultDomain");

            migrationBuilder.RenameColumn(
                name: "custom_domain",
                table: "AspNetUsers",
                newName: "CustomDomain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SquatterFlag",
                table: "AspNetUsers",
                newName: "squatter_flag");

            migrationBuilder.RenameColumn(
                name: "MemberStatus",
                table: "AspNetUsers",
                newName: "member_status");

            migrationBuilder.RenameColumn(
                name: "DefaultDomain",
                table: "AspNetUsers",
                newName: "default_domain");

            migrationBuilder.RenameColumn(
                name: "CustomDomain",
                table: "AspNetUsers",
                newName: "custom_domain");
        }
    }
}
