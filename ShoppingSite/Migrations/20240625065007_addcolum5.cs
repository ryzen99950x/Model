using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingSite.Migrations
{
    /// <inheritdoc />
    public partial class addcolum5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DupCount",
                table: "Carts",
                type: "int", 
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DupCount",
                table: "Carts");

        }
    }
}
