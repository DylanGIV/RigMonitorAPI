using Microsoft.EntityFrameworkCore.Migrations;

namespace RigMonitorAPI.Migrations
{
    public partial class optimize_space : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockMemory",
                table: "DevicesStats");

            migrationBuilder.DropColumn(
                name: "ClockSM",
                table: "DevicesStats");

            migrationBuilder.DropColumn(
                name: "PowerLimit",
                table: "DevicesStats");

            migrationBuilder.DropColumn(
                name: "TemperatureHardLimit",
                table: "DevicesStats");

            migrationBuilder.DropColumn(
                name: "TemperatureSoftLimit",
                table: "DevicesStats");

            migrationBuilder.DropColumn(
                name: "Utilization",
                table: "DevicesStats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClockMemory",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ClockSM",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PowerLimit",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TemperatureHardLimit",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TemperatureSoftLimit",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Utilization",
                table: "DevicesStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
