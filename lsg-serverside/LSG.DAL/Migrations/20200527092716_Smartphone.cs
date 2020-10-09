using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class Smartphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartphoneContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PhoneItemId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    IsFavorite = table.Column<bool>(nullable: false),
                    IsAlarmNumber = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartphoneContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartphoneContacts_Items_PhoneItemId",
                        column: x => x.PhoneItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartphoneMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderNumber = table.Column<int>(nullable: false),
                    CellphoneId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartphoneMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartphoneMessages_Items_CellphoneId",
                        column: x => x.CellphoneId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartphoneRecentCalls",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PhoneItemId = table.Column<int>(nullable: false),
                    CallNumber = table.Column<int>(nullable: false),
                    CalledDate = table.Column<DateTime>(nullable: false),
                    CallTime = table.Column<int>(nullable: false),
                    IsAnwser = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartphoneRecentCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartphoneRecentCalls_Items_PhoneItemId",
                        column: x => x.PhoneItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SmartphoneContacts_PhoneItemId",
                table: "SmartphoneContacts",
                column: "PhoneItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartphoneMessages_CellphoneId",
                table: "SmartphoneMessages",
                column: "CellphoneId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartphoneRecentCalls_PhoneItemId",
                table: "SmartphoneRecentCalls",
                column: "PhoneItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmartphoneContacts");

            migrationBuilder.DropTable(
                name: "SmartphoneMessages");

            migrationBuilder.DropTable(
                name: "SmartphoneRecentCalls");
        }
    }
}
