using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Analysis.Migrations
{
    public partial class fifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecievedDate",
                table: "ClientAnalysis");

            migrationBuilder.AddColumn<int>(
                name: "ClientCode",
                table: "Clients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecievedDate",
                table: "Clients",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientCode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RecievedDate",
                table: "Clients");

            migrationBuilder.AddColumn<DateTime>(
                name: "RecievedDate",
                table: "ClientAnalysis",
                type: "Date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
