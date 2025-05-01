using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_leaderboards_users_UserId",
                table: "leaderboards");

            migrationBuilder.DropIndex(
                name: "IX_leaderboards_UserId",
                table: "leaderboards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "leaderboards");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "leaderboards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userName",
                table: "leaderboards");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "leaderboards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_leaderboards_UserId",
                table: "leaderboards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_leaderboards_users_UserId",
                table: "leaderboards",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
