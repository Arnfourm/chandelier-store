using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.CatalogAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class NewCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Product_ProductTypeId",
                table: "ProductAttribute");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "ProductAttribute",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttribute_ProductTypeId",
                table: "ProductAttribute",
                newName: "IX_ProductAttribute_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Product_ProductId",
                table: "ProductAttribute",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttribute_Product_ProductId",
                table: "ProductAttribute");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductAttribute",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                newName: "IX_ProductAttribute_ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttribute_Product_ProductTypeId",
                table: "ProductAttribute",
                column: "ProductTypeId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
