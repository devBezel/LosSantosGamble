using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class ChangeCharacterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_CharacterDetails_CharacterDetailId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_CharacterDetailId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "CharacterDetailId",
                table: "Characters");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "CharacterDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDetails_CharacterId",
                table: "CharacterDetails",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterDetails_Characters_CharacterId",
                table: "CharacterDetails",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterDetails_Characters_CharacterId",
                table: "CharacterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CharacterDetails_CharacterId",
                table: "CharacterDetails");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "CharacterDetails");

            migrationBuilder.AddColumn<int>(
                name: "CharacterDetailId",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterDetailId",
                table: "Characters",
                column: "CharacterDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterDetails_CharacterDetailId",
                table: "Characters",
                column: "CharacterDetailId",
                principalTable: "CharacterDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
