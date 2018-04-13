using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CarShop.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarFeatureType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BasePrice = table.Column<decimal>(nullable: false),
                    CarCompanyId = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(maxLength: 100, nullable: true),
                    LongDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    ShortDescription = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarCompanies_CarCompanyId",
                        column: x => x.CarCompanyId,
                        principalTable: "CarCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarFeatureModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarFeatureId = table.Column<int>(nullable: true),
                    CarModelId = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFeatureModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarFeatureModels_CarFeatures_CarFeatureId",
                        column: x => x.CarFeatureId,
                        principalTable: "CarFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarFeatureModels_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarModelId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    FullName = table.Column<string>(maxLength: 200, nullable: true),
                    SecureCode = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarOrders_CarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "CarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarOrderFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureModelId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarOrderFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarOrderFeature_CarFeatureModels_FeatureModelId",
                        column: x => x.FeatureModelId,
                        principalTable: "CarFeatureModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarOrderFeature_CarOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "CarOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarFeatureModels_CarFeatureId",
                table: "CarFeatureModels",
                column: "CarFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_CarFeatureModels_CarModelId",
                table: "CarFeatureModels",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarCompanyId",
                table: "CarModels",
                column: "CarCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOrderFeature_FeatureModelId",
                table: "CarOrderFeature",
                column: "FeatureModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOrderFeature_OrderId",
                table: "CarOrderFeature",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CarOrders_CarModelId",
                table: "CarOrders",
                column: "CarModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarOrderFeature");

            migrationBuilder.DropTable(
                name: "CarFeatureModels");

            migrationBuilder.DropTable(
                name: "CarOrders");

            migrationBuilder.DropTable(
                name: "CarFeatures");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropTable(
                name: "CarCompanies");
        }
    }
}
