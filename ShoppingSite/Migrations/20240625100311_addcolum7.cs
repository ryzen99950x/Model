using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingSite.Migrations
{
    /// <inheritdoc />
    public partial class addcolum7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DupCount",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CartsId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CartsId",
                table: "Carts",
                column: "CartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Carts_CartsId",
                table: "Carts",
                column: "CartsId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Carts_CartsId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CartsId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartsId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "DupCount",
                table: "Carts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
