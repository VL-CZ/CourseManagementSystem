using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class Course_member_renamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMemberships_Courses_CourseId",
                table: "CourseMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseMemberships_AspNetUsers_UserId",
                table: "CourseMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseMemberships",
                table: "CourseMemberships");

            migrationBuilder.RenameTable(
                name: "CourseMemberships",
                newName: "CourseMembers");

            migrationBuilder.RenameIndex(
                name: "IX_CourseMemberships_UserId",
                table: "CourseMembers",
                newName: "IX_CourseMembers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseMemberships_CourseId",
                table: "CourseMembers",
                newName: "IX_CourseMembers_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseMembers",
                table: "CourseMembers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMembers_Courses_CourseId",
                table: "CourseMembers",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMembers_AspNetUsers_UserId",
                table: "CourseMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMembers_CourseMemberId",
                table: "Grades",
                column: "CourseMemberId",
                principalTable: "CourseMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMembers_Courses_CourseId",
                table: "CourseMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseMembers_AspNetUsers_UserId",
                table: "CourseMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMembers_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseMembers",
                table: "CourseMembers");

            migrationBuilder.RenameTable(
                name: "CourseMembers",
                newName: "CourseMemberships");

            migrationBuilder.RenameIndex(
                name: "IX_CourseMembers_UserId",
                table: "CourseMemberships",
                newName: "IX_CourseMemberships_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseMembers_CourseId",
                table: "CourseMemberships",
                newName: "IX_CourseMemberships_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseMemberships",
                table: "CourseMemberships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMemberships_Courses_CourseId",
                table: "CourseMemberships",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMemberships_AspNetUsers_UserId",
                table: "CourseMemberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMemberId",
                table: "Grades",
                column: "CourseMemberId",
                principalTable: "CourseMemberships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
