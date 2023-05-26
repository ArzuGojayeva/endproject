using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _25may.Migrations
{
    public partial class Professionss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_Professions_ProfessionId1",
                table: "team");

            migrationBuilder.DropIndex(
                name: "IX_team_ProfessionId1",
                table: "team");

            migrationBuilder.DropColumn(
                name: "ProfessionId1",
                table: "team");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "team",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_team_ProfessionId",
                table: "team",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team");

            migrationBuilder.DropIndex(
                name: "IX_team_ProfessionId",
                table: "team");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessionId",
                table: "team",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId1",
                table: "team",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_team_ProfessionId1",
                table: "team",
                column: "ProfessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_team_Professions_ProfessionId1",
                table: "team",
                column: "ProfessionId1",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
