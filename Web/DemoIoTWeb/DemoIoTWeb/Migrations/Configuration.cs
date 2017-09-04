namespace DemoIoTWeb.Migrations
{
    using DemoIoTWeb.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<DemoIoTWeb.Models.DemoIoTContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DemoIoTWeb.Models.DemoIoTContext context)
        {
            User admin = new User();
            admin.Email = "admin@admin.com";
            admin.Password = Crypto.HashPassword("admin123");
            admin.isAdmin = true;
            context.Users.Add(admin);
            context.SaveChanges();

            User testUser = new User();
            testUser.Email = "test@demoiotsociesc.com.br";
            testUser.Password = Crypto.HashPassword("test123");
            context.Users.Add(testUser);
            context.SaveChanges();

            if (context.Devices.Count() == 0)
            {
                Device device = new Device();
                device.SerialNumber = "1";
                device.Description = "Dispositivo de teste";
                device.User = testUser;
                context.Devices.Add(device);
                context.SaveChanges();

                for(int i = 0; i < 100; i++)
                {
                    DeviceUpdate update = new DeviceUpdate();
                    update.Value = i.ToString();
                    update.DateTime = DateTime.Now;
                    device.Updates.Add(update);
                }

                context.SaveChanges();
            }
        }
    }
}
