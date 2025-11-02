using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.UserAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Password",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSaulHash = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Password", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    Registration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PasswordId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshTokenId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRole = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Password_PasswordId",
                        column: x => x.PasswordId,
                        principalTable: "Password",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_RefreshToken_RefreshTokenId",
                        column: x => x.RefreshTokenId,
                        principalTable: "RefreshToken",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryAddressCountry = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    DeliveryAddressDistrict = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DeliveryAddressCity = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    DeliveryAddressStreet = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DeliveryAddressHouse = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    DeliveryAddressPostalIndex = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Client_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    ResidenceAddressCountry = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: false),
                    ResidenceAddressDistrict = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ResidenceAddressCity = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    ResidenceAddressStreet = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ResidenceAddressHouse = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    ResidenceAddressPostalIndex = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Employee_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PasswordId",
                table: "User",
                column: "PasswordId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RefreshTokenId",
                table: "User",
                column: "RefreshTokenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Password");

            migrationBuilder.DropTable(
                name: "RefreshToken");
        }
    }
}
