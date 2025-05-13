using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class add_tokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bomb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    yCordinate = table.Column<string>(type: "text", nullable: false),
                    xCordinate = table.Column<string>(type: "text", nullable: false),
                    explosionRadius = table.Column<int>(type: "integer", nullable: false),
                    fuseTime = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bomb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InputType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inputID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InputType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leaderboards",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userName = table.Column<string>(type: "text", nullable: false),
                    totalGames = table.Column<int>(type: "integer", nullable: false),
                    totalWins = table.Column<int>(type: "integer", nullable: false),
                    hightScore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_leaderboards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lobby",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    HostUserID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobby", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "powerup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Effect = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_powerup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<long>(type: "bigint", nullable: false),
                    lives = table.Column<long>(type: "bigint", nullable: false),
                    powerUpId = table.Column<int>(type: "integer", nullable: true),
                    inLobby = table.Column<bool>(type: "boolean", nullable: false),
                    wins = table.Column<int>(type: "integer", nullable: false),
                    sessionIdId = table.Column<int>(type: "integer", nullable: false),
                    characterColor = table.Column<string>(type: "text", nullable: false),
                    lobbyId = table.Column<int>(type: "integer", nullable: true),
                    bombId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_players_Session_sessionIdId",
                        column: x => x.sessionIdId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_players_bomb_bombId",
                        column: x => x.bombId,
                        principalTable: "bomb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_players_lobby_lobbyId",
                        column: x => x.lobbyId,
                        principalTable: "lobby",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_players_powerup_powerUpId",
                        column: x => x.powerUpId,
                        principalTable: "powerup",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_players_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refreshTokens_users_Id",
                        column: x => x.Id,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "controllerLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InputTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controllerLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_controllerLogs_InputType_InputTypeId",
                        column: x => x.InputTypeId,
                        principalTable: "InputType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_controllerLogs_players_PlayerUserId",
                        column: x => x.PlayerUserId,
                        principalTable: "players",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_InputTypeId",
                table: "controllerLogs",
                column: "InputTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_PlayerUserId",
                table: "controllerLogs",
                column: "PlayerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_players_bombId",
                table: "players",
                column: "bombId");

            migrationBuilder.CreateIndex(
                name: "IX_players_lobbyId",
                table: "players",
                column: "lobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_players_powerUpId",
                table: "players",
                column: "powerUpId");

            migrationBuilder.CreateIndex(
                name: "IX_players_sessionIdId",
                table: "players",
                column: "sessionIdId");

            migrationBuilder.CreateIndex(
                name: "IX_refreshTokens_Token",
                table: "refreshTokens",
                column: "Token",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "controllerLogs");

            migrationBuilder.DropTable(
                name: "leaderboards");

            migrationBuilder.DropTable(
                name: "refreshTokens");

            migrationBuilder.DropTable(
                name: "InputType");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "bomb");

            migrationBuilder.DropTable(
                name: "lobby");

            migrationBuilder.DropTable(
                name: "powerup");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
