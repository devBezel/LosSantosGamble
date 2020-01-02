using LSG.DAL.Database.Models;
using LSG.DAL.Database.Models.AccountModels;
using LSG.DAL.Database.Models.CharacterModels;
using LSG.DAL.Database.Models.VehicleModels;
using Microsoft.EntityFrameworkCore;
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
    }
}
