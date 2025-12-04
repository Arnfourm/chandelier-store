using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.CatalogAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProductLampAttrAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LampCount",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LampPower",
                table: "Product",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LampCount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LampPower",
                table: "Product");
        }
    }
}
