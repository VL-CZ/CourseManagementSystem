using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class TestQuestion_NavigationProperties_required : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CourseTestId",
                table: "TestQuestions",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CourseTestId",
                table: "TestQuestions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));
        }
    }
}
