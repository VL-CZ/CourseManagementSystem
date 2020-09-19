using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class Entitiesupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_People_StudentID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Grades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_PersonID",
                table: "Grades",
                column: "PersonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_People_PersonID",
                table: "Grades",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_People_PersonID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_PersonID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "People",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentID",
                table: "Grades",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_People_StudentID",
                table: "Grades",
                column: "StudentID",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
