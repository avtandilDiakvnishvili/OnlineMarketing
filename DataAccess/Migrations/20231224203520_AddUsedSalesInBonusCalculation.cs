using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUsedSalesInBonusCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistributorBonus",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    distributorid = table.Column<int>(name: "distributor_id", type: "int", nullable: false),
                    bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    startdate = table.Column<DateTime>(name: "start_date", type: "datetime2", nullable: false),
                    enddate = table.Column<DateTime>(name: "end_date", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorBonus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsedSale",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bonusid = table.Column<int>(name: "bonus_id", type: "int", nullable: false),
                    saleid = table.Column<int>(name: "sale_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedSale", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsedSale_DistributorBonus_bonus_id",
                        column: x => x.bonusid,
                        principalTable: "DistributorBonus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsedSale_bonus_id",
                table: "UsedSale",
                column: "bonus_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsedSale");

            migrationBuilder.DropTable(
                name: "DistributorBonus");
        }
    }
}
