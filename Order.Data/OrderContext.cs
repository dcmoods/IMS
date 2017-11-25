using Order.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=IMSConnectionString")
        {

        }
        public DbSet<Domain.Order> Orders { get; set; }        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Order");
            base.OnModelCreating(modelBuilder);
        }
    }
    //public class OrderSystemContextConfig : DbConfiguration
    //{
    //    public OrderSystemContextConfig()
    //    {
    //        SetDatabaseInitializer(new NullDatabaseInitializer<OrderContext>());
    //    }

    //}
}
