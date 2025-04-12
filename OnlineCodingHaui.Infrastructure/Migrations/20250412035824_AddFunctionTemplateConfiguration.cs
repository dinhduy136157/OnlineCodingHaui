using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFunctionTemplateConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FunctionTemplates",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseID = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FunctionTemplateContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodingExerciseExerciseID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionTemplates", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_FunctionTemplates_CodingExercises_CodingExerciseExerciseID",
                        column: x => x.CodingExerciseExerciseID,
                        principalTable: "CodingExercises",
                        principalColumn: "ExerciseID");
                    table.ForeignKey(
                        name: "FK_FunctionTemplates_CodingExercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "CodingExercises",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionTemplates_CodingExerciseExerciseID",
                table: "FunctionTemplates",
                column: "CodingExerciseExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionTemplates_ExerciseID",
                table: "FunctionTemplates",
                column: "ExerciseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FunctionTemplates");
        }
    }
}
