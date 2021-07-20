using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class TestIsSubmitted_field_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "TestSubmissions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "TestSubmissions");
        }
    }
}
