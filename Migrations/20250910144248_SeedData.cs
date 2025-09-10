using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceSchool.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "IdCourse", "AmountStudents", "IdTeacher", "NameCourse" },
                values: new object[] { 1, 0, 1, "Matemáticas" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "IdStudent", "ContactGuardian", "DocumentNumberStudent", "IdCourse", "NameGuardian", "NameStudent", "SurnameStuden" },
                values: new object[] { 1, "555-1234", "12345678", 1, "Luis García", "Ana", "García" });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "IdTeacher", "NameTeacher" },
                values: new object[] { 1, "Juan Pérez" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "IdCourse",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "IdStudent",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "IdTeacher",
                keyValue: 1);
        }
    }
}
