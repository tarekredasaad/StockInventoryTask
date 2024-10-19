﻿using Microsoft.EntityFrameworkCore;
using StayCation.API.VerticalSlicing.Data.Models;
//using StayCation.API.VerticalSlicing.Data.Models;
using System.Reflection;

namespace StayCation.API.VerticalSlicing.Data.Data
{
    public class Context : DbContext
    {
       
        public Context(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }
        //public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Tran> Trans { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }

        //public DbSet<Recipe> Recipes { get; set; }
    }
}
