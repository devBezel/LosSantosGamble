using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    Height = table.Column<float>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    History = table.Column<string>(nullable: true),
                    PicUrl = table.Column<string>(nullable: true),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    Rotation = table.Column<float>(nullable: false),
                    Money = table.Column<float>(nullable: false),
                    DirtyMoney = table.Column<float>(nullable: false),
                    Bank = table.Column<float>(nullable: false),
                    BankStatus = table.Column<bool>(nullable: false),
                    Health = table.Column<float>(nullable: false),
                    Armor = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterDescriptions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterLooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    FatherFaceId = table.Column<byte>(nullable: true),
                    MotherFaceId = table.Column<byte>(nullable: true),
                    SkinColour = table.Column<byte>(nullable: true),
                    ShapeMix = table.Column<float>(nullable: true),
                    EarsColor = table.Column<byte>(nullable: true),
                    BlemishesId = table.Column<byte>(nullable: true),
                    BlemishesOpacity = table.Column<float>(nullable: true),
                    AgeingId = table.Column<byte>(nullable: true),
                    AgeingOpacity = table.Column<float>(nullable: true),
                    BlushId = table.Column<byte>(nullable: true),
                    BlushOpacity = table.Column<float>(nullable: true),
                    BlushColor = table.Column<byte>(nullable: true),
                    BeardId = table.Column<byte>(nullable: true),
                    BeardOpacity = table.Column<float>(nullable: true),
                    BeardColor = table.Column<float>(nullable: true),
                    NoseWidth = table.Column<float>(nullable: true),
                    NosePeakHight = table.Column<float>(nullable: true),
                    NosePeakLenght = table.Column<float>(nullable: true),
                    NoseBoneHigh = table.Column<float>(nullable: true),
                    NosePeakLowering = table.Column<float>(nullable: true),
                    NoseBoneTwist = table.Column<float>(nullable: true),
                    EyeBrownHigh = table.Column<float>(nullable: true),
                    EyeBrownForward = table.Column<float>(nullable: true),
                    CheeksBoneWidth = table.Column<float>(nullable: true),
                    CheeksWidth = table.Column<float>(nullable: true),
                    EyesOpenning = table.Column<float>(nullable: true),
                    LipsThickness = table.Column<float>(nullable: true),
                    JawBoneWidth = table.Column<float>(nullable: true),
                    JawBoneBackLenght = table.Column<float>(nullable: true),
                    ChimpBoneLowering = table.Column<float>(nullable: true),
                    ChimpBoneLenght = table.Column<float>(nullable: true),
                    ChimpBoneWidth = table.Column<float>(nullable: true),
                    ChimpHole = table.Column<float>(nullable: true),
                    NeckThikness = table.Column<float>(nullable: true),
                    EyebrowsId = table.Column<byte>(nullable: true),
                    SecondEyebrowsColor = table.Column<byte>(nullable: true),
                    EyeBrowsOpacity = table.Column<float>(nullable: false),
                    FirstEyebrowsColor = table.Column<byte>(nullable: true),
                    LipstickId = table.Column<byte>(nullable: true),
                    FirstLipstickColor = table.Column<byte>(nullable: true),
                    LipstickOpacity = table.Column<float>(nullable: true),
                    SecondLipstickColor = table.Column<byte>(nullable: true),
                    MakeupId = table.Column<float>(nullable: true),
                    FirstMakeupColor = table.Column<byte>(nullable: true),
                    MakeupOpacity = table.Column<float>(nullable: true),
                    SecondMakeupColor = table.Column<byte>(nullable: true),
                    GlassesId = table.Column<byte>(nullable: true),
                    GlassesTexture = table.Column<byte>(nullable: true),
                    HairId = table.Column<byte>(nullable: true),
                    HairTexture = table.Column<byte>(nullable: true),
                    HairColor = table.Column<byte>(nullable: true),
                    HairColorTwo = table.Column<byte>(nullable: true),
                    HatId = table.Column<byte>(nullable: true),
                    HatTexture = table.Column<byte>(nullable: true),
                    TopId = table.Column<byte>(nullable: true),
                    TopTexture = table.Column<byte>(nullable: true),
                    TorsoId = table.Column<byte>(nullable: true),
                    TorsoTexture = table.Column<byte>(nullable: true),
                    UndershirtId = table.Column<byte>(nullable: true),
                    UndershirtTexture = table.Column<byte>(nullable: true),
                    LegsId = table.Column<byte>(nullable: true),
                    LegsTexture = table.Column<byte>(nullable: true),
                    ShoesId = table.Column<byte>(nullable: true),
                    ShoesTexture = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterLooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterLooks_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: false),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    Rot = table.Column<float>(nullable: false),
                    R = table.Column<int>(nullable: false),
                    G = table.Column<int>(nullable: false),
                    B = table.Column<int>(nullable: false),
                    State = table.Column<bool>(nullable: false),
                    Health = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDescriptions_CharacterId",
                table: "CharacterDescriptions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLooks_CharacterId",
                table: "CharacterLooks",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountId",
                table: "Characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterDescriptions");

            migrationBuilder.DropTable(
                name: "CharacterLooks");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
