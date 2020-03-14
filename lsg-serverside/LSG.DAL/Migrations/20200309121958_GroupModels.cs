using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class GroupModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Grant = table.Column<int>(nullable: false),
                    MaxPayday = table.Column<int>(nullable: false),
                    Money = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    GroupType = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LeaderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Characters_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Characters_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupRanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Rights = table.Column<int>(nullable: false),
                    Salary = table.Column<decimal>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    DefaultForGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRanks_Groups_DefaultForGroupId",
                        column: x => x.DefaultForGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRanks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Salary = table.Column<int>(nullable: false),
                    DutyMinutes = table.Column<int>(nullable: false),
                    Rights = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false),
                    GroupRankModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_GroupRanks_GroupRankModelId",
                        column: x => x.GroupRankModelId,
                        principalTable: "GroupRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupRanks_DefaultForGroupId",
                table: "GroupRanks",
                column: "DefaultForGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRanks_GroupId",
                table: "GroupRanks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_CharacterId",
                table: "GroupWorkers",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupId",
                table: "GroupWorkers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupRankModelId",
                table: "GroupWorkers",
                column: "GroupRankModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupWorkers");

            migrationBuilder.DropTable(
                name: "GroupRanks");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
