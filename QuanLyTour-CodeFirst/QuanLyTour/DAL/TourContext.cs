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
        public TourContext() : base("TourContext"){}

        public DbSet<TourGroup> TourGroup { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}