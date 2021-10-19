using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RigMonitorAPI.Migrations
{
    public partial class initialnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicesStats");

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                columns: table => new
                {
                    DeviceGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceGroupName = table.Column<string>(type: "text", nullable: true),
                    DeviceGroupDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroup", x => x.DeviceGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Rig",
                columns: table => new
                {
                    RigId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RigName = table.Column<string>(type: "text", nullable: true),
                    RigDescription = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rig", x => x.RigId);
                    table.ForeignKey(
                        name: "FK_Rig_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RigGroup",
                columns: table => new
                {
                    RigGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RigGroupName = table.Column<string>(type: "text", nullable: true),
                    RigGroupDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigGroup", x => x.RigGroupId);
                });

            migrationBuilder.CreateTable(
                name: "WalletAddress",
                columns: table => new
                {
                    Address = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    PoolId = table.Column<string>(type: "text", nullable: true),
                    PoolName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletAddress", x => new { x.Address, x.UserId });
                    table.ForeignKey(
                        name: "FK_WalletAddress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    DeviceDescription = table.Column<string>(type: "text", nullable: true),
                    RigId = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Device_Rig_RigId",
                        column: x => x.RigId,
                        principalTable: "Rig",
                        principalColumn: "RigId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RigRigGroup",
                columns: table => new
                {
                    RigGroupsRigGroupId = table.Column<long>(type: "bigint", nullable: false),
                    RigsRigId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigRigGroup", x => new { x.RigGroupsRigGroupId, x.RigsRigId });
                    table.ForeignKey(
                        name: "FK_RigRigGroup_Rig_RigsRigId",
                        column: x => x.RigsRigId,
                        principalTable: "Rig",
                        principalColumn: "RigId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RigRigGroup_RigGroup_RigGroupsRigGroupId",
                        column: x => x.RigGroupsRigGroupId,
                        principalTable: "RigGroup",
                        principalColumn: "RigGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDeviceGroup",
                columns: table => new
                {
                    DeviceGroupsDeviceGroupId = table.Column<long>(type: "bigint", nullable: false),
                    DevicesDeviceId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDeviceGroup", x => new { x.DeviceGroupsDeviceGroupId, x.DevicesDeviceId });
                    table.ForeignKey(
                        name: "FK_DeviceDeviceGroup_Device_DevicesDeviceId",
                        column: x => x.DevicesDeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDeviceGroup_DeviceGroup_DeviceGroupsDeviceGroupId",
                        column: x => x.DeviceGroupsDeviceGroupId,
                        principalTable: "DeviceGroup",
                        principalColumn: "DeviceGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStats",
                columns: table => new
                {
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeviceId = table.Column<string>(type: "text", nullable: false),
                    Temperature = table.Column<short>(type: "smallint", nullable: false),
                    PowerUsage = table.Column<decimal>(type: "numeric(3,1)", nullable: false),
                    FanSpeed = table.Column<short>(type: "smallint", nullable: false),
                    MemoryClockSpeed = table.Column<short>(type: "smallint", nullable: false),
                    CoreClockSpeed = table.Column<short>(type: "smallint", nullable: false),
                    DeviceUsage = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStats", x => new { x.Timestamp, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_DeviceStats_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_RigId",
                table: "Device",
                column: "RigId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeviceGroup_DevicesDeviceId",
                table: "DeviceDeviceGroup",
                column: "DevicesDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStats_DeviceId",
                table: "DeviceStats",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Rig_UserId",
                table: "Rig",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RigRigGroup_RigsRigId",
                table: "RigRigGroup",
                column: "RigsRigId");

            migrationBuilder.CreateIndex(
                name: "IX_WalletAddress_UserId",
                table: "WalletAddress",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceDeviceGroup");

            migrationBuilder.DropTable(
                name: "DeviceStats");

            migrationBuilder.DropTable(
                name: "RigRigGroup");

            migrationBuilder.DropTable(
                name: "WalletAddress");

            migrationBuilder.DropTable(
                name: "DeviceGroup");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "RigGroup");

            migrationBuilder.DropTable(
                name: "Rig");

            migrationBuilder.CreateTable(
                name: "DevicesStats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<string>(type: "text", nullable: true),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    FanSpeed = table.Column<long>(type: "bigint", nullable: false),
                    PowerUsage = table.Column<long>(type: "bigint", nullable: false),
                    RigName = table.Column<string>(type: "text", nullable: true),
                    Temperature = table.Column<long>(type: "bigint", nullable: false),
                    Timestamp = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicesStats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesStats_UserId",
                table: "DevicesStats",
                column: "UserId");
        }
    }
}
