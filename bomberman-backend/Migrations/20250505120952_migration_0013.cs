using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_0013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users",
                column: "sessionIdId",
                principalTable: "Session",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users");

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
