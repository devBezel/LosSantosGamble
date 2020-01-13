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
    [Migration("20200112142918_AddedAtmModel")]
    partial class AddedAtmModel
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

                    b.Property<float>("DirtyMoney");

                    b.Property<bool>("Gender");

                    b.Property<float>("Health");

                    b.Property<float>("Height");

                    b.Property<string>("History");

                    b.Property<float>("Money");

                    b.Property<string>("Name");

                    b.Property<string>("PicUrl");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<float>("Rotation");

                    b.Property<string>("Surname");

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

            modelBuilder.Entity("LSG.DAL.Database.Models.VehicleModels.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("B");

                    b.Property<int>("G");

                    b.Property<int>("Health");

                    b.Property<string>("Model");

                    b.Property<int>("OwnerId");

                    b.Property<float>("PosX");

                    b.Property<float>("PosY");

                    b.Property<float>("PosZ");

                    b.Property<int>("R");

                    b.Property<float>("RotPitch");

                    b.Property<float>("RotRoll");

                    b.Property<float>("RotYaw");

                    b.Property<bool>("State");

                    b.HasKey("Id");

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

            modelBuilder.Entity("LSG.DAL.Database.Models.VehicleModels.Vehicle", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.CharacterModels.Character", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}