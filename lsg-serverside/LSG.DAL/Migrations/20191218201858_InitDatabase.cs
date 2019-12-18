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
                    Armor = table.Column<float>(nullable: false),
                    CharacterDetailId = table.Column<int>(nullable: false)
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
                name: "CharacterFaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterId = table.Column<int>(nullable: false),
                    ShapeFirstID = table.Column<int>(nullable: false),
                    ShapeSecondID = table.Column<int>(nullable: false),
                    ShapeThirdID = table.Column<int>(nullable: false),
                    SkinFirstID = table.Column<int>(nullable: false),
                    SkinSecondID = table.Column<int>(nullable: false),
                    SkinThirdID = table.Column<int>(nullable: false),
                    ShapeMix = table.Column<float>(nullable: false),
                    SkinMix = table.Column<float>(nullable: false),
                    ThirdMix = table.Column<float>(nullable: false),
                    IsParent = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "CharacterDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CharacterFaceId = table.Column<int>(nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDescriptions_CharacterId",
                table: "CharacterDescriptions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDetails_CharacterFaceId",
                table: "CharacterDetails",
                column: "CharacterFaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterFaces_CharacterId",
                table: "CharacterFaces",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountId",
                table: "Characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterDetailId",
                table: "Characters",
                column: "CharacterDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_CharacterDetails_CharacterDetailId",
                table: "Characters",
                column: "CharacterDetailId",
                principalTable: "CharacterDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterFaces_Characters_CharacterId",
                table: "CharacterFaces");

            migrationBuilder.DropTable(
                name: "CharacterDescriptions");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CharacterDetails");

            migrationBuilder.DropTable(
                name: "CharacterFaces");
        }
    }
}
