using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databalk.Infrastructure.dal.migrations
{
    /// <inheritdoc />
    public partial class addrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Assignee",
                table: "DataTasks",
                newName: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataTasks_AssigneeId",
                table: "DataTasks",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DataTasks_Users_AssigneeId",
                table: "DataTasks",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataTasks_Users_AssigneeId",
                table: "DataTasks");

            migrationBuilder.DropIndex(
                name: "IX_DataTasks_AssigneeId",
                table: "DataTasks");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "DataTasks",
                newName: "Assignee");
        }
    }
}
