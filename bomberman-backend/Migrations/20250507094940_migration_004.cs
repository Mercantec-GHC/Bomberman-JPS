using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bomberman_backend.Migrations
{
    /// <inheritdoc />
    public partial class migration_004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_controllerLogs_users_PlayerId",
                table: "controllerLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_bomb_bombId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_lobby_lobbyId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_users_powerup_powerUpId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_bombId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_lobbyId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_powerUpId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_sessionIdId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "users");

            migrationBuilder.DropColumn(
                name: "bombId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "characterColor",
                table: "users");

            migrationBuilder.DropColumn(
                name: "inLobby",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lives",
                table: "users");

            migrationBuilder.DropColumn(
                name: "lobbyId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "powerUpId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "score",
                table: "users");

            migrationBuilder.DropColumn(
                name: "sessionIdId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "wins",
                table: "users");

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_players", x => x.Id);
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
                        name: "FK_players_users_Id",
                        column: x => x.Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_controllerLogs_players_PlayerId",
                table: "controllerLogs",
                column: "PlayerId",
                principalTable: "players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_controllerLogs_players_PlayerId",
                table: "controllerLogs");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "users",
                type: "character varying(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "bombId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "characterColor",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "inLobby",
                table: "users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "lives",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lobbyId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "powerUpId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "score",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sessionIdId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "wins",
                table: "users",
                type: "integer",
                nullable: true);

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
                name: "FK_controllerLogs_users_PlayerId",
                table: "controllerLogs",
                column: "PlayerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_Session_sessionIdId",
                table: "users",
                column: "sessionIdId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_bomb_bombId",
                table: "users",
                column: "bombId",
                principalTable: "bomb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
