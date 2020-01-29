using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class BuildingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingModelId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BuildingType = table.Column<int>(nullable: false),
                    EntryFee = table.Column<float>(nullable: false),
                    ExternalPickupPositionX = table.Column<float>(nullable: false),
                    ExternalPickupPositionY = table.Column<float>(nullable: false),
                    ExternalPickupPositionZ = table.Column<float>(nullable: false),
                    InternalPickupPositionX = table.Column<float>(nullable: false),
                    InternalPickupPositionY = table.Column<float>(nullable: false),
                    InternalPickupPositionZ = table.Column<float>(nullable: false),
                    MaxObjectsCount = table.Column<int>(nullable: false),
                    CurrentObjectsCount = table.Column<int>(nullable: false),
                    HasCCTV = table.Column<bool>(nullable: false),
                    HasSafe = table.Column<bool>(nullable: false),
                    SpawnPossible = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: true),
                    CharacterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_BuildingModelId",
                table: "Items",
                column: "BuildingModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AccountId",
                table: "Buildings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CharacterId",
                table: "Buildings",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Buildings_BuildingModelId",
                table: "Items",
                column: "BuildingModelId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Buildings_BuildingModelId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Items_BuildingModelId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "BuildingModelId",
                table: "Items");
        }
    }
}
