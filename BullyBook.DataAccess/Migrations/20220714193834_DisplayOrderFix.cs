using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BullyBookWeb.Migrations
{
    public partial class DisplayOrderFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DysplayOrder",
                table: "Categories",
                newName: "DisplayOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "Categories",
                newName: "DysplayOrder");
        }
    }
}
