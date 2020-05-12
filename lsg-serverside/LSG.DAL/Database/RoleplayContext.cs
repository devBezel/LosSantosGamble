using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.GroupModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.Database.Models.VehicleModels;
using LSG.DAL.Database.Models.WarehouseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database
{
    public class RoleplayContext : DbContext
    {
        public RoleplayContext(DbContextOptions<RoleplayContext> options) : base(options) { }


        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<CharacterDescription> CharacterDescriptions { get; set; }
        public DbSet<CharacterLook> CharacterLooks { get; set; }
        public DbSet<AccountPremium> AccountPremiums { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<BusStopStation> BusStopStations { get; set; }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<BuildingModel> Buildings { get; set; }
        public DbSet<BuildingTenantModel> BuildingTenants { get; set; }
        public DbSet<ShopModel> Shops { get; set; }
        public DbSet<ShopAssortmentModel> ShopAssortments { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<GroupWorkerModel> GroupWorkers { get; set; }
        public DbSet<GroupRankModel> GroupRanks { get; set; }
        public DbSet<WarehouseModel> Warehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupModel>()
                .HasOne(group => group.DefaultRank)
                .WithOne(rank => rank.DefaultForGroup)
                .HasForeignKey<GroupRankModel>(groupRank => groupRank.DefaultForGroupId);


            modelBuilder.Entity<GroupModel>()
                .HasMany(group => group.Ranks)
                .WithOne(rank => rank.Group)
                .HasForeignKey(rank => rank.GroupId);
        }

    }
}
