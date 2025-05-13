using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class add_tokens2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshTokens_users_Id",
                table: "refreshTokens");

            migrationBuilder.CreateIndex(
                name: "IX_refreshTokens_UserId",
                table: "refreshTokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshTokens_users_UserId",
                table: "refreshTokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refreshTokens_users_UserId",
                table: "refreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_refreshTokens_UserId",
                table: "refreshTokens");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshTokens_users_Id",
                table: "refreshTokens",
                column: "Id",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
