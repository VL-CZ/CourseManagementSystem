using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class CourseTest_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "TestSubmissions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "TestSubmissions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseTests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions",
                column: "StudentId",
                principalTable: "CourseMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions",
                column: "TestId",
                principalTable: "CourseTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions");

            migrationBuilder.AlterColumn<int>(
                name: "TestId",
                table: "TestSubmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "TestSubmissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseTests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CourseTests_Courses_CourseId",
                table: "CourseTests",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions",
                column: "StudentId",
                principalTable: "CourseMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions",
                column: "TestId",
                principalTable: "CourseTests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
