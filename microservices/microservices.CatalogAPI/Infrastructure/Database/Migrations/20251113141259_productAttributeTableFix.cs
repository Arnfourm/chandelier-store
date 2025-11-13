using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.CatalogAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class productAttributeTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute",
                columns: new[] { "ProductId", "AttributeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductAttribute",
                table: "ProductAttribute");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                column: "ProductId");
        }
    }
}
