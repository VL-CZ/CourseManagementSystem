using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseManagementSystem.API.Data.Migrations
{
    public partial class Person_entity_updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_People_PersonID",
                table: "Grades");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "Grades",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_PersonID",
                table: "Grades",
                newName: "IX_Grades_PersonId");

            migrationBuilder.AlterColumn<string>(
                name: "PersonId",
                table: "Grades",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_PersonId",
                table: "Grades",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_PersonId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Grades",
                newName: "PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_PersonId",
                table: "Grades",
                newName: "IX_Grades_PersonID");

            migrationBuilder.AlterColumn<int>(
                name: "PersonID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_People_PersonID",
                table: "Grades",
                column: "PersonID",
                principalTable: "People",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
