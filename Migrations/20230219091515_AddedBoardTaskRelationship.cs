using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedBoardTaskRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Boards_BoardId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Tasks");
        }
    }
}
