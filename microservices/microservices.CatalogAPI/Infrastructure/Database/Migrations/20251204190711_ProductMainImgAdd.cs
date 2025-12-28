using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.CatalogAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProductMainImgAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImgPath",
                table: "Product",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImgPath",
                table: "Product");
        }
    }
}
