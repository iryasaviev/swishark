using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Upd1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserLastName",
                table: "ProjectMembers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "UserFirstName",
                table: "ProjectMembers",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ProjectMembers",
                newName: "UserLastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "ProjectMembers",
                newName: "UserFirstName");
        }
    }
}
