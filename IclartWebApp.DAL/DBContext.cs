using IclartWebApp.Common.Entities;
using IclartWebApp.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IclartWebApp.DAL
{
    public class DBContext : DbContext, ITrackerContext
    {
        public DBContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new IclartDBInitializer());
        }
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<CompetitorEntity> Competitors { get; set; }
        public DbSet<CompetitorDiscountSchemesEntity> CompetitorDiscountSchemes { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CompetitorPricesEntity> CompetitorPrices { get; set; }
        public DbSet<PasswordExpiryEntity> PasswordExpiry { get; set; }
        public DbSet<SOSEntity> SalesOrderSlips { get; set; }
        public DbSet<SOSProductEntity> SOSOrders { get; set; }
        public DbSet<SOSCustomEntity> SOSCustomOrders { get; set; }      
    }

  

    public class IclartDBInitializer : CreateDatabaseIfNotExists<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            base.Seed(context);
        }
    }
}
