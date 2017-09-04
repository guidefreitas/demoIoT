using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DemoIoTWeb.Models
{
    public class DemoIoTContext : DbContext
    {
        public DemoIoTContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DemoIoTContext>(new DropCreateDatabaseIfModelChanges<DemoIoTContext>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceUpdate> DeviceUpdates { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}