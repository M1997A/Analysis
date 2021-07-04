using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Analysis.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalysisType",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    SampleType = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Precautions = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Diseases = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalysisFeatures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    NormalRange = table.Column<string>(nullable: true),
                    MeasruingUnit = table.Column<string>(nullable: true),
                    AnalysisTypeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalysisFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalysisFeatures_AnalysisType_AnalysisTypeId",
                        column: x => x.AnalysisTypeId,
                        principalTable: "AnalysisType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAnalysis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<long>(nullable: false),
                    AnalysisTypeId = table.Column<long>(nullable: false),
                    RecievedDate = table.Column<DateTime>(type: "Date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAnalysis_AnalysisType_AnalysisTypeId",
                        column: x => x.AnalysisTypeId,
                        principalTable: "AnalysisType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAnalysis_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientAnalysisId = table.Column<long>(nullable: false),
                    AnalysisFeaturesId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_AnalysisFeatures_AnalysisFeaturesId",
                        column: x => x.AnalysisFeaturesId,
                        principalTable: "AnalysisFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_ClientAnalysis_ClientAnalysisId",
                        column: x => x.ClientAnalysisId,
                        principalTable: "ClientAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalysisFeatures_AnalysisTypeId",
                table: "AnalysisFeatures",
                column: "AnalysisTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAnalysis_AnalysisTypeId",
                table: "ClientAnalysis",
                column: "AnalysisTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAnalysis_ClientId",
                table: "ClientAnalysis",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AnalysisFeaturesId",
                table: "Results",
                column: "AnalysisFeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_ClientAnalysisId",
                table: "Results",
                column: "ClientAnalysisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "AnalysisFeatures");

            migrationBuilder.DropTable(
                name: "ClientAnalysis");

            migrationBuilder.DropTable(
                name: "AnalysisType");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
