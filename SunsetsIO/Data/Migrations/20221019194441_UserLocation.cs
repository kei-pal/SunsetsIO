using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetsIO.Data.Migrations
{
    public partial class UserLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Rating");
        }
    }
}
