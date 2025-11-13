using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace microservice.SupplyAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    Comment = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supply",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplyDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supply_Supply_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeliveryTypeId = table.Column<int>(type: "integer", nullable: false),
                    eliveryTypeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_DeliveryType_eliveryTypeId",
                        column: x => x.eliveryTypeId,
                        principalTable: "DeliveryType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplyProduct",
                columns: table => new
                {
                    SupplyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyProduct", x => new { x.SupplyId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_SupplyProduct_Supply_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supply",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_eliveryTypeId",
                table: "Supplier",
                column: "eliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Supply_SupplierId",
                table: "Supply",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "SupplyProduct");

            migrationBuilder.DropTable(
                name: "DeliveryType");

            migrationBuilder.DropTable(
                name: "Supply");
        }
    }
}
