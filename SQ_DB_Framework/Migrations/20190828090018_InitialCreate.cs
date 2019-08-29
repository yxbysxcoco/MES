using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQ_DB_Framework.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationParameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Cycle = table.Column<int>(nullable: false),
                    CycleCount = table.Column<int>(nullable: false),
                    Attrition = table.Column<short>(nullable: false),
                    Yield = table.Column<short>(nullable: false),
                    SafetyStock = table.Column<short>(nullable: false),
                    Occupancy = table.Column<short>(nullable: false),
                    InvName = table.Column<short>(nullable: false),
                    MinBatch = table.Column<short>(nullable: false),
                    Delivergoodsed = table.Column<short>(nullable: false),
                    NowStock = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationParameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalculationRange",
                columns: table => new
                {
                    CalculationRangeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationRange", x => x.CalculationRangeId);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "DateTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    SuperiorDateTableId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DT_DT_SDTId",
                        column: x => x.SuperiorDateTableId,
                        principalTable: "DateTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandSource",
                columns: table => new
                {
                    DemandSourceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandSource", x => x.DemandSourceId);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    SuperiorDepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_D_D_SDId",
                        column: x => x.SuperiorDepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailType",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Describe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    ParentMaterialTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MT_MT_PMTId",
                        column: x => x.ParentMaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeterageUnit",
                columns: table => new
                {
                    MeterageUnitId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterageUnit", x => x.MeterageUnitId);
                });

            migrationBuilder.CreateTable(
                name: "MoneyUnit",
                columns: table => new
                {
                    MoneyUnitId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyUnit", x => x.MoneyUnitId);
                });

            migrationBuilder.CreateTable(
                name: "PlanAccessories",
                columns: table => new
                {
                    PlanAccessoriesId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAccessories", x => x.PlanAccessoriesId);
                });

            migrationBuilder.CreateTable(
                name: "PlanType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunParameter",
                columns: table => new
                {
                    RunParameterId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    AutomaticRun = table.Column<short>(nullable: false),
                    RunTime = table.Column<string>(nullable: true),
                    CreatePlan = table.Column<short>(nullable: false),
                    PlanPeriods = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunParameter", x => x.RunParameterId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Storehouse",
                columns: table => new
                {
                    StorehouseId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storehouse", x => x.StorehouseId);
                });

            migrationBuilder.CreateTable(
                name: "Technological",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true),
                    TechnologicalRoute = table.Column<string>(nullable: true),
                    ProductionObject = table.Column<string>(nullable: true),
                    OrganizationTime = table.Column<DateTime>(nullable: false),
                    TaskTimeOrganization = table.Column<string>(nullable: true),
                    TaskTimeOrganizationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technological", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ToolEquipmentType",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolEquipmentType", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    UserName = table.Column<string>(nullable: true),
                    PassWord = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    WarehouseId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    WarehouseName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    MaxInventory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WarehouseId);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseType",
                columns: table => new
                {
                    WarehouseTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    WarehouseTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseType", x => x.WarehouseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DemandParameter",
                columns: table => new
                {
                    DemandParameterId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DemandSourceId = table.Column<int>(nullable: false),
                    CalculationRangeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandParameter", x => x.DemandParameterId);
                    table.ForeignKey(
                        name: "FK_DP_C_CRId",
                        column: x => x.CalculationRangeId,
                        principalTable: "CalculationRange",
                        principalColumn: "CalculationRangeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DP_DS_DSId",
                        column: x => x.DemandSourceId,
                        principalTable: "DemandSource",
                        principalColumn: "DemandSourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    HeadImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_E_D_DId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    Specifications = table.Column<string>(nullable: true),
                    MaterialTypeId = table.Column<int>(nullable: false),
                    MeterageUnitId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.MaterialId);
                    table.ForeignKey(
                        name: "FK_M_MT_MTId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_M_MU_MUId",
                        column: x => x.MeterageUnitId,
                        principalTable: "MeterageUnit",
                        principalColumn: "MeterageUnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanMaintain",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PlanTypeId = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    PlanOrganization = table.Column<string>(nullable: true),
                    OrganizationDepartment = table.Column<string>(nullable: true),
                    InstructionNumber = table.Column<string>(nullable: true),
                    OrganizationDate = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    PlanAccessoriesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanMaintain", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PM_D_DId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PM_PA_PAId",
                        column: x => x.PlanAccessoriesId,
                        principalTable: "PlanAccessories",
                        principalColumn: "PlanAccessoriesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PM_PT_PTId",
                        column: x => x.PlanTypeId,
                        principalTable: "PlanType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PM_S_SId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseDepartmentMap",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    DateLimit = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseDepartmentMap", x => new { x.DepartmentId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WDMap_D_DId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WDM_W_WId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionDemandScheme",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DemandParameterId = table.Column<int>(nullable: false),
                    CalculationParameterId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    RunParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionDemandScheme", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PDS_CP_CPId",
                        column: x => x.CalculationParameterId,
                        principalTable: "CalculationParameter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDS_DP_DPId",
                        column: x => x.DemandParameterId,
                        principalTable: "DemandParameter",
                        principalColumn: "DemandParameterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDS_E_EId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDS_RP_RPId",
                        column: x => x.RunParameterId,
                        principalTable: "RunParameter",
                        principalColumn: "RunParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    OrderCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    DeliverTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ReceivingAddress = table.Column<string>(nullable: true),
                    SalesPersonId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.OrderCode);
                    table.ForeignKey(
                        name: "FK_SO_C_CId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SO_D_DId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SO_E_SPId",
                        column: x => x.SalesPersonId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseEmployeeMap",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    DateLimit = table.Column<DateTime>(nullable: false),
                    InventoryPeriod = table.Column<string>(nullable: true),
                    Schedule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseEmployeeMap", x => new { x.EmployeeId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WEMap_E_EId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WEMap_W_WId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DemandParameterMeterailMap",
                columns: table => new
                {
                    DemandParameterId = table.Column<int>(nullable: false),
                    materialId = table.Column<int>(nullable: false),
                    SourceType = table.Column<string>(nullable: true),
                    SourceBill = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandParameterMeterailMap", x => new { x.DemandParameterId, x.materialId });
                    table.ForeignKey(
                        name: "FK_DPMMap_DP_DPId",
                        column: x => x.DemandParameterId,
                        principalTable: "DemandParameter",
                        principalColumn: "DemandParameterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DPMMap_M_MId",
                        column: x => x.materialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolEquipment",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 45, nullable: false),
                    Edition = table.Column<double>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Standard = table.Column<string>(nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Mark = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    MeterageUnitId = table.Column<int>(nullable: false),
                    MoneyUnitId = table.Column<int>(nullable: false),
                    Univalence = table.Column<double>(nullable: false),
                    LowestStock = table.Column<double>(nullable: false),
                    SaveStock = table.Column<double>(nullable: false),
                    HighestStock = table.Column<double>(nullable: false),
                    StorehouseId = table.Column<int>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    ExitNumber = table.Column<string>(nullable: true),
                    InspectionCompany = table.Column<string>(nullable: true),
                    MaxUseTime = table.Column<double>(nullable: false),
                    RepairCycle = table.Column<string>(nullable: true),
                    RepairNumber = table.Column<double>(nullable: false),
                    Supplier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolEquipment", x => x.Code);
                    table.ForeignKey(
                        name: "FK_TE_M_MId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TEt_MU_MUId",
                        column: x => x.MeterageUnitId,
                        principalTable: "MeterageUnit",
                        principalColumn: "MeterageUnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolEquipment_MoneyUnit_MUId",
                        column: x => x.MoneyUnitId,
                        principalTable: "MoneyUnit",
                        principalColumn: "MoneyUnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TE_S_SId",
                        column: x => x.StorehouseId,
                        principalTable: "Storehouse",
                        principalColumn: "StorehouseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TE_TET_TId",
                        column: x => x.TypeId,
                        principalTable: "ToolEquipmentType",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseMaterialMap",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseMaterialMap", x => new { x.MaterialId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WMMap_M_MId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WMMap_W_WId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanDetailMeterailMap",
                columns: table => new
                {
                    materialId = table.Column<int>(nullable: false),
                    PlanCode = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    MeterageUnitIdId = table.Column<int>(nullable: false),
                    MeterageUnitId = table.Column<int>(nullable: true),
                    DetailTypeId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDetailMeterailMap", x => new { x.PlanCode, x.materialId });
                    table.ForeignKey(
                        name: "FK_PDMMap_DT_DTId",
                        column: x => x.DetailTypeId,
                        principalTable: "DetailType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDMMap_MU_MUId",
                        column: x => x.MeterageUnitId,
                        principalTable: "MeterageUnit",
                        principalColumn: "MeterageUnitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PDMMap_PM_PC",
                        column: x => x.PlanCode,
                        principalTable: "PlanMaintain",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDMMap_St_SId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PDMMap_M_Id",
                        column: x => x.materialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrder",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProductionObject = table.Column<string>(nullable: true),
                    MainWorkshop = table.Column<string>(nullable: true),
                    WorkOrderOrganization = table.Column<string>(nullable: true),
                    TechnologicalCode = table.Column<string>(nullable: true),
                    PlanMaintainCode = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    Emergency = table.Column<int>(nullable: false),
                    WorkOrderOrganizationTime = table.Column<DateTime>(nullable: false),
                    PrintStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrder", x => x.Code);
                    table.ForeignKey(
                        name: "FK_WO_PM_PMC",
                        column: x => x.PlanMaintainCode,
                        principalTable: "PlanMaintain",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WO_St_SId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WO_T_TC",
                        column: x => x.TechnologicalCode,
                        principalTable: "Technological",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductionDemandAnalysis",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    SchemeCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProductionDemandSchemeCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionDemandAnalysis", x => x.Code);
                    table.ForeignKey(
                        name: "FK_PDA_PDS_PDSC",
                        column: x => x.ProductionDemandSchemeCode,
                        principalTable: "ProductionDemandScheme",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DemandParameterSalesOrderMap",
                columns: table => new
                {
                    DemandParameterId = table.Column<int>(nullable: false),
                    OrderCode = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandParameterSalesOrderMap", x => new { x.DemandParameterId, x.OrderCode });
                    table.ForeignKey(
                        name: "FK_DPSOMap_DPr_DPId",
                        column: x => x.DemandParameterId,
                        principalTable: "DemandParameter",
                        principalColumn: "DemandParameterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DPSOMap_SO_OC",
                        column: x => x.OrderCode,
                        principalTable: "SalesOrder",
                        principalColumn: "OrderCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMaterialMap",
                columns: table => new
                {
                    MaterialId = table.Column<int>(nullable: false),
                    OrderCode = table.Column<string>(nullable: false),
                    DeliveryTime = table.Column<DateTime>(nullable: false),
                    UnitPrice = table.Column<double>(nullable: false),
                    Count = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    Remarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMaterialMap", x => new { x.MaterialId, x.OrderCode });
                    table.ForeignKey(
                        name: "FK_OMMap_M_MId",
                        column: x => x.MaterialId,
                        principalTable: "Material",
                        principalColumn: "MaterialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OMMap_SO_OC",
                        column: x => x.OrderCode,
                        principalTable: "SalesOrder",
                        principalColumn: "OrderCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnMoney",
                columns: table => new
                {
                    ReturnMoneyId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ReturnTime = table.Column<DateTime>(nullable: false),
                    Money = table.Column<double>(nullable: false),
                    Mode = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    OrderCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnMoney", x => x.ReturnMoneyId);
                    table.ForeignKey(
                        name: "FK_RM_SO_OC",
                        column: x => x.OrderCode,
                        principalTable: "SalesOrder",
                        principalColumn: "OrderCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DT_SDTId",
                table: "DateTable",
                column: "SuperiorDateTableId");

            migrationBuilder.CreateIndex(
                name: "IX_DP_CRId",
                table: "DemandParameter",
                column: "CalculationRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_DP_DSId",
                table: "DemandParameter",
                column: "DemandSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_DPMMap_Id",
                table: "DemandParameterMeterailMap",
                column: "materialId");

            migrationBuilder.CreateIndex(
                name: "IX_DPSOM_OC",
                table: "DemandParameterSalesOrderMap",
                column: "OrderCode");

            migrationBuilder.CreateIndex(
                name: "IX_D_SDeId",
                table: "Department",
                column: "SuperiorDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_E_DId",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Material_MaterialTypeId",
                table: "Material",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_M_MUId",
                table: "Material",
                column: "MeterageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MT_PMId",
                table: "MaterialType",
                column: "ParentMaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialMap_OrderCode",
                table: "OrderMaterialMap",
                column: "OrderCode");

            migrationBuilder.CreateIndex(
                name: "IX_PDMMap_DTId",
                table: "PlanDetailMeterailMap",
                column: "DetailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PDMMap_MId",
                table: "PlanDetailMeterailMap",
                column: "MeterageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PDMMap_SId",
                table: "PlanDetailMeterailMap",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PDMMap_Id",
                table: "PlanDetailMeterailMap",
                column: "materialId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMaintain_DepartmentId",
                table: "PlanMaintain",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMaintain_PlanAccessoriesId",
                table: "PlanMaintain",
                column: "PlanAccessoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMaintain_PlanTypeId",
                table: "PlanMaintain",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanMaintain_StatusId",
                table: "PlanMaintain",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PDA_PDSC",
                table: "ProductionDemandAnalysis",
                column: "ProductionDemandSchemeCode");

            migrationBuilder.CreateIndex(
                name: "IX_PDS_CPId",
                table: "ProductionDemandScheme",
                column: "CalculationParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_PDS_DId",
                table: "ProductionDemandScheme",
                column: "DemandParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_PDS_EId",
                table: "ProductionDemandScheme",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PDS_RPId",
                table: "ProductionDemandScheme",
                column: "RunParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnMoney_OrderCode",
                table: "ReturnMoney",
                column: "OrderCode");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_DepartmentId",
                table: "SalesOrder",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_SalesPersonId",
                table: "SalesOrder",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolEquipment_MaterialId",
                table: "ToolEquipment",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_TE_MoUId",
                table: "ToolEquipment",
                column: "MeterageUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TE_MUId",
                table: "ToolEquipment",
                column: "MoneyUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_TE_SId",
                table: "ToolEquipment",
                column: "StorehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolEquipment_TypeId",
                table: "ToolEquipment",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WDMap_WId",
                table: "WarehouseDepartmentMap",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WaEMap_WId",
                table: "WarehouseEmployeeMap",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WMMap_WId",
                table: "WarehouseMaterialMap",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WO_PMC",
                table: "WorkOrder",
                column: "PlanMaintainCode");

            migrationBuilder.CreateIndex(
                name: "IX_WO_SId",
                table: "WorkOrder",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WO_TC",
                table: "WorkOrder",
                column: "TechnologicalCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateTable");

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
                name: "WarehouseDepartmentMap");

            migrationBuilder.DropTable(
                name: "WarehouseEmployeeMap");

            migrationBuilder.DropTable(
                name: "WarehouseMaterialMap");

            migrationBuilder.DropTable(
                name: "WarehouseType");

            migrationBuilder.DropTable(
                name: "WorkOrder");

            migrationBuilder.DropTable(
                name: "DetailType");

            migrationBuilder.DropTable(
                name: "ProductionDemandScheme");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "MoneyUnit");

            migrationBuilder.DropTable(
                name: "Storehouse");

            migrationBuilder.DropTable(
                name: "ToolEquipmentType");

            migrationBuilder.DropTable(
                name: "Material");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "PlanMaintain");

            migrationBuilder.DropTable(
                name: "Technological");

            migrationBuilder.DropTable(
                name: "CalculationParameter");

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
