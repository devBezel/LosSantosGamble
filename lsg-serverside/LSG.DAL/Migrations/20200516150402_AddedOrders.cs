using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Warehouses_WarehouseId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_WarehouseId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "WarehouseItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    FirstParameter = table.Column<double>(nullable: true),
                    SecondParameter = table.Column<double>(nullable: true),
                    ThirdParameter = table.Column<double>(nullable: true),
                    FourthParameter = table.Column<double>(nullable: true),
                    ItemEntityType = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseItems_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    FirstParameter = table.Column<double>(nullable: true),
                    SecondParameter = table.Column<double>(nullable: true),
                    ThirdParameter = table.Column<double>(nullable: true),
                    FourthParameter = table.Column<double>(nullable: true),
                    ItemEntityType = table.Column<int>(nullable: false),
                    WarehouseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseOrders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItems_WarehouseId",
                table: "WarehouseItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOrders_WarehouseId",
                table: "WarehouseOrders",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseItems");

            migrationBuilder.DropTable(
                name: "WarehouseOrders");

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Items",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_WarehouseId",
                table: "Items",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Warehouses_WarehouseId",
                table: "Items",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
