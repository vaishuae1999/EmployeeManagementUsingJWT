using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpTest.Migrations
{
    /// <inheritdoc />
    public partial class EmpDbMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "EmployeeTbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeTbl",
                table: "EmployeeTbl",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeTbl",
                table: "EmployeeTbl");

            migrationBuilder.RenameTable(
                name: "EmployeeTbl",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");
        }
    }
}
