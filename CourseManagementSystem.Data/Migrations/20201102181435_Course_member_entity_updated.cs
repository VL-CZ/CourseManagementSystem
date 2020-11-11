using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class Course_member_entity_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMemberships_AspNetUsers_StudentId",
                table: "CourseMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMembershipID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseMembershipID",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_CourseMemberships_StudentId",
                table: "CourseMemberships");

            migrationBuilder.DropColumn(
                name: "CourseMembershipID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "CourseMemberships");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CourseMemberships",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "CourseMemberId",
                table: "Grades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CourseMemberships",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseMemberId",
                table: "Grades",
                column: "CourseMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMemberships_UserId",
                table: "CourseMemberships",
                column: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseMemberships_AspNetUsers_UserId",
                table: "CourseMemberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_CourseMemberships_UserId",
                table: "CourseMemberships");

            migrationBuilder.DropColumn(
                name: "CourseMemberId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseMemberships");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CourseMemberships",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "CourseMembershipID",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "CourseMemberships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseMembershipID",
                table: "Grades",
                column: "CourseMembershipID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMemberships_StudentId",
                table: "CourseMemberships",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseMemberships_AspNetUsers_StudentId",
                table: "CourseMemberships",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMembershipID",
                table: "Grades",
                column: "CourseMembershipID",
                principalTable: "CourseMemberships",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
