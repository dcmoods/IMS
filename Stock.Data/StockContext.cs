﻿using Stock.Domain;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using SharedKernel.Data;

namespace Stock.Data
{
   
    public class StockContext : DbContext
    {
        public StockContext() : base("name=IMSConnectionString") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<ItemEntry> ItemEntries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Stock");
            modelBuilder.Types<IStateObject>().Configure(c => c.Ignore(p => p.State));
            base.OnModelCreating(modelBuilder);
        }
    }

    //public class StockContextConfig : DbConfiguration
    //{
    //    public StockContextConfig()
    //    {
    //        SetDatabaseInitializer(new NullDatabaseInitializer<StockContext>());
    //    }
    //}
   
}
