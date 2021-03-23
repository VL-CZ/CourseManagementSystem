using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class Grade_weight_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Grades",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Grades",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Grades",
                newName: "ID");
        }
    }
}
