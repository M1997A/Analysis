using Microsoft.EntityFrameworkCore.Migrations;

namespace Analysis.Migrations
{
    public partial class forth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "ClientAnalysis",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Finished",
                table: "ClientAnalysis");
        }
    }
}
