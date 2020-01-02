using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedInitialMigrationFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPremiums_Characters_CharacterId",
                table: "AccountPremiums");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountPremiums_AccountPremiumId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountPremiumId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_AccountPremiums_CharacterId",
                table: "AccountPremiums");

            migrationBuilder.DropColumn(
                name: "AccountPremiumId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CharacterId",
                table: "AccountPremiums",
                newName: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPremiums_AccountId",
                table: "AccountPremiums",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPremiums_Accounts_AccountId",
                table: "AccountPremiums",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountPremiums_Accounts_AccountId",
                table: "AccountPremiums");

            migrationBuilder.DropIndex(
                name: "IX_AccountPremiums_AccountId",
                table: "AccountPremiums");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountPremiums",
                newName: "CharacterId");

            migrationBuilder.AddColumn<int>(
                name: "AccountPremiumId",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountPremiumId",
                table: "Accounts",
                column: "AccountPremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPremiums_CharacterId",
                table: "AccountPremiums",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPremiums_Characters_CharacterId",
                table: "AccountPremiums",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountPremiums_AccountPremiumId",
                table: "Accounts",
                column: "AccountPremiumId",
                principalTable: "AccountPremiums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
