using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class BuildingModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Accounts_AccountId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_AccountId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Buildings");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CreatorId",
                table: "Buildings",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Accounts_CreatorId",
                table: "Buildings",
                column: "CreatorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Accounts_CreatorId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_CreatorId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Buildings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AccountId",
                table: "Buildings",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Accounts_AccountId",
                table: "Buildings",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
