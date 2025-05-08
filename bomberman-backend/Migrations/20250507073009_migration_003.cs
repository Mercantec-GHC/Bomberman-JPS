using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lobby_users_HostUserIDId",
                table: "lobby");

            migrationBuilder.DropIndex(
                name: "IX_lobby_HostUserIDId",
                table: "lobby");

            migrationBuilder.DropColumn(
                name: "HostUserIDId",
                table: "lobby");

            migrationBuilder.AddColumn<Guid>(
                name: "HostUserID",
                table: "lobby",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HostUserID",
                table: "lobby");

            migrationBuilder.AddColumn<int>(
                name: "HostUserIDId",
                table: "lobby",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_lobby_HostUserIDId",
                table: "lobby",
                column: "HostUserIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_lobby_users_HostUserIDId",
                table: "lobby",
                column: "HostUserIDId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
