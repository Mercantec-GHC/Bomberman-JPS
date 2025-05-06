using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_0011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "sessionIdId",
                table: "users",
                newName: "sessionId");

            migrationBuilder.RenameIndex(
                name: "IX_users_sessionIdId",
                table: "users",
                newName: "IX_users_sessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Session_sessionId",
                table: "users",
                column: "sessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Session_sessionId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "sessionId",
                table: "users",
                newName: "sessionIdId");

            migrationBuilder.RenameIndex(
                name: "IX_users_sessionId",
                table: "users",
                newName: "IX_users_sessionIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users",
                column: "sessionIdId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
