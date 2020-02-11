using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyCrm.Core.Migrations
{
    public partial class add_instockto_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                schema: "core",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                schema: "core",
                table: "Customer",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "core",
                table: "Customer",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "core",
                table: "Customer",
                newName: "Firstname");
        }
    }
}
