using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_0005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bomb_users_userId",
                table: "bomb");

            migrationBuilder.DropIndex(
                name: "IX_bomb_userId",
                table: "bomb");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "bomb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "bomb",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_bomb_userId",
                table: "bomb",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_bomb_users_userId",
                table: "bomb",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
