using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class initial_0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "bomb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
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
                name: "controllerLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controllerLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "leaderboards",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
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
                    HostUserIDId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lobby", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    score = table.Column<long>(type: "bigint", nullable: true),
                    lives = table.Column<long>(type: "bigint", nullable: true),
                    powerUpId = table.Column<int>(type: "integer", nullable: true),
                    inLobby = table.Column<bool>(type: "boolean", nullable: true),
                    wins = table.Column<int>(type: "integer", nullable: true),
                    sessionIdId = table.Column<int>(type: "integer", nullable: true),
                    characterColor = table.Column<string>(type: "text", nullable: true),
                    lobbyId = table.Column<int>(type: "integer", nullable: true),
                    bombId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_Session_sessionIdId",
                        column: x => x.sessionIdId,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_bomb_bombId",
                        column: x => x.bombId,
                        principalTable: "bomb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_lobby_lobbyId",
                        column: x => x.lobbyId,
                        principalTable: "lobby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_powerup_powerUpId",
                        column: x => x.powerUpId,
                        principalTable: "powerup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bomb_userId",
                table: "bomb",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_PlayerId",
                table: "controllerLogs",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_leaderboards_UserId",
                table: "leaderboards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_lobby_HostUserIDId",
                table: "lobby",
                column: "HostUserIDId");

            migrationBuilder.CreateIndex(
                name: "IX_users_bombId",
                table: "users",
                column: "bombId");

            migrationBuilder.CreateIndex(
                name: "IX_users_lobbyId",
                table: "users",
                column: "lobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_users_powerUpId",
                table: "users",
                column: "powerUpId");

            migrationBuilder.CreateIndex(
                name: "IX_users_sessionIdId",
                table: "users",
                column: "sessionIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_bomb_users_userId",
                table: "bomb",
                column: "userId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_controllerLogs_users_PlayerId",
                table: "controllerLogs",
                column: "PlayerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_leaderboards_users_UserId",
                table: "leaderboards",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lobby_users_HostUserIDId",
                table: "lobby",
                column: "HostUserIDId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bomb_users_userId",
                table: "bomb");

            migrationBuilder.DropForeignKey(
                name: "FK_lobby_users_HostUserIDId",
                table: "lobby");

            migrationBuilder.DropTable(
                name: "controllerLogs");

            migrationBuilder.DropTable(
                name: "leaderboards");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "bomb");

            migrationBuilder.DropTable(
                name: "lobby");

            migrationBuilder.DropTable(
                name: "powerup");
        }
    }
}
