using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCodingHaui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addStudentCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Bước 1: Thêm cột, cho phép NULL tạm thời
            migrationBuilder.AddColumn<string>(
                name: "StudentCode",
                table: "Student",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true); // Cho phép NULL

            // Bước 2: Cập nhật giá trị duy nhất cho các bản ghi hiện có
            migrationBuilder.Sql(@"
        UPDATE s SET s.StudentCode = 'STU' + CAST(s.StudentID AS VARCHAR(10))
        FROM Student s
        WHERE s.StudentCode IS NULL
    ");

            // Bước 3: Đổi thành NOT NULL
            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "Student",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            // Bước 4: Tạo unique index
            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentCode",
                table: "Student",
                column: "StudentCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Student_StudentCode",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentCode",
                table: "Student");
        }
    }
}
