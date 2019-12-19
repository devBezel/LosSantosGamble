using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class ChangingCharacterLookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterDetails");

            migrationBuilder.DropTable(
                name: "CharacterFaces");

            migrationBuilder.CreateTable(
                name: "CharacterLooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    FatherFaceId = table.Column<byte>(nullable: true),
                    MotherFaceId = table.Column<byte>(nullable: true),
                    ShapeMix = table.Column<float>(nullable: true),
                    EarsId = table.Column<byte>(nullable: true),
                    EarsTexture = table.Column<byte>(nullable: true),
                    EyebrowsId = table.Column<byte>(nullable: true),
                    SecondEyebrowsColor = table.Column<byte>(nullable: true),
                    EyeBrowsOpacity = table.Column<float>(nullable: false),
                    FirstEyebrowsColor = table.Column<byte>(nullable: true),
                    FirstLipstickColor = table.Column<byte>(nullable: true),
                    LipstickOpacity = table.Column<float>(nullable: true),
                    SecondLipstickColor = table.Column<byte>(nullable: true),
                    FirstMakeupColor = table.Column<byte>(nullable: true),
                    MakeupOpacity = table.Column<float>(nullable: true),
                    SecondMakeupColor = table.Column<byte>(nullable: true),
                    GlassesId = table.Column<byte>(nullable: true),
                    GlassesTexture = table.Column<byte>(nullable: true),
                    HairId = table.Column<byte>(nullable: true),
                    HairTexture = table.Column<byte>(nullable: true),
                    HairColor = table.Column<byte>(nullable: true),
                    HatId = table.Column<byte>(nullable: true),
                    HatTexture = table.Column<byte>(nullable: true),
                    TopId = table.Column<byte>(nullable: true),
                    TopTexture = table.Column<byte>(nullable: true),
                    TorsoId = table.Column<byte>(nullable: true),
                    UndershirtId = table.Column<byte>(nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLooks_CharacterId",
                table: "CharacterLooks",
                column: "CharacterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterLooks");

            migrationBuilder.CreateTable(
                name: "CharacterFaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    IsParent = table.Column<bool>(nullable: false),
                    ShapeFirstID = table.Column<int>(nullable: false),
                    ShapeMix = table.Column<float>(nullable: false),
                    ShapeSecondID = table.Column<int>(nullable: false),
                    ShapeThirdID = table.Column<int>(nullable: false),
                    SkinFirstID = table.Column<int>(nullable: false),
                    SkinMix = table.Column<float>(nullable: false),
                    SkinSecondID = table.Column<int>(nullable: false),
                    SkinThirdID = table.Column<int>(nullable: false),
                    ThirdMix = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterFaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterFaces_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterFaceId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterDetails_CharacterFaces_CharacterFaceId",
                        column: x => x.CharacterFaceId,
                        principalTable: "CharacterFaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterDetails_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDetails_CharacterFaceId",
                table: "CharacterDetails",
                column: "CharacterFaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDetails_CharacterId",
                table: "CharacterDetails",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFaces_CharacterId",
                table: "CharacterFaces",
                column: "CharacterId");
        }
    }
}
