using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedCharacterFaceDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CheeksBoneWidth",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CheeksWidth",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ChimpBoneLenght",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ChimpBoneLowering",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ChimpBoneWidth",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ChimpHole",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EyeBrownForward",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EyeBrownHigh",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "EyesOpenning",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "JawBoneBackLenght",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "JawBoneWidth",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "LipsThickness",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NeckThikness",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NoseBoneHigh",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NoseBoneTwist",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NosePeakHight",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NosePeakLenght",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NosePeakLowering",
                table: "CharacterLooks",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "NoseWidth",
                table: "CharacterLooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheeksBoneWidth",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "CheeksWidth",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "ChimpBoneLenght",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "ChimpBoneLowering",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "ChimpBoneWidth",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "ChimpHole",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "EyeBrownForward",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "EyeBrownHigh",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "EyesOpenning",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "JawBoneBackLenght",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "JawBoneWidth",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "LipsThickness",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NeckThikness",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NoseBoneHigh",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NoseBoneTwist",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NosePeakHight",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NosePeakLenght",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NosePeakLowering",
                table: "CharacterLooks");

            migrationBuilder.DropColumn(
                name: "NoseWidth",
                table: "CharacterLooks");
        }
    }
}
