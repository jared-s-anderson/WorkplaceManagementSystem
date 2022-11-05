using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkplaceManagementSystem.Data.Migrations
{
    public partial class ModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "TaskId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rewards",
                newName: "RewardId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Info",
                newName: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Tasks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RewardId",
                table: "Rewards",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Info",
                newName: "Id");
        }
    }
}
