using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetsIO.Migrations
{
    public partial class addweatherjsonstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "LocalWeather",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "LocalWeather",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "LocalWeather");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "LocalWeather");
        }
    }
}
