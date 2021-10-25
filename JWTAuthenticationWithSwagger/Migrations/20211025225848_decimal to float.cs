using Microsoft.EntityFrameworkCore.Migrations;

namespace RigMonitorAPI.Migrations
{
    public partial class decimaltofloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PowerUsage",
                table: "DeviceStats",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PowerUsage",
                table: "DeviceStats",
                type: "numeric(3,1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
