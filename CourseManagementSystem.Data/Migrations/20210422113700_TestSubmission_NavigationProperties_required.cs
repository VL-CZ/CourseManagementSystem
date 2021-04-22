using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class TestSubmission_NavigationProperties_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "TestSubmissionAnswers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseMembers_StudentId",
                table: "TestSubmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSubmissions_CourseTests_TestId",
                table: "TestSubmissions");

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "TestSubmissionAnswers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

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
    }
}
