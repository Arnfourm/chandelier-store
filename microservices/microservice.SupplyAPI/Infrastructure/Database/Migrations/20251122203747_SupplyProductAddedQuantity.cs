using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservice.SupplyAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SupplyProductAddedQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SupplyProduct",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SupplyProduct");
        }
    }
}
