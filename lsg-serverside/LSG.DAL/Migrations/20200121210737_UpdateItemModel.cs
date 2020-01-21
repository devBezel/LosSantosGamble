using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class UpdateItemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CharacterItems_CreatorId",
                table: "CharacterItems",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterItems_Accounts_CreatorId",
                table: "CharacterItems",
                column: "CreatorId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterItems_Accounts_CreatorId",
                table: "CharacterItems");

            migrationBuilder.DropIndex(
                name: "IX_CharacterItems_CreatorId",
                table: "CharacterItems");
        }
    }
}
