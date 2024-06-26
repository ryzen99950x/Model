using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingSite.Migrations
{
    /// <inheritdoc />
    public partial class addcolum11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CardNum",
                table: "Credits",
                newName: "CardNum2");

            migrationBuilder.AddColumn<int>(
                name: "CardNum1",
                table: "Credits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNum1",
                table: "Credits");

            migrationBuilder.RenameColumn(
                name: "CardNum2",
                table: "Credits",
                newName: "CardNum");
        }
    }
}
