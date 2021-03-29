using Microsoft.EntityFrameworkCore.Migrations;

namespace SupportApp.Data.Migrations
{
    public partial class CreatedMentorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Mentors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Mentors_UserId",
                table: "Mentors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_Users_UserId",
                table: "Mentors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_Users_UserId",
                table: "Mentors");

            migrationBuilder.DropIndex(
                name: "IX_Mentors_UserId",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Mentors");
        }
    }
}
