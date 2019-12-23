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
    [Migration("20191223194708_InitDatabase")]
    partial class InitDatabase
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

                    b.Property<byte?>("AgeingId");

                    b.Property<float?>("AgeingOpacity");

                    b.Property<float?>("BeardColor");

                    b.Property<byte?>("BeardId");

                    b.Property<float?>("BeardOpacity");

                    b.Property<byte?>("BlemishesId");

                    b.Property<float?>("BlemishesOpacity");

                    b.Property<byte?>("BlushColor");

                    b.Property<byte?>("BlushId");

                    b.Property<float?>("BlushOpacity");

                    b.Property<int>("CharacterId");

                    b.Property<float?>("CheeksBoneWidth");

                    b.Property<float?>("CheeksWidth");

                    b.Property<float?>("ChimpBoneLenght");

                    b.Property<float?>("ChimpBoneLowering");

                    b.Property<float?>("ChimpBoneWidth");

                    b.Property<float?>("ChimpHole");

                    b.Property<byte?>("EarsColor");

                    b.Property<float?>("EyeBrownForward");

                    b.Property<float?>("EyeBrownHigh");

                    b.Property<float>("EyeBrowsOpacity");

                    b.Property<byte?>("EyebrowsId");

                    b.Property<float?>("EyesOpenning");

                    b.Property<byte?>("FatherFaceId");

                    b.Property<byte?>("FirstEyebrowsColor");

                    b.Property<byte?>("FirstLipstickColor");

                    b.Property<byte?>("FirstMakeupColor");

                    b.Property<byte?>("GlassesId");

                    b.Property<byte?>("GlassesTexture");

                    b.Property<byte?>("HairColor");

                    b.Property<byte?>("HairColorTwo");

                    b.Property<byte?>("HairId");

                    b.Property<byte?>("HairTexture");

                    b.Property<byte?>("HatId");

                    b.Property<byte?>("HatTexture");

                    b.Property<float?>("JawBoneBackLenght");

                    b.Property<float?>("JawBoneWidth");

                    b.Property<byte?>("LegsId");

                    b.Property<byte?>("LegsTexture");

                    b.Property<float?>("LipsThickness");

                    b.Property<byte?>("LipstickId");

                    b.Property<float?>("LipstickOpacity");

                    b.Property<float?>("MakeupId");

                    b.Property<float?>("MakeupOpacity");

                    b.Property<byte?>("MotherFaceId");

                    b.Property<float?>("NeckThikness");

                    b.Property<float?>("NoseBoneHigh");

                    b.Property<float?>("NoseBoneTwist");

                    b.Property<float?>("NosePeakHight");

                    b.Property<float?>("NosePeakLenght");

                    b.Property<float?>("NosePeakLowering");

                    b.Property<float?>("NoseWidth");

                    b.Property<byte?>("SecondEyebrowsColor");

                    b.Property<byte?>("SecondLipstickColor");

                    b.Property<byte?>("SecondMakeupColor");

                    b.Property<float?>("ShapeMix");

                    b.Property<byte?>("ShoesId");

                    b.Property<byte?>("ShoesTexture");

                    b.Property<byte?>("SkinColour");

                    b.Property<byte?>("TopId");

                    b.Property<byte?>("TopTexture");

                    b.Property<byte?>("TorsoId");

                    b.Property<byte?>("TorsoTexture");

                    b.Property<byte?>("UndershirtId");

                    b.Property<byte?>("UndershirtTexture");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

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

                    b.Property<float>("Rot");

                    b.Property<bool>("State");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Vehicles");
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
                        .WithMany()
                        .HasForeignKey("CharacterId")
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