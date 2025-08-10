using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoApp.Migrations
{
    /// <inheritdoc />
    public partial class PascalCasingIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "TodoItems",
                newName: "IsCompleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "TodoItems",
                newName: "isCompleted");
        }
    }
}
