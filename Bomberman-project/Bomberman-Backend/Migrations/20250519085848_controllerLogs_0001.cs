using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class controllerLogs_0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_controllerLogs_InputType_InputTypeId",
                table: "controllerLogs");

            migrationBuilder.DropTable(
                name: "InputType");

            migrationBuilder.DropIndex(
                name: "IX_controllerLogs_InputTypeId",
                table: "controllerLogs");

            migrationBuilder.DropColumn(
                name: "InputTypeId",
                table: "controllerLogs");

            migrationBuilder.AddColumn<string>(
                name: "InputType",
                table: "controllerLogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputType",
                table: "controllerLogs");

            migrationBuilder.AddColumn<int>(
                name: "InputTypeId",
                table: "controllerLogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_controllerLogs_InputTypeId",
                table: "controllerLogs",
                column: "InputTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_controllerLogs_InputType_InputTypeId",
                table: "controllerLogs",
                column: "InputTypeId",
                principalTable: "InputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
