using Microsoft.EntityFrameworkCore.Migrations;

namespace IceCream3.Migrations
{
    public partial class Price_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAdded",
                table: "Menu",
                newName: "DateAdded");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Menu",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Menu");

            migrationBuilder.RenameColumn(
                name: "DateAdded",
                table: "Menu",
                newName: "DataAdded");
        }
    }
}
