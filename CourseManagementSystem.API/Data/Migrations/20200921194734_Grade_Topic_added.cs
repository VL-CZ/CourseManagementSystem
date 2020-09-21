using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.API.Data.Migrations
{
    public partial class Grade_Topic_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Grades",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Grades");
        }
    }
}
