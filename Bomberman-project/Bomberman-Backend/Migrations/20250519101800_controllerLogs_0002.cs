using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class controllerLogs_0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_controllerLogs_players_PlayerUserId",
                table: "controllerLogs");

            migrationBuilder.DropIndex(
                name: "IX_controllerLogs_PlayerUserId",
                table: "controllerLogs");

            migrationBuilder.DropColumn(
                name: "PlayerUserId",
                table: "controllerLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerID",
                table: "controllerLogs",
                type: "uuid",
                maxLength: 200,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_PlayerID",
                table: "controllerLogs",
                column: "PlayerID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_controllerLogs_players_PlayerID",
                table: "controllerLogs",
                column: "PlayerID",
                principalTable: "players",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_controllerLogs_players_PlayerID",
                table: "controllerLogs");

            migrationBuilder.DropIndex(
                name: "IX_controllerLogs_PlayerID",
                table: "controllerLogs");

            migrationBuilder.DropColumn(
                name: "PlayerID",
                table: "controllerLogs");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerUserId",
                table: "controllerLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_PlayerUserId",
                table: "controllerLogs",
                column: "PlayerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_controllerLogs_players_PlayerUserId",
                table: "controllerLogs",
                column: "PlayerUserId",
                principalTable: "players",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
