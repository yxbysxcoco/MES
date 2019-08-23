using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Oracle.EntityFrameworkCore.Metadata;

namespace SQ_DB_Framework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentMaterialTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                    table.ForeignKey(
                        name: "MT_MT_PMTID",
                        column: x => x.ParentMaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

           
            
            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MeterageUnitId",
                table: "Material",
                column: "MeterageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialType_PMTID",
                table: "MaterialType",
                column: "ParentMaterialTypeId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemandParameterMeterailMap");

            migrationBuilder.DropTable(
                name: "DemandParameterSalesOrderMap");

            migrationBuilder.DropTable(
                name: "OrderMaterialMap");

            migrationBuilder.DropTable(
                name: "PlanDetailMeterailMap");

            migrationBuilder.DropTable(
                name: "ProductionDemandAnalysis");

            migrationBuilder.DropTable(
                name: "ReturnMoney");

            migrationBuilder.DropTable(
                name: "ToolEquipment");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkOrder");

            migrationBuilder.DropTable(
                name: "DetailType");

            migrationBuilder.DropTable(
                name: "ProductionDemandScheme");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "MoneyUnit");

            migrationBuilder.DropTable(
                name: "Storehouse");

            migrationBuilder.DropTable(
                name: "ToolEquipmentType");

            migrationBuilder.DropTable(
                name: "PlanMaintain");

            migrationBuilder.DropTable(
                name: "Technological");

            migrationBuilder.DropTable(
                name: "DemandParameter");

            migrationBuilder.DropTable(
                name: "RunParameter");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "MaterialType");

            migrationBuilder.DropTable(
                name: "MeterageUnit");

            migrationBuilder.DropTable(
                name: "PlanAccessories");

            migrationBuilder.DropTable(
                name: "PlanType");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "CalculationRange");

            migrationBuilder.DropTable(
                name: "DemandSource");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
