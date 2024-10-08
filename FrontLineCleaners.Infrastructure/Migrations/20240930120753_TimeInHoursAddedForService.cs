using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontLineCleaners.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TimeInHoursAddedForService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeInHours",
                table: "Services",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeInHours",
                table: "Services");
        }
    }
}
