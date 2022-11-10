using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetsIO.Migrations
{
    public partial class localweatherid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeUtc",
                table: "LocalWeather",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SunsetUtc",
                table: "LocalWeather",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeUtc",
                table: "LocalWeather");

            migrationBuilder.DropColumn(
                name: "SunsetUtc",
                table: "LocalWeather");
        }
    }
}
