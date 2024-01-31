using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class DeleteStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ToDos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ToDos",
                type: "nvarchar",
                nullable: true);
        }
    }
}
