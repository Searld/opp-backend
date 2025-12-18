using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPP.Domain.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Subject_SubjectId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DepencentTaskId",
                table: "ProjectTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameColumn(
                name: "DepencentTaskId",
                table: "ProjectTasks",
                newName: "DependentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_DepencentTaskId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_DependentTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Subjects_SubjectId",
                table: "Projects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks",
                column: "DependentTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Subjects_SubjectId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DependentTaskId",
                table: "ProjectTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "DependentTaskId",
                table: "ProjectTasks",
                newName: "DepencentTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTasks_DependentTaskId",
                table: "ProjectTasks",
                newName: "IX_ProjectTasks_DepencentTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Subject_SubjectId",
                table: "Projects",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectTasks_DepencentTaskId",
                table: "ProjectTasks",
                column: "DepencentTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
