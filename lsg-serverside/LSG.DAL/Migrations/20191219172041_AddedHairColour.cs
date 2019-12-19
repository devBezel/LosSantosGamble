using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedHairColour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "SkinColour",
                table: "CharacterLooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkinColour",
                table: "CharacterLooks");
        }
    }
}
