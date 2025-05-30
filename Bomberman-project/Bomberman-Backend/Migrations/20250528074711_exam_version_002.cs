using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class exam_version_002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buttons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buttons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gyroscope",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    xCordinate = table.Column<float>(type: "real", nullable: false),
                    yCordinate = table.Column<float>(type: "real", nullable: false),
                    zCordinate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyroscope", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controllers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    playerColor = table.Column<string>(type: "text", nullable: false),
                    ledBrightness = table.Column<float>(type: "real", nullable: false),
                    playerId = table.Column<Guid>(type: "uuid", nullable: false),
                    gyroScopeId = table.Column<int>(type: "integer", nullable: false),
                    buttonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controllers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controllers_Buttons_buttonsId",
                        column: x => x.buttonsId,
                        principalTable: "Buttons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Controllers_Gyroscope_gyroScopeId",
                        column: x => x.gyroScopeId,
                        principalTable: "Gyroscope",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Controllers_players_playerId",
                        column: x => x.playerId,
                        principalTable: "players",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_buttonsId",
                table: "Controllers",
                column: "buttonsId");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_gyroScopeId",
                table: "Controllers",
                column: "gyroScopeId");

            migrationBuilder.CreateIndex(
                name: "IX_Controllers_playerId",
                table: "Controllers",
                column: "playerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Controllers");

            migrationBuilder.DropTable(
                name: "Buttons");

            migrationBuilder.DropTable(
                name: "Gyroscope");
        }
    }
}
