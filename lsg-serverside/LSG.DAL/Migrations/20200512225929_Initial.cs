using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LSG.DAL.Migrations
{
    public partial class Initial : Migration
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
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShopEntityType = table.Column<int>(nullable: false),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountPremiums",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(nullable: false),
                    BoughtTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPremiums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPremiums_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atms_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusStops_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Dimension = table.Column<int>(nullable: false),
                    Rotation = table.Column<float>(nullable: false),
                    Money = table.Column<float>(nullable: false),
                    DirtyMoney = table.Column<float>(nullable: false),
                    Bank = table.Column<float>(nullable: false),
                    BankStatus = table.Column<bool>(nullable: false),
                    Health = table.Column<float>(nullable: false),
                    Armor = table.Column<float>(nullable: false),
                    Thirsty = table.Column<float>(nullable: false),
                    Hunger = table.Column<float>(nullable: false),
                    Online = table.Column<bool>(nullable: false),
                    TimeSpent = table.Column<int>(nullable: false),
                    GamblePoints = table.Column<float>(nullable: false),
                    RecentlyPlayed = table.Column<DateTime>(nullable: false)
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
                name: "ShopAssortments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    FirstParameter = table.Column<double>(nullable: true),
                    SecondParameter = table.Column<double>(nullable: true),
                    ThirdParameter = table.Column<double>(nullable: true),
                    FourthParameter = table.Column<double>(nullable: true),
                    ItemEntityType = table.Column<int>(nullable: false),
                    ShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopAssortments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopAssortments_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusStopStations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusStopId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    Time = table.Column<float>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStopStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusStopStations_BusStops_BusStopId",
                        column: x => x.BusStopId,
                        principalTable: "BusStops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusStopStations_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    BuildingType = table.Column<int>(nullable: false),
                    EntryFee = table.Column<int>(nullable: false),
                    ExternalPickupPositionX = table.Column<float>(nullable: false),
                    ExternalPickupPositionY = table.Column<float>(nullable: false),
                    ExternalPickupPositionZ = table.Column<float>(nullable: false),
                    InternalPickupPositionX = table.Column<float>(nullable: false),
                    InternalPickupPositionY = table.Column<float>(nullable: false),
                    InternalPickupPositionZ = table.Column<float>(nullable: false),
                    MaxObjectsCount = table.Column<int>(nullable: false),
                    CurrentObjectsCount = table.Column<int>(nullable: false),
                    HasCCTV = table.Column<bool>(nullable: false),
                    HasSafe = table.Column<bool>(nullable: false),
                    SpawnPossible = table.Column<bool>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    OnSale = table.Column<bool>(nullable: false),
                    SaleCost = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buildings_Accounts_CreatorId",
                        column: x => x.CreatorId,
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
                    FatherFaceId = table.Column<int>(nullable: false),
                    MotherFaceId = table.Column<int>(nullable: false),
                    SkinColour = table.Column<int>(nullable: false),
                    ShapeMix = table.Column<float>(nullable: false),
                    EarsColor = table.Column<int>(nullable: false),
                    BlemishesId = table.Column<int>(nullable: false),
                    BlemishesOpacity = table.Column<float>(nullable: false),
                    AgeingId = table.Column<int>(nullable: false),
                    AgeingOpacity = table.Column<float>(nullable: false),
                    BlushId = table.Column<int>(nullable: false),
                    BlushOpacity = table.Column<float>(nullable: false),
                    BlushColor = table.Column<int>(nullable: false),
                    BeardId = table.Column<int>(nullable: false),
                    BeardOpacity = table.Column<float>(nullable: false),
                    BeardColor = table.Column<float>(nullable: false),
                    NoseWidth = table.Column<float>(nullable: false),
                    NosePeakHight = table.Column<float>(nullable: false),
                    NosePeakLenght = table.Column<float>(nullable: false),
                    NoseBoneHigh = table.Column<float>(nullable: false),
                    NosePeakLowering = table.Column<float>(nullable: false),
                    NoseBoneTwist = table.Column<float>(nullable: false),
                    EyeBrownForward = table.Column<float>(nullable: false),
                    EyeBrownHigh = table.Column<float>(nullable: false),
                    CheeksBoneWidth = table.Column<float>(nullable: false),
                    CheeksWidth = table.Column<float>(nullable: false),
                    EyesOpenning = table.Column<float>(nullable: false),
                    LipsThickness = table.Column<float>(nullable: false),
                    JawBoneWidth = table.Column<float>(nullable: false),
                    JawBoneBackLenght = table.Column<float>(nullable: false),
                    ChimpBoneLowering = table.Column<float>(nullable: false),
                    ChimpBoneLenght = table.Column<float>(nullable: false),
                    ChimpBoneWidth = table.Column<float>(nullable: false),
                    ChimpHole = table.Column<float>(nullable: false),
                    NeckThikness = table.Column<float>(nullable: false),
                    EyebrowsId = table.Column<int>(nullable: false),
                    SecondEyebrowsColor = table.Column<int>(nullable: false),
                    EyeBrowsOpacity = table.Column<float>(nullable: false),
                    FirstEyebrowsColor = table.Column<int>(nullable: false),
                    LipstickId = table.Column<int>(nullable: false),
                    FirstLipstickColor = table.Column<int>(nullable: false),
                    LipstickOpacity = table.Column<float>(nullable: false),
                    SecondLipstickColor = table.Column<int>(nullable: false),
                    MakeupId = table.Column<float>(nullable: false),
                    FirstMakeupColor = table.Column<int>(nullable: false),
                    MakeupOpacity = table.Column<float>(nullable: false),
                    SecondMakeupColor = table.Column<int>(nullable: false),
                    GlassesId = table.Column<int>(nullable: false),
                    GlassesTexture = table.Column<int>(nullable: false),
                    HairId = table.Column<int>(nullable: false),
                    HairTexture = table.Column<int>(nullable: false),
                    HairColor = table.Column<int>(nullable: false),
                    HairColorTwo = table.Column<int>(nullable: false),
                    HatId = table.Column<int>(nullable: false),
                    HatTexture = table.Column<int>(nullable: false),
                    TopId = table.Column<int>(nullable: false),
                    TopTexture = table.Column<int>(nullable: false),
                    TorsoId = table.Column<int>(nullable: false),
                    TorsoTexture = table.Column<int>(nullable: false),
                    UndershirtId = table.Column<int>(nullable: false),
                    UndershirtTexture = table.Column<int>(nullable: false),
                    LegsId = table.Column<int>(nullable: false),
                    LegsTexture = table.Column<int>(nullable: false),
                    ShoesId = table.Column<int>(nullable: false),
                    ShoesTexture = table.Column<int>(nullable: false)
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
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Tag = table.Column<string>(nullable: true),
                    Grant = table.Column<int>(nullable: false),
                    MaxPayday = table.Column<int>(nullable: false),
                    Money = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    GroupType = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    LeaderId = table.Column<int>(nullable: false),
                    DefaultRankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Characters_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Groups_Characters_LeaderId",
                        column: x => x.LeaderId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingTenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuildingId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false),
                    TenantAdded = table.Column<DateTime>(nullable: false),
                    CanEditBuilding = table.Column<bool>(nullable: false),
                    CanWithdrawDeposit = table.Column<bool>(nullable: false),
                    CanManagmentTenants = table.Column<bool>(nullable: false),
                    CanManagmentMagazine = table.Column<bool>(nullable: false),
                    CanRespawnInBuilding = table.Column<bool>(nullable: false),
                    CanLockDoor = table.Column<bool>(nullable: false),
                    CanManagmentGuests = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingTenants_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingTenants_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupRanks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Rights = table.Column<int>(nullable: false),
                    Salary = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    DefaultForGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupRanks_Groups_DefaultForGroupId",
                        column: x => x.DefaultForGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupRanks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
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
                    OwnerId = table.Column<int>(nullable: true),
                    GroupId = table.Column<int>(nullable: true),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    RotRoll = table.Column<float>(nullable: false),
                    RotPitch = table.Column<float>(nullable: false),
                    RotYaw = table.Column<float>(nullable: false),
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
                        name: "FK_Vehicles_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Characters_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PosX = table.Column<float>(nullable: false),
                    PosY = table.Column<float>(nullable: false),
                    PosZ = table.Column<float>(nullable: false),
                    GroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Salary = table.Column<int>(nullable: false),
                    DutyMinutes = table.Column<int>(nullable: false),
                    Rights = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false),
                    GroupRankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupWorkers_GroupRanks_GroupRankId",
                        column: x => x.GroupRankId,
                        principalTable: "GroupRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatorId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    FirstParameter = table.Column<double>(nullable: true),
                    SecondParameter = table.Column<double>(nullable: true),
                    ThirdParameter = table.Column<double>(nullable: true),
                    FourthParameter = table.Column<double>(nullable: true),
                    ItemEntityType = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: true),
                    VehicleId = table.Column<int>(nullable: true),
                    BuildingId = table.Column<int>(nullable: true),
                    WarehouseId = table.Column<int>(nullable: true),
                    VehicleUpgradeId = table.Column<int>(nullable: true),
                    ItemInUse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Accounts_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Vehicles_VehicleUpgradeId",
                        column: x => x.VehicleUpgradeId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountPremiums_AccountId",
                table: "AccountPremiums",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Atms_CreatorId",
                table: "Atms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CharacterId",
                table: "Buildings",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CreatorId",
                table: "Buildings",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingTenants_BuildingId",
                table: "BuildingTenants",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingTenants_CharacterId",
                table: "BuildingTenants",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_CreatorId",
                table: "BusStops",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStopStations_BusStopId",
                table: "BusStopStations",
                column: "BusStopId");

            migrationBuilder.CreateIndex(
                name: "IX_BusStopStations_CreatorId",
                table: "BusStopStations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterDescriptions_CharacterId",
                table: "CharacterDescriptions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterLooks_CharacterId",
                table: "CharacterLooks",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_AccountId",
                table: "Characters",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRanks_DefaultForGroupId",
                table: "GroupRanks",
                column: "DefaultForGroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupRanks_GroupId",
                table: "GroupRanks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_CreatorId",
                table: "Groups",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups",
                column: "LeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_CharacterId",
                table: "GroupWorkers",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupId",
                table: "GroupWorkers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupWorkers_GroupRankId",
                table: "GroupWorkers",
                column: "GroupRankId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BuildingId",
                table: "Items",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CharacterId",
                table: "Items",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatorId",
                table: "Items",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_VehicleId",
                table: "Items",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_VehicleUpgradeId",
                table: "Items",
                column: "VehicleUpgradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WarehouseId",
                table: "Items",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopAssortments_ShopId",
                table: "ShopAssortments",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_GroupId",
                table: "Vehicles",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_OwnerId",
                table: "Vehicles",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_GroupId",
                table: "Warehouses",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPremiums");

            migrationBuilder.DropTable(
                name: "Atms");

            migrationBuilder.DropTable(
                name: "BuildingTenants");

            migrationBuilder.DropTable(
                name: "BusStopStations");

            migrationBuilder.DropTable(
                name: "CharacterDescriptions");

            migrationBuilder.DropTable(
                name: "CharacterLooks");

            migrationBuilder.DropTable(
                name: "GroupWorkers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ShopAssortments");

            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "GroupRanks");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
