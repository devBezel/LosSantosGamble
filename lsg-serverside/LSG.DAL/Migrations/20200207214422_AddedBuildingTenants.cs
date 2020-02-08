using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedBuildingTenants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingTenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildingId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false),
                    TenantAdded = table.Column<DateTime>(nullable: false),
                    CanEditBuilding = table.Column<bool>(nullable: false),
                    CanWithdrawDeposit = table.Column<bool>(nullable: false),
                    CanManagmentTenants = table.Column<bool>(nullable: false),
                    CanManagmentMagazine = table.Column<bool>(nullable: false),
                    CanRespawnInBuilding = table.Column<bool>(nullable: false),
                    CanLockDoor = table.Column<bool>(nullable: false),
                    CanManagmentGuests = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingTenants_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingTenants_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingTenants_BuildingId",
                table: "BuildingTenants",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingTenants_CharacterId",
                table: "BuildingTenants",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingTenants");
        }
    }
}
