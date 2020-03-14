using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class GroupModelsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWorkers_GroupRanks_GroupRankModelId",
                table: "GroupWorkers");

            migrationBuilder.DropIndex(
                name: "IX_GroupWorkers_GroupRankModelId",
                table: "GroupWorkers");

            migrationBuilder.DropColumn(
                name: "GroupRankModelId",
                table: "GroupWorkers");

            migrationBuilder.AddColumn<int>(
                name: "GroupRankId",
                table: "GroupWorkers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupRankId",
                table: "GroupWorkers",
                column: "GroupRankId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWorkers_GroupRanks_GroupRankId",
                table: "GroupWorkers",
                column: "GroupRankId",
                principalTable: "GroupRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupWorkers_GroupRanks_GroupRankId",
                table: "GroupWorkers");

            migrationBuilder.DropIndex(
                name: "IX_GroupWorkers_GroupRankId",
                table: "GroupWorkers");

            migrationBuilder.DropColumn(
                name: "GroupRankId",
                table: "GroupWorkers");

            migrationBuilder.AddColumn<int>(
                name: "GroupRankModelId",
                table: "GroupWorkers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupRankModelId",
                table: "GroupWorkers",
                column: "GroupRankModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupWorkers_GroupRanks_GroupRankModelId",
                table: "GroupWorkers",
                column: "GroupRankModelId",
                principalTable: "GroupRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
