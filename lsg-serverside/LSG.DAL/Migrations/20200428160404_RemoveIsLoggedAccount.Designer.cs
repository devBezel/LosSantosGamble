﻿// <auto-generated />
using System;
using LSG.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LSG.DAL.Migrations
{
    [DbContext(typeof(RoleplayContext))]
    [Migration("20200428160404_RemoveIsLoggedAccount")]
    partial class RemoveIsLoggedAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LSG.DAL.Database.Models.AccountModels.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<int>("Rank");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.AccountModels.AccountPremium", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("BoughtTime");

                    b.Property<DateTime>("EndTime");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountPremiums");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BankModels.Atm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("CreatorId");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Atms");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BuildingModels.BuildingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Balance");

                    b.Property<int>("BuildingType");

                    b.Property<int?>("CharacterId");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("CreatorId");

                    b.Property<int>("CurrentObjectsCount");

                    b.Property<string>("Description");

                    b.Property<int>("EntryFee");

                    b.Property<float>("ExternalPickupPositionX");

                    b.Property<float>("ExternalPickupPositionY");

                    b.Property<float>("ExternalPickupPositionZ");

                    b.Property<bool>("HasCCTV");

                    b.Property<bool>("HasSafe");

                    b.Property<float>("InternalPickupPositionX");

                    b.Property<float>("InternalPickupPositionY");

                    b.Property<float>("InternalPickupPositionZ");

                    b.Property<int>("MaxObjectsCount");

                    b.Property<string>("Name");

                    b.Property<bool>("OnSale");

                    b.Property<int>("SaleCost");

                    b.Property<bool>("SpawnPossible");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BuildingModels.BuildingTenantModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildingId");

                    b.Property<bool>("CanEditBuilding");

                    b.Property<bool>("CanLockDoor");

                    b.Property<bool>("CanManagmentGuests");

                    b.Property<bool>("CanManagmentMagazine");

                    b.Property<bool>("CanManagmentTenants");

                    b.Property<bool>("CanRespawnInBuilding");

                    b.Property<bool>("CanWithdrawDeposit");

                    b.Property<int>("CharacterId");

                    b.Property<DateTime>("TenantAdded");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CharacterId");

                    b.ToTable("BuildingTenants");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BusModels.BusStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("CreatorId");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("BusStops");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BusModels.BusStopStation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusStopId");

                    b.Property<int>("Cost");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("CreatorId");

                    b.Property<string>("Name");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<float>("Time");

                    b.HasKey("Id");

                    b.HasIndex("BusStopId");

                    b.HasIndex("CreatorId");

                    b.ToTable("BusStopStations");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("Age");

                    b.Property<float>("Armor");

                    b.Property<float>("Bank");

                    b.Property<bool>("BankStatus");

                    b.Property<string>("Description");

                    b.Property<int>("Dimension");

                    b.Property<float>("DirtyMoney");

                    b.Property<float>("GamblePoints");

                    b.Property<bool>("Gender");

                    b.Property<float>("Health");

                    b.Property<float>("Height");

                    b.Property<string>("History");

                    b.Property<float>("Hunger");

                    b.Property<float>("Money");

                    b.Property<string>("Name");

                    b.Property<bool>("Online");

                    b.Property<string>("PicUrl");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<DateTime>("RecentlyPlayed");

                    b.Property<float>("Rotation");

                    b.Property<string>("Surname");

                    b.Property<float>("Thirsty");

                    b.Property<int>("TimeSpent");

                    b.Property<float>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.CharacterDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterDescriptions");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.CharacterLook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AgeingId");

                    b.Property<float>("AgeingOpacity");

                    b.Property<float>("BeardColor");

                    b.Property<int>("BeardId");

                    b.Property<float>("BeardOpacity");

                    b.Property<int>("BlemishesId");

                    b.Property<float>("BlemishesOpacity");

                    b.Property<int>("BlushColor");

                    b.Property<int>("BlushId");

                    b.Property<float>("BlushOpacity");

                    b.Property<int>("CharacterId");

                    b.Property<float>("CheeksBoneWidth");

                    b.Property<float>("CheeksWidth");

                    b.Property<float>("ChimpBoneLenght");

                    b.Property<float>("ChimpBoneLowering");

                    b.Property<float>("ChimpBoneWidth");

                    b.Property<float>("ChimpHole");

                    b.Property<int>("EarsColor");

                    b.Property<float>("EyeBrownForward");

                    b.Property<float>("EyeBrownHigh");

                    b.Property<float>("EyeBrowsOpacity");

                    b.Property<int>("EyebrowsId");

                    b.Property<float>("EyesOpenning");

                    b.Property<int>("FatherFaceId");

                    b.Property<int>("FirstEyebrowsColor");

                    b.Property<int>("FirstLipstickColor");

                    b.Property<int>("FirstMakeupColor");

                    b.Property<int>("GlassesId");

                    b.Property<int>("GlassesTexture");

                    b.Property<int>("HairColor");

                    b.Property<int>("HairColorTwo");

                    b.Property<int>("HairId");

                    b.Property<int>("HairTexture");

                    b.Property<int>("HatId");

                    b.Property<int>("HatTexture");

                    b.Property<float>("JawBoneBackLenght");

                    b.Property<float>("JawBoneWidth");

                    b.Property<int>("LegsId");

                    b.Property<int>("LegsTexture");

                    b.Property<float>("LipsThickness");

                    b.Property<int>("LipstickId");

                    b.Property<float>("LipstickOpacity");

                    b.Property<float>("MakeupId");

                    b.Property<float>("MakeupOpacity");

                    b.Property<int>("MotherFaceId");

                    b.Property<float>("NeckThikness");

                    b.Property<float>("NoseBoneHigh");

                    b.Property<float>("NoseBoneTwist");

                    b.Property<float>("NosePeakHight");

                    b.Property<float>("NosePeakLenght");

                    b.Property<float>("NosePeakLowering");

                    b.Property<float>("NoseWidth");

                    b.Property<int>("SecondEyebrowsColor");

                    b.Property<int>("SecondLipstickColor");

                    b.Property<int>("SecondMakeupColor");

                    b.Property<float>("ShapeMix");

                    b.Property<int>("ShoesId");

                    b.Property<int>("ShoesTexture");

                    b.Property<int>("SkinColour");

                    b.Property<int>("TopId");

                    b.Property<int>("TopTexture");

                    b.Property<int>("TorsoId");

                    b.Property<int>("TorsoTexture");

                    b.Property<int>("UndershirtId");

                    b.Property<int>("UndershirtTexture");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("CharacterLooks");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int>("CreatorId");

                    b.Property<int>("DefaultRankId");

                    b.Property<int>("Grant");

                    b.Property<int>("GroupType");

                    b.Property<int>("LeaderId");

                    b.Property<int>("MaxPayday");

                    b.Property<int>("Money");

                    b.Property<string>("Name");

                    b.Property<string>("Tag");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("LeaderId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupRankModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultForGroupId");

                    b.Property<int>("GroupId");

                    b.Property<string>("Name");

                    b.Property<int>("Rights");

                    b.Property<int>("Salary");

                    b.HasKey("Id");

                    b.HasIndex("DefaultForGroupId")
                        .IsUnique();

                    b.HasIndex("GroupId");

                    b.ToTable("GroupRanks");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupWorkerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CharacterId");

                    b.Property<int>("DutyMinutes");

                    b.Property<int>("GroupId");

                    b.Property<int>("GroupRankId");

                    b.Property<int>("Rights");

                    b.Property<int>("Salary");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("GroupId");

                    b.HasIndex("GroupRankId");

                    b.ToTable("GroupWorkers");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.ItemModels.ItemModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BuildingId");

                    b.Property<int?>("CharacterId");

                    b.Property<int>("Count");

                    b.Property<int?>("CreatorId");

                    b.Property<double?>("FirstParameter");

                    b.Property<double?>("FourthParameter");

                    b.Property<int?>("GroupId");

                    b.Property<int>("ItemEntityType");

                    b.Property<bool>("ItemInUse");

                    b.Property<string>("Name");

                    b.Property<double?>("SecondParameter");

                    b.Property<double?>("ThirdParameter");

                    b.Property<int?>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("GroupId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.ShopModels.ShopAssortmentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cost");

                    b.Property<int>("Count");

                    b.Property<double?>("FirstParameter");

                    b.Property<double?>("FourthParameter");

                    b.Property<int>("ItemEntityType");

                    b.Property<string>("Name");

                    b.Property<double?>("SecondParameter");

                    b.Property<int>("ShopId");

                    b.Property<double?>("ThirdParameter");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("ShopAssortments");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.ShopModels.ShopModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<int>("ShopEntityType");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.VehicleModels.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("B");

                    b.Property<int>("G");

                    b.Property<int?>("GroupId");

                    b.Property<int>("Health");

                    b.Property<string>("Model");

                    b.Property<int?>("OwnerId");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<int>("R");

                    b.Property<float>("RotPitch");

                    b.Property<float>("RotRoll");

                    b.Property<float>("RotYaw");

                    b.Property<bool>("State");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.AccountModels.AccountPremium", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Account")
                        .WithOne("AccountPremium")
                        .HasForeignKey("LSG.DAL.Database.Models.AccountModels.AccountPremium", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BankModels.Atm", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BuildingModels.BuildingModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BuildingModels.BuildingTenantModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.BuildingModels.BuildingModel", "Building")
                        .WithMany("BuildingTenants")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BusModels.BusStop", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.BusModels.BusStopStation", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.BusModels.BusStop", "BusStop")
                        .WithMany("BusStopStations")
                        .HasForeignKey("BusStopId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.Character", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.CharacterDescription", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithMany("CharacterDescriptions")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.CharacterModels.CharacterLook", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithOne("CharacterLook")
                        .HasForeignKey("LSG.DAL.Database.Models.CharacterModels.CharacterLook", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupRankModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupModel", "DefaultForGroup")
                        .WithOne("DefaultRank")
                        .HasForeignKey("LSG.DAL.Database.Models.GroupModels.GroupRankModel", "DefaultForGroupId");

                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupModel", "Group")
                        .WithMany("Ranks")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.GroupModels.GroupWorkerModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithMany("GroupWorkers")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupModel", "Group")
                        .WithMany("Workers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupRankModel", "GroupRank")
                        .WithMany("Workers")
                        .HasForeignKey("GroupRankId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.ItemModels.ItemModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.BuildingModels.BuildingModel", "Building")
                        .WithMany("ItemsInBuilding")
                        .HasForeignKey("BuildingId");

                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Character")
                        .WithMany("Items")
                        .HasForeignKey("CharacterId");

                    b.HasOne("LSG.DAL.Database.Models.AccountModels.Account", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupModel", "Group")
                        .WithMany("Magazine")
                        .HasForeignKey("GroupId");

                    b.HasOne("LSG.DAL.Database.Models.VehicleModels.Vehicle", "Vehicle")
                        .WithMany("ItemsInVehicle")
                        .HasForeignKey("VehicleId");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.ShopModels.ShopAssortmentModel", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.ShopModels.ShopModel", "Shop")
                        .WithMany("ShopAssortments")
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.VehicleModels.Vehicle", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.GroupModels.GroupModel", "Group")
                        .WithMany("Vehicles")
                        .HasForeignKey("GroupId");

                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}