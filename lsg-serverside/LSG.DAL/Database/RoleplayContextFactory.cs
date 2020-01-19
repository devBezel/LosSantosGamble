﻿using Microsoft.EntityFrameworkCore;
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

        public RoleplayContextFactory() : this("Server=51.38.142.78;Database=lsg;User=algorytm;Password=f7ufcuEXMcNpmaDwgAPuxpUt; convert zero datetime=True") { }

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
