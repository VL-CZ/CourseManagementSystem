using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class Courses_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_PersonId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_PersonId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Grades");

            migrationBuilder.AddColumn<int>(
                name: "CourseMembershipID",
                table: "Grades",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    AdminId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseMemberships",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMemberships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CourseMemberships_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseMemberships_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseMembershipID",
                table: "Grades",
                column: "CourseMembershipID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMemberships_CourseId",
                table: "CourseMemberships",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseMemberships_StudentId",
                table: "CourseMemberships",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AdminId",
                table: "Courses",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMembershipID",
                table: "Grades",
                column: "CourseMembershipID",
                principalTable: "CourseMemberships",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_CourseMemberships_CourseMembershipID",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "CourseMemberships");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseMembershipID",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "CourseMembershipID",
                table: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "PersonId",
                table: "Grades",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_PersonId",
                table: "Grades",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_PersonId",
                table: "Grades",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
