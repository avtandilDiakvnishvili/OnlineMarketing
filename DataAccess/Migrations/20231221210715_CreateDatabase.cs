using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Distributor",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "datetime2", nullable: false),
                    recommender = table.Column<int>(type: "int", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    imgpath = table.Column<string>(name: "img_path", type: "nvarchar(max)", nullable: true),
                    recommendedcount = table.Column<int>(name: "recommended_count", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    unitprice = table.Column<decimal>(name: "unit_price", type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    distributorid = table.Column<int>(name: "distributor_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Address_Distributor_distributor_id",
                        column: x => x.distributorid,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalContact",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", nullable: false),
                    contactinfo = table.Column<string>(name: "contact_info", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    distributorid = table.Column<int>(name: "distributor_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalContact", x => x.id);
                    table.ForeignKey(
                        name: "FK_PersonalContact_Distributor_distributor_id",
                        column: x => x.distributorid,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDocument",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    documentseria = table.Column<string>(name: "document_seria", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    documentnumber = table.Column<string>(name: "document_number", type: "nvarchar(10)", maxLength: 10, nullable: true),
                    releasedate = table.Column<DateTime>(name: "release_date", type: "datetime2", nullable: false),
                    duedate = table.Column<DateTime>(name: "due_date", type: "datetime2", nullable: false),
                    personalnumber = table.Column<string>(name: "personal_number", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    agency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    distributorid = table.Column<int>(name: "distributor_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDocument", x => x.id);
                    table.ForeignKey(
                        name: "FK_PersonalDocument_Distributor_distributor_id",
                        column: x => x.distributorid,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    distributorid = table.Column<int>(name: "distributor_id", type: "int", nullable: true),
                    tdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Distributor_distributor_id",
                        column: x => x.distributorid,
                        principalSchema: "dbo",
                        principalTable: "Distributor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SalesProduct",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    saleid = table.Column<int>(name: "sale_id", type: "int", nullable: false),
                    productid = table.Column<int>(name: "product_id", type: "int", nullable: false),
                    productprice = table.Column<decimal>(name: "product_price", type: "decimal(18,2)", nullable: false),
                    productselfcost = table.Column<decimal>(name: "product_self_cost", type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_SalesProduct_Sales_sale_id",
                        column: x => x.saleid,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_distributor_id",
                schema: "dbo",
                table: "Address",
                column: "distributor_id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalContact_distributor_id",
                schema: "dbo",
                table: "PersonalContact",
                column: "distributor_id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDocument_distributor_id",
                schema: "dbo",
                table: "PersonalDocument",
                column: "distributor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_distributor_id",
                table: "Sales",
                column: "distributor_id");

            migrationBuilder.CreateIndex(
                name: "IX_SalesProduct_sale_id",
                table: "SalesProduct",
                column: "sale_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonalContact",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PersonalDocument",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SalesProduct");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Distributor",
                schema: "dbo");
        }
    }
}
