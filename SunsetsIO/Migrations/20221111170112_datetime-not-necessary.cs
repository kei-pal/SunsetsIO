using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunsetsIO.Migrations
{
    public partial class datetimenotnecessary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeUtc",
                table: "LocalWeather");

            migrationBuilder.RenameColumn(
                name: "DateTimeRated",
                table: "Rating",
                newName: "DateTimeRatedUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTimeRatedUtc",
                table: "Rating",
                newName: "DateTimeRated");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeUtc",
                table: "LocalWeather",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
