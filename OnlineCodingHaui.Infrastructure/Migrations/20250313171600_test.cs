using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Student",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "CodingExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.TeacherID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TeacherID",
                table: "Lessons",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_CodingExercises_TeacherID",
                table: "CodingExercises",
                column: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_CodingExercises_Teacher_TeacherID",
                table: "CodingExercises",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "TeacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teacher_TeacherID",
                table: "Lessons",
                column: "TeacherID",
                principalTable: "Teacher",
                principalColumn: "TeacherID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodingExercises_Teacher_TeacherID",
                table: "CodingExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teacher_TeacherID",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_TeacherID",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_CodingExercises_TeacherID",
                table: "CodingExercises");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "CodingExercises");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Student",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectID",
                table: "Lessons",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }
    }
}
