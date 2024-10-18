using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontLineCleaners.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Cleaners",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            //Added this manually for the update-database script to pass
            migrationBuilder.Sql("UPDATE Cleaners " +
                "SET OwnerId = (SELECT TOP 1 Id FROM AspNetUsers)");

            migrationBuilder.CreateIndex(
                name: "IX_Cleaners_OwnerId",
                table: "Cleaners",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cleaners_AspNetUsers_OwnerId",
                table: "Cleaners",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cleaners_AspNetUsers_OwnerId",
                table: "Cleaners");

            migrationBuilder.DropIndex(
                name: "IX_Cleaners_OwnerId",
                table: "Cleaners");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cleaners");
        }
    }
}
