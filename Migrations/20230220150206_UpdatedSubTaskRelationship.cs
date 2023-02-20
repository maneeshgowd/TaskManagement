using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSubTaskRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "SubTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColumnId",
                table: "SubTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SubTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_BoardId",
                table: "SubTasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_ColumnId",
                table: "SubTasks",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_SubTasks_UserId",
                table: "SubTasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Boards_BoardId",
                table: "SubTasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Columns_ColumnId",
                table: "SubTasks",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTasks_Users_UserId",
                table: "SubTasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Boards_BoardId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Columns_ColumnId",
                table: "SubTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_SubTasks_Users_UserId",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_BoardId",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_ColumnId",
                table: "SubTasks");

            migrationBuilder.DropIndex(
                name: "IX_SubTasks_UserId",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "SubTasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SubTasks");
        }
    }
}
