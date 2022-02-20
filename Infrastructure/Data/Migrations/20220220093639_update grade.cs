using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppMovie.Data.Migrations
{
    public partial class updategrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_UserIdId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_UserIdId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UserIdId",
                table: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Grades",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "UserIdId",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_UserIdId",
                table: "Grades",
                column: "UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_UserIdId",
                table: "Grades",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
