using Microsoft.EntityFrameworkCore.Migrations;

namespace Analysis.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Doctor",
                table: "Clients",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SampleType",
                table: "AnalysisType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AnalysisType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctor",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "SampleType",
                table: "AnalysisType",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AnalysisType",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
