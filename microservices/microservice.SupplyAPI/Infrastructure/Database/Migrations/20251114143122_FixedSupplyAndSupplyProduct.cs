using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservice.SupplyAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixedSupplyAndSupplyProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supply_Supply_SupplierId",
                table: "Supply");

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_Supplier_SupplierId",
                table: "Supply",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supply_Supplier_SupplierId",
                table: "Supply");

            migrationBuilder.AddForeignKey(
                name: "FK_Supply_Supply_SupplierId",
                table: "Supply",
                column: "SupplierId",
                principalTable: "Supply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
