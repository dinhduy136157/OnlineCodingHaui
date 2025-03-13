using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectID = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    LessonTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LessonContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "CodingExercises",
                columns: table => new
                {
                    ExerciseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgrammingLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExampleInput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExampleOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestCaseInput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestCaseOutput = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodingExercises", x => x.ExerciseID);
                    table.ForeignKey(
                        name: "FK_CodingExercises_Lessons_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lessons",
                        principalColumn: "LessonID");
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    SubmissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ExerciseID = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: true),
                    ExecutionTime = table.Column<int>(type: "int", nullable: true),
                    MemoryUsage = table.Column<float>(type: "real", nullable: true),
                    TestCasesPassed = table.Column<int>(type: "int", nullable: true),
                    TotalTestCases = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.SubmissionID);
                    table.ForeignKey(
                        name: "FK_Submissions_CodingExercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "CodingExercises",
                        principalColumn: "ExerciseID");
                    table.ForeignKey(
                        name: "FK_Submissions_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodingExercises_LessonID",
                table: "CodingExercises",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectID",
                table: "Lessons",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ExerciseID",
                table: "Submissions",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_StudentID",
                table: "Submissions",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "CodingExercises");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
