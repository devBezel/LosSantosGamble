using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class VehicleUpgrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleUpgradeId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_VehicleUpgradeId",
                table: "Items",
                column: "VehicleUpgradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Vehicles_VehicleUpgradeId",
                table: "Items",
                column: "VehicleUpgradeId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Vehicles_VehicleUpgradeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_VehicleUpgradeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "VehicleUpgradeId",
                table: "Items");
        }
    }
}
