using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kalam_blog.Migrations
{
    /// <inheritdoc />
    public partial class custom_domain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "blog_default",
                table: "AspNetUsers",
                newName: "default_domain");

            migrationBuilder.RenameColumn(
                name: "blog_custom",
                table: "AspNetUsers",
                newName: "custom_domain");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "default_domain",
                table: "AspNetUsers",
                newName: "blog_default");

            migrationBuilder.RenameColumn(
                name: "custom_domain",
                table: "AspNetUsers",
                newName: "blog_custom");
        }
    }
}
