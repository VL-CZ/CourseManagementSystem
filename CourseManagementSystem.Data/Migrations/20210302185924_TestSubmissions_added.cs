using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.Data.Migrations
{
    public partial class TestSubmissions_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestSubmissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubmissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSubmissions_CourseMembers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "CourseMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestSubmissions_CourseTests_TestId",
                        column: x => x.TestId,
                        principalTable: "CourseTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestSubmissionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    TestSubmissionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSubmissionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestSubmissionAnswers_TestQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestSubmissionAnswers_TestSubmissions_TestSubmissionId",
                        column: x => x.TestSubmissionId,
                        principalTable: "TestSubmissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissionAnswers_QuestionId",
                table: "TestSubmissionAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissionAnswers_TestSubmissionId",
                table: "TestSubmissionAnswers",
                column: "TestSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissions_StudentId",
                table: "TestSubmissions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TestSubmissions_TestId",
                table: "TestSubmissions",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestSubmissionAnswers");

            migrationBuilder.DropTable(
                name: "TestSubmissions");
        }
    }
}
