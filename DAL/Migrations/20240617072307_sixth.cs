using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class sixth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "UserTasks",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "UserTasks",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "UserTasks",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "UserTasks",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "UserTasks",
                newName: "Content");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "UserTasks",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "UserTasks",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "UserTasks",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserTasks",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "UserTasks",
                newName: "priority");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "UserTasks",
                newName: "content");

            migrationBuilder.AlterColumn<string>(
                name: "content",
                table: "UserTasks",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
