using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Addednewproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "ProjectMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserFirstName",
                table: "ProjectMembers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserLastName",
                table: "ProjectMembers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "UserFirstName",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "UserLastName",
                table: "ProjectMembers");
        }
    }
}
