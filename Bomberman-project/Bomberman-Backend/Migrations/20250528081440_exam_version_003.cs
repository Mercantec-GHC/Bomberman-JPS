using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bomberman_Backend.Migrations
{
    /// <inheritdoc />
    public partial class exam_version_003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Controllers_Gyroscope_gyroScopeId",
                table: "Controllers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gyroscope",
                table: "Gyroscope");

            migrationBuilder.RenameTable(
                name: "Gyroscope",
                newName: "Gyroscopes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gyroscopes",
                table: "Gyroscopes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Buttons_Id",
                table: "Buttons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gyroscopes_Id",
                table: "Gyroscopes",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Controllers_Gyroscopes_gyroScopeId",
                table: "Controllers",
                column: "gyroScopeId",
                principalTable: "Gyroscopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Controllers_Gyroscopes_gyroScopeId",
                table: "Controllers");

            migrationBuilder.DropIndex(
                name: "IX_Buttons_Id",
                table: "Buttons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gyroscopes",
                table: "Gyroscopes");

            migrationBuilder.DropIndex(
                name: "IX_Gyroscopes_Id",
                table: "Gyroscopes");

            migrationBuilder.RenameTable(
                name: "Gyroscopes",
                newName: "Gyroscope");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gyroscope",
                table: "Gyroscope",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Controllers_Gyroscope_gyroScopeId",
                table: "Controllers",
                column: "gyroScopeId",
                principalTable: "Gyroscope",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
