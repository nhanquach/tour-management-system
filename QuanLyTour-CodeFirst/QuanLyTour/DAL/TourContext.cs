using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace QuanLyTour.DAL
{
    public class TourContext : DbContext
    {
        public TourContext() : base("TourContext")
        {
            Database.SetInitializer<TourContext>(new TourInitializer());
        }

        public DbSet<TourGroup> TourGroups { get; set; }
        public DbSet<TourGroupDetail> TourGroupDetails { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bill> Bills { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourDetail> TourDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}