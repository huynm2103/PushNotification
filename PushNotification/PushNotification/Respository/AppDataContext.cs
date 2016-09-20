using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using PushNotification.Models;

namespace PushNotification.Respository
{
    public class AppDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Message> Messages { get; set; }

        public AppDataContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer<AppDataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // This line to remove "S" character of table name in database.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // This line set cascadedelete to false for all one-many relation.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}