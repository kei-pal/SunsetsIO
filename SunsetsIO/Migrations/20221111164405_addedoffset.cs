using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetsIO.Migrations
{
    public partial class addedoffset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimezoneOffsetSecs",
                table: "LocalWeather",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimezoneOffsetSecs",
                table: "LocalWeather");
        }
    }
}
