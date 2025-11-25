using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace microservices.UserAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshToken",
                table: "RefreshToken",
                newName: "Token");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Password",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshToken",
                newName: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Password",
                newName: "id");
        }
    }
}
