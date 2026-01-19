using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPP.Domain.Migrations
{
    /// <inheritdoc />
    public partial class asdkald : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks",
                column: "DependentTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks",
                column: "DependentTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
