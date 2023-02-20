using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAttributeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Columns_BoardColumnId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "BoardColumnId",
                table: "Tasks",
                newName: "ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_BoardColumnId",
                table: "Tasks",
                newName: "IX_Tasks_ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Columns_ColumnId",
                table: "Tasks",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Columns_ColumnId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "ColumnId",
                table: "Tasks",
                newName: "BoardColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ColumnId",
                table: "Tasks",
                newName: "IX_Tasks_BoardColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Columns_BoardColumnId",
                table: "Tasks",
                column: "BoardColumnId",
                principalTable: "Columns",
                principalColumn: "Id");
        }
    }
}
