using LSG.DAL.Database.Models;
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
    }
}
