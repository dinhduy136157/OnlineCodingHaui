using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addwrapmetatocodingexercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FunctionName",
                table: "CodingExercises",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParametersJson",
                table: "CodingExercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReturnType",
                table: "CodingExercises",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionName",
                table: "CodingExercises");

            migrationBuilder.DropColumn(
                name: "ParametersJson",
                table: "CodingExercises");

            migrationBuilder.DropColumn(
                name: "ReturnType",
                table: "CodingExercises");
        }
    }
}
