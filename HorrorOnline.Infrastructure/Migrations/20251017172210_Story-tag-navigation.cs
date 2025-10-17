using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HorrorOnline.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Storytagnavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Stories_StoryId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_StoryId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Stories_ReviewId",
                table: "Reviews",
                column: "ReviewId",
                principalTable: "Stories",
                principalColumn: "StoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Stories_ReviewId",
                table: "Reviews");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_StoryId",
                table: "Reviews",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Stories_StoryId",
                table: "Reviews",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId");
        }
    }
}
