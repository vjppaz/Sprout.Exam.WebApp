using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprout.Exam.WebApp.Data.Migrations
{
    public partial class AddInitialEmployeeTypeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 1, "Regular" });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "TypeName" },
                values: new object[] { 2, "Contractual" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
