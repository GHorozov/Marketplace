using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class ChangeMessageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Messages",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MessageContent",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageContent",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Messages",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Messages",
                newName: "Content");
        }
    }
}
