﻿using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.BankModels;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.BusModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.ShopModels;
using LSG.DAL.Database.Models.VehicleModels;
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

    }
}
