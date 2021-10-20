using Microsoft.EntityFrameworkCore.Migrations;

namespace IceCream3.Migrations
{
    public partial class Name2Flavor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Menu",
                newName: "Flavor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Flavor",
                table: "Menu",
                newName: "Name");
        }
    }
}
