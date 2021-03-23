using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class AnswerComments_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TestSubmissionAnswers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TestSubmissionAnswers");
        }
    }
}
