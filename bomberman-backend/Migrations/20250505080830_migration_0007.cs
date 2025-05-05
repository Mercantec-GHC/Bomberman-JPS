using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_0007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_lobby_lobbyId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_powerup_powerUpId",
                table: "users");

            migrationBuilder.AlterColumn<Guid>(
                name: "sessionIdId",
                table: "users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Session",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_users_lobby_lobbyId",
                table: "users",
                column: "lobbyId",
                principalTable: "lobby",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_powerup_powerUpId",
                table: "users",
                column: "powerUpId",
                principalTable: "powerup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_lobby_lobbyId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_powerup_powerUpId",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "sessionIdId",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Session",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_users_lobby_lobbyId",
                table: "users",
                column: "lobbyId",
                principalTable: "lobby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_powerup_powerUpId",
                table: "users",
                column: "powerUpId",
                principalTable: "powerup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
