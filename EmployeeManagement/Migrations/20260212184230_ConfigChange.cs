using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class ConfigChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesSkills_Employees_EmployeeId",
                table: "EmployeesSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesSkills_Skills_SkillId",
                table: "EmployeesSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeesSkills",
                table: "EmployeesSkills");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Skills",
                keyColumn: "SkillId",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "EmployeesSkills",
                newName: "EmployeeSkills");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesSkills_SkillId",
                table: "EmployeeSkills",
                newName: "IX_EmployeeSkills_SkillId");

            migrationBuilder.AlterColumn<string>(
                name: "SkillName",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeSkills",
                table: "EmployeeSkills",
                columns: new[] { "EmployeeId", "SkillId" });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillName",
                table: "Skills",
                column: "SkillName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSkills_Employees_EmployeeId",
                table: "EmployeeSkills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSkills_Skills_SkillId",
                table: "EmployeeSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSkills_Employees_EmployeeId",
                table: "EmployeeSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSkills_Skills_SkillId",
                table: "EmployeeSkills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillName",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeSkills",
                table: "EmployeeSkills");

            migrationBuilder.RenameTable(
                name: "EmployeeSkills",
                newName: "EmployeesSkills");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeSkills_SkillId",
                table: "EmployeesSkills",
                newName: "IX_EmployeesSkills_SkillId");

            migrationBuilder.AlterColumn<string>(
                name: "SkillName",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeesSkills",
                table: "EmployeesSkills",
                columns: new[] { "EmployeeId", "SkillId" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "HR" },
                    { 2, "IT" },
                    { 3, "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "SkillName" },
                values: new object[,]
                {
                    { 1, "C#" },
                    { 2, "SQL" },
                    { 3, "ASP.NET Core" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesSkills_Employees_EmployeeId",
                table: "EmployeesSkills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesSkills_Skills_SkillId",
                table: "EmployeesSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
