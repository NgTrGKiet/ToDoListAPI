using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start",
                table: "UserTasks",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "UserTasks",
                newName: "End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "UserTasks",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "UserTasks",
                newName: "end");
        }
    }
}
