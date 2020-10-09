using LSG.DAL.Database.Models.GroupModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.DAL.Database
{
    public class RoleplayContextFactory : IDesignTimeDbContextFactory<RoleplayContext>
    {
        public RoleplayContext Create() => this.CreateDbContext(new[] { "" });

        private readonly string _connectionString;
        //Server=localhost;Database=lsg;User=root;Password=; convert zero datetime=True
        public RoleplayContextFactory() : this("Server=localhost;Database=lsg;User=root;Password=root; convert zero datetime=True") { }

        public RoleplayContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }


        public RoleplayContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<RoleplayContext> options = new DbContextOptionsBuilder<RoleplayContext>();
            options.UseMySql(_connectionString);

            return new RoleplayContext(options.Options);
        }

    }
}
