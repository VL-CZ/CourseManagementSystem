using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class ReferencesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMembers_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseMemberId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CourseMemberId",
                table: "Grades");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Grades",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMembers_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "CourseMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMembers_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grades");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseMemberId",
                table: "Grades",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseMemberId",
                table: "Grades",
                column: "CourseMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMembers_CourseMemberId",
                table: "Grades",
                column: "CourseMemberId",
                principalTable: "CourseMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
