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
    [Migration("20191211194302_EditedVehicle")]
    partial class EditedVehicle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LSG.DAL.Database.Models.Account", b =>
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

            modelBuilder.Entity("LSG.DAL.Database.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<int>("Age");

                    b.Property<float>("Armor");

                    b.Property<float>("Bank");

                    b.Property<bool>("BankStatus");

                    b.Property<string>("Description");

                    b.Property<float>("DirtyMoney");

                    b.Property<string>("Gender");

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

            modelBuilder.Entity("LSG.DAL.Database.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("B");

                    b.Property<int>("G");

                    b.Property<int>("Health");

                    b.Property<string>("Model");

                    b.Property<int?>("OwnerId");

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

            modelBuilder.Entity("LSG.DAL.Database.Models.Character", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("LSG.DAL.Database.Models.Vehicle", b =>
                {
                    b.HasOne("LSG.DAL.Database.Models.Character", "Owner")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
