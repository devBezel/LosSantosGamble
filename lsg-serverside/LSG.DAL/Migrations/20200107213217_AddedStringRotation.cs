using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class AddedStringRotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rot",
                table: "Vehicles",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "UndershirtTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UndershirtId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TorsoTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TorsoId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SkinColour",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShoesTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShoesId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ShapeMix",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecondMakeupColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecondLipstickColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SecondEyebrowsColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NoseWidth",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakLowering",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakLenght",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakHight",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NoseBoneTwist",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NoseBoneHigh",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "NeckThikness",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MotherFaceId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "MakeupOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "MakeupId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "LipstickOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LipstickId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "LipsThickness",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LegsTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LegsId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "JawBoneWidth",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "JawBoneBackLenght",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HatTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HatId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HairTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HairId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HairColorTwo",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HairColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GlassesTexture",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GlassesId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstMakeupColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstLipstickColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FirstEyebrowsColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FatherFaceId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "EyesOpenning",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EyebrowsId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "EyeBrownHigh",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "EyeBrownForward",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EarsColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ChimpHole",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneWidth",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneLowering",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneLenght",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CheeksWidth",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "CheeksBoneWidth",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BlushOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlushId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlushColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BlemishesOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlemishesId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BeardOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BeardId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BeardColor",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AgeingOpacity",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeingId",
                table: "CharacterLooks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rot",
                table: "Vehicles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UndershirtTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UndershirtId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TorsoTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TorsoId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TopTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TopId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SkinColour",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ShoesTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ShoesId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "ShapeMix",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "SecondMakeupColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SecondLipstickColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SecondEyebrowsColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "NoseWidth",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakLowering",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakLenght",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NosePeakHight",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NoseBoneTwist",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NoseBoneHigh",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "NeckThikness",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "MotherFaceId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "MakeupOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "MakeupId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "LipstickOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "LipstickId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "LipsThickness",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "LegsTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LegsId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "JawBoneWidth",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "JawBoneBackLenght",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "HatTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HatId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HairTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HairId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HairColorTwo",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HairColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GlassesTexture",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GlassesId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FirstMakeupColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FirstLipstickColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FirstEyebrowsColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FatherFaceId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "EyesOpenning",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "EyebrowsId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "EyeBrownHigh",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "EyeBrownForward",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "EarsColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "ChimpHole",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneWidth",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneLowering",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ChimpBoneLenght",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "CheeksWidth",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "CheeksBoneWidth",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "BlushOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "BlushId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BlushColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "BlemishesOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "BlemishesId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "BeardOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "BeardId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "BeardColor",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "AgeingOpacity",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "AgeingId",
                table: "CharacterLooks",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
