using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskApi.Migrations
{
    public partial class ChangedNameOfDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskEntity",
                table: "TaskEntity");

            migrationBuilder.RenameTable(
                name: "TaskEntity",
                newName: "Tasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "TaskEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskEntity",
                table: "TaskEntity",
                column: "Id");
        }
    }
}
