using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class ChangeSomeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipRank",
                table: "Ships",
                newName: "Rank");

            migrationBuilder.RenameColumn(
                name: "ShipDirection",
                table: "Ships",
                newName: "Direction");

            migrationBuilder.RenameColumn(
                name: "GameStatus",
                table: "Games",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "CellStatus",
                table: "Cells",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rank",
                table: "Ships",
                newName: "ShipRank");

            migrationBuilder.RenameColumn(
                name: "Direction",
                table: "Ships",
                newName: "ShipDirection");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Games",
                newName: "GameStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Cells",
                newName: "CellStatus");
        }
    }
}
