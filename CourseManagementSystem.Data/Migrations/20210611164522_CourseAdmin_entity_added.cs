using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class CourseAdmin_entity_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AdminId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AdminId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CourseAdmins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAdmins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseAdmins_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseAdmins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseAdmins_CourseId",
                table: "CourseAdmins",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseAdmins_UserId",
                table: "CourseAdmins",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseAdmins");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AdminId",
                table: "Courses",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AdminId",
                table: "Courses",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
