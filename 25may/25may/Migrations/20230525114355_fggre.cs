using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _25may.Migrations
{
    public partial class fggre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "team",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessionId",
                table: "team",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_team_Professions_ProfessionId",
                table: "team",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");
        }
    }
}
