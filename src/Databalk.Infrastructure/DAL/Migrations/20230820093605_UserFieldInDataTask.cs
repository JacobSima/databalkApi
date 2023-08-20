using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Databalk.Infrastructure.dal.migrations
{
    /// <inheritdoc />
    public partial class UserFieldInDataTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Assignee",
                table: "DataTasks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "DataTasks");
        }
    }
}
