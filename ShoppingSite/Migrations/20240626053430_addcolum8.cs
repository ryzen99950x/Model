using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingSite.Migrations
{
    /// <inheritdoc />
    public partial class addcolum8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreditId",
                table: "AspNetUsers");
        }
    }
}
