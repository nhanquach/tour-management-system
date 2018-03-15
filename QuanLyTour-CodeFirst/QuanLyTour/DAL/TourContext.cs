using QuanLyTour.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public DbSet<TourGroup> TourGroup { get; set; }
        public DbSet<TourGroupDetail> TourGroupDetail { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Bill> Bill { get; set; }

        public DbSet<Tour> Tour { get; set; }
        public DbSet<TourDetail> TourDetail { get; set; }

        public DbSet<Location> Location { get; set; }

    }
}