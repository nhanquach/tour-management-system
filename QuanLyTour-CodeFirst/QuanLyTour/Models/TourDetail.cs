using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyTour.Models
{
    public class TourDetail
    {
        public int ID { get; set; }
        public int TourID { get; set; }
        //public int LocationID { get; set; }
        //Because 1 Tour can go to many Location
        public virtual ICollection<Location> Locations { get; set; }
    }
}