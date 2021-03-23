using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class TestWeight_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreWeight",
                table: "CourseTests");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "CourseTests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "CourseTests");

            migrationBuilder.AddColumn<int>(
                name: "ScoreWeight",
                table: "CourseTests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
